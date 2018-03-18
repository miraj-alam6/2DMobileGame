using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is associated with the UI. The UI has several buttons which have events that use functions
from this class.
*/
public class PlayerInput : MonoBehaviour {
    Unit playerUnit;
    public Command[] cm;
	// Use this for initialization
	void Start () {
        playerUnit = GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fireballButton1(){
        playerUnit.shootFireball(1);
    }
    public void fireballButton2()
    {
        playerUnit.shootFireball(2);
    }
    public void fireballButton3()
    {
        playerUnit.shootFireball(3);
    }
    public void shieldButton1Press()
    {
        playerUnit.startShield(1);
    }
    public void shieldButton2Press()
    {
        playerUnit.startShield(2);
    }
    public void shieldButton3Press()
    {
        playerUnit.startShield(3);
    }
    public void shieldButton1Release()
    {
        playerUnit.stopShield();
    }
    public void shieldButton2Release()
    {
        playerUnit.stopShield();
    }
    public void shieldButton3Release()
    {
        playerUnit.stopShield();
    }

}
