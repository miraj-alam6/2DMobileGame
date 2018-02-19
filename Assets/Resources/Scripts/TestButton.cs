using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour {
    public string debugMessage ="The button works";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void debugTest(){
        Debug.Log(debugMessage);
    }
}
