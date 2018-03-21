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
	// Use this for initialization
	void Start () {
        isSpawning = false;
        GameplayController.instance.levelController = this;
        //spawnNextEnemy();
        if(!isPracticeLevel){
        nextEnemySpawn(); //this happens instantly, so do this in beginning for now. Band-aid solution,
        }            //not how it will work in game

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
            Unit unit = (Instantiate(baseEnemy, enemyTempSpawnPoint.position, enemyTempSpawnPoint.rotation));
            unit.GetComponent<AIController>().enemyData = enemiesList[currentEnemyIndex]; // TODO: Messy broken window solution, need to rethink this with object pooling
            isSpawning = false;
            currentEnemyIndex++;
        }
    }

}
