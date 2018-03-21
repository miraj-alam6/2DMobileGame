using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour {

    public LayerMask layerMask;
    public float rayDistance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit2D rch2D = CheckRayCast();

        if(rch2D){
            Debug.Log("Hit this object "+rch2D.collider.gameObject);
        }
	}

    public RaycastHit2D CheckRayCast(){
        Debug.DrawRay(transform.position, (transform.right * -1) * rayDistance, Color.red);
        return Physics2D.Raycast(transform.position, transform.right * -1, rayDistance, layerMask);
    }
}
