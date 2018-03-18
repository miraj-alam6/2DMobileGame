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
	void FixedUpdate () {
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
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerInput.shieldButton1Release();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerInput.shieldButton2Release();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerInput.shieldButton3Release();
        }

	}
}
