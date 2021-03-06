﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour {
    public static GameDataController instance;
    public float playerMaxHP;
    public float playerMaxMP;
    public float playerMaxSP;
    public float playerRegenRate;
    public float shieldGeneralCooldownTime = 0.1f; //Cooldown time when you run out of MP or when you let go of one shield 
    public float shieldOverpoweredCooldownTime = 0.1f; //cooldown time when an offense is higher than your shield
    public float shieldBreakCooldownTime = 1f;
    public float preEntranceTime;
    public float postEntranceTime;
    public float entranceSpeed;
    public int highestEnemyIndexReached = 0;



    public bool customEntrance = false;
    public float customEntranceSpeed;

    void Awake(){
        customEntranceSpeed = 2;
        if (instance != null)
            Destroy(gameObject);
        else
        {
            transform.parent = null; //Do this so that we can keep it organized in the hierarchy,
            //but during actual gameplay it needs to be a root object so that we can make it 
            //don't destroy on load
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
	
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadDataIntoPlayer(Unit player){
        player.maxHp = playerMaxHP;
        player.maxMp = playerMaxMP;
        player.maxSp = playerMaxSP;
        player.mpRegenRate = playerRegenRate;
        player.shieldGeneralCooldownTime = shieldGeneralCooldownTime;
        player.shieldBreakCooldownTime = shieldBreakCooldownTime;
        player.shieldOverpoweredCooldownTime = shieldOverpoweredCooldownTime;
        player.InitializeUnit();
    }

    public void LoadDataIntoPlayer(Unit player, Entrance entrance)
    {
        LoadDataIntoPlayer(player);
        entrance.preEntranceTime = preEntranceTime;
        entrance.postEntranceTime = postEntranceTime;
        entrance.speed = entranceSpeed;
    }

}
