using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController: MonoBehaviour {
    public Unit[] EnemyList;
    public Transform EnemySpawnPoint;
    public Transform EnemyTempSpawnPoint;
	// Use this for initialization
	void Start () {
        GameplayController.instance.levelController = this;
	}
	


    public void nextEnemy(){
        
    }
	// Update is called once per frame
	void Update () {
		
	}

}
