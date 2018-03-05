using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These are helper classes 
public class Utility {

    //takes parent transform, returns child transform
    public static Transform GetChildByTag(Transform parent, string childTag){
        for (int i = 0; i < parent.childCount;i++){
            if(parent.GetChild(i).gameObject.CompareTag(childTag)){
                return parent.GetChild(i).transform;
            }
        }
        return null;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
