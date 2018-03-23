using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    private PlayerInput playerInput;
    private Unit player;
	// Use this for initialization
	void Start () {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.timeScale > Mathf.Epsilon){
            if(Input.GetKeyDown(KeyCode.J) && player.currentTurnType == TurnType.Attack ){
                playerInput.fireballButton1();
            }
            else if (Input.GetKeyDown(KeyCode.K) && player.currentTurnType == TurnType.Attack)
            {
                playerInput.fireballButton2();
            }
            else if (Input.GetKeyDown(KeyCode.L) && player.currentTurnType == TurnType.Attack)
            {
                playerInput.fireballButton3();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                playerInput.shieldButton1Press();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                playerInput.shieldButton2Press();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                playerInput.shieldButton3Press();
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                playerInput.shieldButton1Release();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                playerInput.shieldButton2Release();
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                playerInput.shieldButton3Release();
            }
            else if (Input.GetKeyUp(KeyCode.P))
            {
                if(Time.timeScale >= Mathf.Epsilon){
                    GameplayController.instance.PauseGame();
                }
            }
	    }
        //Controls available when the game is paused
        else{ 
            if (Input.GetKeyUp(KeyCode.P))
            {
                    GameplayController.instance.UnpauseGame();


            }
        }
    }
}
