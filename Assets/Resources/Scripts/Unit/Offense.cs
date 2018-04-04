
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offense : MonoBehaviour, IPooledObject {

    public int numberValue; //1,2, or 3
    public int damage;
    public float mpCost;
    //When two instances of this class collide with each other, make one of them only handle the collision
    private bool collisionBeingHandled;
    [SerializeField]
    private int _unitID; //Id of which unit "owns" the fireball, thus can't get hit by it.

    public ParticleSystem movingParticleSystem;
    public ParticleSystem succcessParticleSystem;
    public ParticleSystem failParticleSystem;

    public SpriteRenderer spriteRenderer;
    public Mover mover;

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
//        print("Palms are sweaty");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagNames.Fireball) && !collisionBeingHandled){
            collision.GetComponent<Offense>().collisionBeingHandled = true;
//            print("Collided with another offense"); //This message should only show up once
        }
    }

    public void InitProperties(Direction facing, int unitID){
        GameplayController.instance.addFireball(this);
        spriteRenderer.enabled = true;
        if(facing == Direction.Right){
            spriteRenderer.flipX = false;
        }
        else if (facing == Direction.Left)
        {
            spriteRenderer.flipX = true;
        }
        mover.direction = facing;
        mover.InitDirection();
        this.unitID = unitID;

    }
    public void Explode(bool success){
        if (success)
        {
            if(!GameplayController.instance.enabled){
                Utility.AddToParticleSystemContainer(succcessParticleSystem.transform);
            }
            movingParticleSystem.Stop();
            succcessParticleSystem.Play();
        }
        else{
            if (!GameplayController.instance.enabled)
            {
                Utility.AddToParticleSystemContainer(failParticleSystem.transform);
            }
            movingParticleSystem.Stop();
            failParticleSystem.Play(); 

        }

        DestroyFromGame();
    }
    public void DestroyFromGame(){
        if(GameplayController.instance.useObjectPooling){
            spriteRenderer.enabled = false;
            mover.SetCurrentSpeed(0.0f);
            Invoke("Deactivate",0.5f);
        }
        else{
            GameplayController.instance.removeFireball(this);
            Utility.AddToParticleSystemContainer(movingParticleSystem.transform);
            Destroy(movingParticleSystem.gameObject,1f); //fix this from being hard coded later
            Destroy(this.gameObject);
        }
    }
    public void Deactivate(){
        GameplayController.instance.removeFireball(this);
        this.gameObject.SetActive(false);
    }

    public void OnObjectSpawn(){
        //TODO: get rid of the immediately next TODO as well as this entire function probably. Made it work in 
        //a different way
        //TODO: need to spawn a moving particles as well as destroy the children on it if there are
        //any by default so that there are no duplicates.
        if (GameplayController.instance.useObjectPooling)
        {


        }
        else{
            
        }
    }



}
