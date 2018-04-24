using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugModeController : MonoBehaviour {

    public Text currentPlayerHPDataText;
    public Text currentPlayerMPDataText;
    public Text currentPlayerRegenDataText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M)){
            killCurrentEnemy();
        }
	}

    public void increaseDataHP(){
        GameDataController.instance.playerMaxHP++;
        updateVitals();
    }
    public void decreaseDataHP()
    {
        GameDataController.instance.playerMaxHP--;
        updateVitals();
    }
    public void increaseDataMP()
    {
        GameDataController.instance.playerMaxMP++;
        updateVitals();
    }
    public void decreaseDataMP()
    {
        GameDataController.instance.playerMaxMP--;
        updateVitals();
    }
    public void increaseMPRegen()
    {
        //Didn't just add or subtract 0.1f because of floating point imprecision
        GameDataController.instance.playerRegenRate = Mathf.RoundToInt(GameDataController.instance.playerRegenRate * 10 + 1)/10.0f;
        updateVitals();
    }
    public void decreaseMPRegen()
    {
        //Yellow
        GameDataController.instance.playerRegenRate = Mathf.RoundToInt(GameDataController.instance.playerRegenRate * 10 - 1) / 10.0f;
        updateVitals();
    }

    public void increaseDataSP()
    {
        GameDataController.instance.playerMaxSP++;
        updateVitals();
    }
    public void decreaseDataSP()
    {
        GameDataController.instance.playerMaxSP--;
        updateVitals();
    }

    public void updateVitals(){
        GameDataController.instance.LoadDataIntoPlayer(GameplayController.instance.player);
        currentPlayerHPDataText.text = ""+GameDataController.instance.playerMaxHP;
        currentPlayerMPDataText.text = "" + GameDataController.instance.playerMaxMP;
        currentPlayerRegenDataText.text = "" + GameDataController.instance.playerRegenRate;
    }

    public void reinitializePlayer(){
        GameDataController.instance.LoadDataIntoPlayer(GameplayController.instance.player);
    }
    public void killCurrentEnemy(){
        GameplayController.instance.currentEnemy.addHP(-9000);
    }
}
