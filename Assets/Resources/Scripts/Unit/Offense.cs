
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offense : MonoBehaviour {

    public int numberValue; //1,2, or 3
    public int damage;
    //When two instances of this class collide with each other, make one of them only handle the collision
    private bool collisionBeingHandled;
    [SerializeField]
    private int _unitID; //Id of which unit "owns" the fireball, thus can't get hit by it.

    public ParticleSystem movingParticleSystem;
    public ParticleSystem succcessParticleSystem;
    public ParticleSystem failParticleSystem;

    public int unitID
    {
        get
        {
            return _unitID;
        }
        set
        {
            _unitID = value;
        }
    }
	// Use this for initialization
	void Start () {
        GameplayController.instance.addFireball(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagNames.Fireball) && !collisionBeingHandled){
            collision.GetComponent<Offense>().collisionBeingHandled = true;
            print("Collided with another offense"); //This message should only show up once
        }
    }


    public void Explode(bool success){
        if (success)
        {
            Utility.AddToParticleSystemContainer(succcessParticleSystem.transform);
            succcessParticleSystem.Play();
        }
        else{
            Utility.AddToParticleSystemContainer(failParticleSystem.transform);
            failParticleSystem.Play(); 
        }

        DestroyFromGame();
    }
    public void DestroyFromGame(){
        GameplayController.instance.removeFireball(this);
        //movingParticleSystem.transform.SetParent(GameplayController.instance.particleSystemHolder);
        Utility.AddToParticleSystemContainer(movingParticleSystem.transform);
        Destroy(movingParticleSystem.gameObject,1f); //fix this from being hard coded later
        Destroy(this.gameObject);

    }



}
