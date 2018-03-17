using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController: MonoBehaviour {
    public Unit[] enemyList;
    public Transform enemySpawnPoint;
    public Transform enemyTempSpawnPoint;
    public bool isPracticeLevel;
    public float spawnWaitTime =1f; //how long we wait to spawn next enemy once current enemy dies
    private bool isSpawning;
	// Use this for initialization
	void Start () {
        isSpawning = false;
        GameplayController.instance.levelController = this;
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
            //Real code here todo
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void practiceArenaSpawn(){
        isSpawning = false;
        Instantiate(enemyList[0],enemyTempSpawnPoint.position, enemyTempSpawnPoint.rotation);
    }

}
