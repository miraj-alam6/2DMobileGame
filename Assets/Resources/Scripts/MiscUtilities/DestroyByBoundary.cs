using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is needed in the game for fireballs that will be shot while there is no enemy in position
//aka the downtime between each enemy. 
public class DestroyByBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(TagNames.Fireball)){
            collision.GetComponent<Offense>().DestroyFromGame();
                        }
        else{
            Destroy(collision.gameObject);
        }
    }
}
