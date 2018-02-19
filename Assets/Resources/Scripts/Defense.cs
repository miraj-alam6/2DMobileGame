using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defense : MonoBehaviour {

    public int numberValue; //1,2, or 3
    public float drainRate =0.5f;
    private float numberTimesDrain;
    public Unit unit;
	// Use this for initialization
	void Start () {
         
	}
	

    //Don't need to pass in numberValue, because that is already existing from
    //the prefab.
    public void Initialize(int drainRate, Unit unit){
        this.unit = unit;
        this.drainRate = drainRate;
        this.numberTimesDrain = numberValue * drainRate;
    }
	// Update is called once per frame
	void Update () {
        if(unit){
            unit.addMP(-(numberTimesDrain *Time.deltaTime));
        }
	}
}
