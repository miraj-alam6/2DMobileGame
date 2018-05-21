using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController: MonoBehaviour {
    public Unit[] enemyList;
    public EnemyData[] enemiesList;
    public Transform enemySpawnPoint;
    public Transform enemyTempSpawnPoint;
    public Unit baseEnemy;
    public bool isPracticeLevel;
    public float spawnWaitTime =1f; //how long we wait to spawn next enemy once current enemy dies
    private bool isSpawning;
    private int currentEnemyIndex =0;
    public int[] checkPoints;
	// Use this for initialization
	void Start () {
        isSpawning = false;
        GameplayController.instance.levelController = this;
        checkCheckPoints();

        //spawnNextEnemy();
        if(!isPracticeLevel){
            if(GameplayController.instance.entranceOn){
               // Invoke("nextEnemySpawn",3);  
            }
            else{
                nextEnemySpawn(); //this happens instantly, so do this in beginning for now. Band-aid solution,
            }
        }            //not how it will work in game

	}
	


    public void checkCheckPoints(){
        int highestReached = 0;

        foreach(int c in checkPoints){
            if(GameDataController.instance.highestEnemyIndexReached >= c){
                highestReached = c;
            }
        }
        currentEnemyIndex = highestReached;
    }

    public void spawnNextEnemy()
    {
        if (isPracticeLevel)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                Invoke("practiceArenaSpawn", spawnWaitTime);
            }
        }
        else
        {
           
            if (!isSpawning)
            {
                isSpawning = true;
                Invoke("nextEnemySpawn", spawnWaitTime);
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void practiceArenaSpawn(){
        Unit unit = (Instantiate(enemyList[0],enemyTempSpawnPoint.position, enemyTempSpawnPoint.rotation));
        unit.GetComponent<AIController>().enemyData = enemiesList[0]; // TODO: Messy broken window solution, need to rethink this with object pooling
        isSpawning = false;
    }
    public void nextEnemySpawn()
    {
        if(currentEnemyIndex < enemiesList.Length){
            GameDataController.instance.highestEnemyIndexReached = currentEnemyIndex;
            Unit unit = (Instantiate(baseEnemy, enemyTempSpawnPoint.position, enemyTempSpawnPoint.rotation));
            unit.GetComponent<AIController>().enemyData = enemiesList[currentEnemyIndex]; // TODO: Messy broken window solution, need to rethink this with object pooling
            isSpawning = false;
            currentEnemyIndex++;
        }
        else{
            //TODO: Level is done, so we have to clear highestIndex reached and load next level.
            GameDataController.instance.highestEnemyIndexReached = 0;

        }
    }

}
