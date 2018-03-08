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
            defenseButtons.alpha = 0;
            defenseButtons.interactable = false;
            defenseButtons.blocksRaycasts = false;
            attackButtons.alpha = 1;
            attackButtons.interactable = true;
            attackButtons.blocksRaycasts = true;

        }
        else if (player.currentTurnType == TurnType.Defense)
        {
            attackButtons.alpha = 0;
            attackButtons.interactable = false;
            attackButtons.blocksRaycasts = false;
            defenseButtons.alpha = 1;
            defenseButtons.interactable = true;
            defenseButtons.blocksRaycasts = true;

        }
        maxTimeBarWidth = timeBarTransform.sizeDelta.x;
        turnTimeLeft = turnTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(turnTimeLeft <= 0.01){
            if(currentFireballs.Count <= 0){
                if(player){
                    
                    if(currentEnemy && !currentEnemy.spawning){
                        turnSwitch(); 
                    }
                    else{
                        //TODO: Spawn the next enemy
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
        if(player){
            if (player.currentTurnType == TurnType.Attack){
                switchPlayerToDefense(); 
            }
            else{
                switchPlayerToAttack();
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
//        print("Fireballs left: "+currentFireballs.Count);
    }
}
