using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defense : MonoBehaviour {

    public int numberValue; //1,2, or 3
    public float drainRate =0.5f;
   // private float numberTimesDrain;
    public Unit unit;
	// Use this for initialization
	void Start () {
         
	}
	

    //Unit will call this so that defense no it exists.
    //This also calculate numberValue * drainRate and stores the value.
    public void Initialize(Unit unit){
        this.unit = unit;
        //this.drainRate = drainRate;
        //this.numberTimesDrain = numberValue * drainRate; //no point to doing it like this. Just
        //make drainRate its own thing independent of numberValue
    }
    public void destroySelf(){
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
        if(unit){
            print("got here");
            unit.addMP(-(drainRate *Time.deltaTime));
        }
	}
}
