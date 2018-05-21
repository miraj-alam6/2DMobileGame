using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameplayController : MonoBehaviour {
    public Unit player;
    public Unit currentEnemy;
    public float turnTime;
    public float turnTimeLeft;
    public RectTransform timeBarTransform;
    public Text playerTurnStatusText;
    public Text enemyTurnStatusText;

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
    public bool lightPause; //Stop the timer but not for actually pausing the game. Mainly for things to happen like spawning
    public bool pauseGame;
    public CanvasGroup pauseMenu;

    public bool entranceOn = false;
    public bool useObjectPooling = false;

    public int iterationDebugger;

  

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
        UnpauseGame();
        //Player is set from the inspector
        if(GameDataController.instance){
            GameDataController.instance.LoadDataIntoPlayer(player);
        }

        if(entranceOn){
            player.currentTurnType = TurnType.Standby;
            player.preventFireballs = true;
            lightPause = true;
        }
        else{
            if (player.currentTurnType == TurnType.Attack)
            {
    //            defenseButtons.alpha = 0;
                //defenseButtons.alpha = 0.5f;
                //defenseButtons.interactable = false;
                //defenseButtons.blocksRaycasts = false;
                turnOffCanvasGroup(defenseButtons,Constants.InactiveButtonAlpha);
                turnOnCanvasGroup(attackButtons);
                //attackButtons.alpha = 1;
                //attackButtons.interactable = true;
                //attackButtons.blocksRaycasts = true;

            }
            else if (player.currentTurnType == TurnType.Defense)
            {
                //attackButtons.alpha = 0;
                //attackButtons.alpha = 0.5f;
                //attackButtons.interactable = false;
                //attackButtons.blocksRaycasts = false;
                turnOffCanvasGroup(attackButtons,Constants.InactiveButtonAlpha);
                //defenseButtons.alpha = 1;
                //defenseButtons.interactable = true;
                //defenseButtons.blocksRaycasts = true;
                turnOnCanvasGroup(defenseButtons);

            }
        }
        maxTimeBarWidth = timeBarTransform.sizeDelta.x;
        turnTimeLeft = turnTime;
        turnCurrentInterludeTime = turnInterludeTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(turnTimeLeft <= 0.00001){
            if(currentFireballs.Count <= 0){
                if(player){
                    if(currentEnemy && !currentEnemy.spawning && 
                       (!entranceOn || currentEnemy.entrance.entranceDone))
                    {
                        if(turnCurrentInterludeTime <= 0.0001f){
                            turnSwitch(); 
                        }
                        else{
                            turnCurrentInterludeTime -= Time.deltaTime;
                        }
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
            if (lightPause && player && player.currentTurnType == TurnType.Standby)
                //The second two conditions are there because we want to pause time reduction during
                //only the intro. once an enemy dies we want time to continue
            {
                
            }
            else{
                reduceTime();
            }
        }

        //
//        print(iterationDebugger);
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
                timeBarTransform.anchorMin = new Vector2(0, 0.5f);
                timeBarTransform.anchorMax = new Vector2(0, 0.5f);
                timeBarTransform.pivot = new Vector2(0, 0.5f);

                playerTurnStatusText.text = "DEF";
                enemyTurnStatusText.text = "ATK";

            }
            else{
                //switchPlayerToAttack();
                switchPlayerToAttackDeux();
                playerTurnStatusText.text = "ATK";
                enemyTurnStatusText.text = "DEF";
                timeBarTransform.anchorMin = new Vector2(1, 0.5f);
                timeBarTransform.anchorMax = new Vector2(1, 0.5f);
                timeBarTransform.pivot = new Vector2(1, 0.5f);
           


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
        if(currentEnemy.entrance.entranceDone){
            currentEnemy.currentTurnType = TurnType.Defense;
        }
        else{
            currentEnemy.currentTurnType = TurnType.Standby;
        }
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
        if (currentEnemy.entrance.entranceDone)
        {
            currentEnemy.currentTurnType = TurnType.Attack;
        }
        else{
            currentEnemy.currentTurnType = TurnType.Standby;

        }

    }

    public void switchPlayerToAttackDeux()
    {
        player.currentTurnType = TurnType.Attack;
        //defenseButtons.alpha = 0.5f;
        //defenseButtons.interactable = false;
        //defenseButtons.blocksRaycasts = false;
        turnOffCanvasGroup(defenseButtons,Constants.InactiveButtonAlpha);
        //attackButtons.alpha = 1;
        //attackButtons.interactable = true;
        //attackButtons.blocksRaycasts = true;
        turnOnCanvasGroup(attackButtons);
        player.stopShield();
        player.preventFireballs = false;
        currentEnemy.currentTurnType = TurnType.Defense;
    }
    public void switchPlayerToDefenseDeux()
    {
        player.currentTurnType = TurnType.Defense;
        //attackButtons.alpha = 0.5f;
        //attackButtons.interactable = false;
        //attackButtons.blocksRaycasts = false;
        turnOffCanvasGroup(attackButtons,Constants.InactiveButtonAlpha);
        //defenseButtons.alpha = 1;
        //defenseButtons.interactable = true;
        //defenseButtons.blocksRaycasts = true;
        turnOnCanvasGroup(defenseButtons);
        currentEnemy.preventFireballs = false;
        currentEnemy.currentTurnType = TurnType.Attack;

    }
    public void turnOnCanvasGroup(CanvasGroup cG){
        cG.alpha = 1;
        cG.interactable = true;
        cG.blocksRaycasts = true;
    }
    public void turnOnCanvasGroup(CanvasGroup cG,float alpha)
    {
        cG.alpha = alpha;
        cG.interactable = true;
        cG.blocksRaycasts = true;
    }
    public void turnOffCanvasGroup(CanvasGroup cG)
    {
        cG.alpha = 0;
        cG.interactable = false;
        cG.blocksRaycasts = false;
    }
    public void turnOffCanvasGroup(CanvasGroup cG, float alpha)
    {
        cG.alpha = alpha;
        cG.interactable = false;
        cG.blocksRaycasts = false;
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

    public void PauseGame(){
        print("Paused Game");
        Time.timeScale = 0;
        turnOnCanvasGroup(pauseMenu,0.7f);
        pauseGame = true;
    }
    public void UnpauseGame()
    {
//        print("Unpaused the game");
        pauseGame = false;
        Time.timeScale = 1.0f;
        turnOffCanvasGroup(pauseMenu);
    }

    //This doesn't fully pause the game but it prevents timer from going down as well as actions
    public void LightPause(){
        
    }

    public void LightUnpause(){
        
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //resets checkpoint
    public void RestartFullLevel()
    {
        GameDataController.instance.highestEnemyIndexReached = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        RestartLevel();
    }

    public void FinishSpawning(string tagName){
        
        if (player && player.currentTurnType == TurnType.Attack)
        {
            player.preventFireballs = true;
        }

        if(lightPause){
            //If player spawns (which is in the beginning), then spawn the first enemy next.
            if(tagName.Equals(TagNames.Player)){
                player.showVitals();
                levelController.nextEnemySpawn();         
            }
            else{
                

            }
        }

        if (player && currentEnemy && player.entrance.entranceDone
            && currentEnemy.entrance.entranceDone)
        {
            currentEnemy.showVitals();
            lightPause = false;
            //first turn of the game
            if(player.currentTurnType == TurnType.Standby && currentEnemy.currentTurnType 
               == TurnType.Standby){
                player.currentTurnType = TurnType.Attack;
                player.preventFireballs = false;
                currentEnemy.currentTurnType = TurnType.Defense;
            }
            else{ //only need to set enemy's turn type

                if ( player.currentTurnType == TurnType.Attack)
                {

                    currentEnemy.currentTurnType = TurnType.Defense;
                    player.preventFireballs = true;

                }
                    else{
                    currentEnemy.currentTurnType = TurnType.Attack;
                    player.preventFireballs = false;
                }
            }
        }
       
    }

    public void SpawnNextEnemy(){
        if(entranceOn){
            lightPause = true;
        }
        levelController.spawnNextEnemy();
    }
   
}
