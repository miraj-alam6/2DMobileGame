using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {
    public Unit player;
    public Unit currentEnemy;
    public float turnTime;
    public float turnTimeLeft;
    public RectTransform timeBarTransform;
    public CanvasGroup attackButtons;
    public CanvasGroup defenseButtons;
    private float maxTimeBarWidth;
    public static GameplayController instance = null;
    private int lastUnitID=-1;
    private List<Offense> currentFireballs;
    public LevelController levelController;
    public VitalsUI playerVitalsUI; 
    public VitalsUI enemyVitalsUI; //need this here so gameplay controller can pass on the UI reference
    //to newly spawned enemies.
    public Transform particleSystemHolder; //This is to hold particle systems so that they don't get
    //destroyed when parent gets destroyed.
    public float turnInterludeTime = 0.5f;
    private float turnCurrentInterludeTime = 0;

    private void Awake(){
        currentFireballs = new List<Offense>();
        if(instance){
            Destroy(this.gameObject);
        }
        else{
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        
        if (player.currentTurnType == TurnType.Attack)
        {
//            defenseButtons.alpha = 0;
            defenseButtons.alpha = 0.5f;
            defenseButtons.interactable = false;
            defenseButtons.blocksRaycasts = false;
            attackButtons.alpha = 1;
            attackButtons.interactable = true;
            attackButtons.blocksRaycasts = true;

        }
        else if (player.currentTurnType == TurnType.Defense)
        {
            //attackButtons.alpha = 0;
            attackButtons.alpha = 0.5f;
            attackButtons.interactable = false;
            attackButtons.blocksRaycasts = false;
            defenseButtons.alpha = 1;
            defenseButtons.interactable = true;
            defenseButtons.blocksRaycasts = true;

        }
        maxTimeBarWidth = timeBarTransform.sizeDelta.x;
        turnTimeLeft = turnTime;
        turnCurrentInterludeTime = turnInterludeTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(turnTimeLeft <= 0.00001){
            if(currentFireballs.Count <= 0){
                turnCurrentInterludeTime -= Time.deltaTime;
                if(player){
                    if(currentEnemy && !currentEnemy.spawning && turnCurrentInterludeTime <=0.0001f){
                        turnSwitch(); 
                    }
                    else{
                        //TODO: Spawn the next enemy, right now this is being done in a different way
                    }
                }
            }
            else{
                stopFireballShooting();
            }
            
        }
        else{
            reduceTime();
        }
	}
    //No parameters
    public void reduceTime(){
        turnTimeLeft = Mathf.Clamp(turnTimeLeft - Time.deltaTime, 0, turnTime);
        UpdateTimeUI();
    } 
    public void UpdateTimeUI(){
        timeBarTransform.sizeDelta = new Vector2(((float)turnTimeLeft / turnTime) *maxTimeBarWidth, timeBarTransform.sizeDelta.y);
    }
    public void tempTurnSwitch(){
        turnTimeLeft = turnTime;
        if(player){
            if(player.currentTurnType == TurnType.Defense){
                player.currentTurnType = TurnType.Attack;
                defenseButtons.alpha = 0;
                defenseButtons.interactable = false;
                defenseButtons.blocksRaycasts = false;
                attackButtons.alpha = 1;
                attackButtons.interactable = true;
                attackButtons.blocksRaycasts = true;
                player.stopShield();
            }
            else if(player.currentTurnType == TurnType.Attack){
                player.currentTurnType = TurnType.Defense;
                attackButtons.alpha = 0;
                attackButtons.interactable = false;
                attackButtons.blocksRaycasts = false;
                defenseButtons.alpha = 1;
                defenseButtons.interactable = true;
                defenseButtons.blocksRaycasts = true;

            }
            else{
                Debug.LogError("Invalid TurnType");
            }
        }
    }
    //Once turn timer is up, you want to stop fireballs, but let the other unit continue blocking.
    public void stopFireballShooting(){
        if(player && player.currentTurnType == TurnType.Attack){
            player.preventFireballs = true;
        }
        else{
            if(currentEnemy){
                currentEnemy.preventFireballs = true;
            }
        }
    }
    public void turnSwitch(){
        turnTimeLeft = turnTime;
        turnCurrentInterludeTime = turnInterludeTime;
        if(player){
            if (currentEnemy)
            {
                //TODO: Also need to stop thinking coroutine first at this point, just do it in 
                //clear action queue
                currentEnemy.GetComponent<AIController>().ClearActionQueue();
                currentEnemy.stopShield();
            }
            if (player.currentTurnType == TurnType.Attack){
                //switchPlayerToDefense();
                switchPlayerToDefenseDeux();
            }
            else{
                //switchPlayerToAttack();
                switchPlayerToAttackDeux();
            }

        }
    }
    public void switchPlayerToAttack(){
        player.currentTurnType = TurnType.Attack;
        defenseButtons.alpha = 0;
        defenseButtons.interactable = false;
        defenseButtons.blocksRaycasts = false;
        attackButtons.alpha = 1;
        attackButtons.interactable = true;
        attackButtons.blocksRaycasts = true;
        player.stopShield();
        player.preventFireballs = false;
        currentEnemy.currentTurnType = TurnType.Defense;
    }
    public void switchPlayerToDefense(){
        player.currentTurnType = TurnType.Defense;
        attackButtons.alpha = 0;
        attackButtons.interactable = false;
        attackButtons.blocksRaycasts = false;
        defenseButtons.alpha = 1;
        defenseButtons.interactable = true;
        defenseButtons.blocksRaycasts = true;
        currentEnemy.preventFireballs = false;
        currentEnemy.currentTurnType = TurnType.Attack;

    }

    public void switchPlayerToAttackDeux()
    {
        player.currentTurnType = TurnType.Attack;
        defenseButtons.alpha = 0.5f;
        defenseButtons.interactable = false;
        defenseButtons.blocksRaycasts = false;
        attackButtons.alpha = 1;
        attackButtons.interactable = true;
        attackButtons.blocksRaycasts = true;
        player.stopShield();
        player.preventFireballs = false;
        currentEnemy.currentTurnType = TurnType.Defense;
    }
    public void switchPlayerToDefenseDeux()
    {
        player.currentTurnType = TurnType.Defense;
        attackButtons.alpha = 0.5f;
        attackButtons.interactable = false;
        attackButtons.blocksRaycasts = false;
        defenseButtons.alpha = 1;
        defenseButtons.interactable = true;
        defenseButtons.blocksRaycasts = true;
        currentEnemy.preventFireballs = false;
        currentEnemy.currentTurnType = TurnType.Attack;

    }
    public int GetNextUnitID(){
        return ++lastUnitID;
    }

    public void addFireball(Offense fireball)
    {
        currentFireballs.Add(fireball);
    }

    public void removeFireball(Offense fireball)
    {
        currentFireballs.Remove(fireball);
      //  print("Fireballs left: "+currentFireballs.Count);
    }

   
}
