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
            tempTurnSwitch(); 
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
