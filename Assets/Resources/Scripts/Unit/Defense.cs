using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defense : MonoBehaviour {

    public int numberValue; //1,2, or 3
    public float mpCost;
    public float drainRate =0.5f;
    public SpriteRenderer spriteRenderer;

   // private float numberTimesDrain;
    public Unit unit;
	// Use this for initialization
	void Start () {
         
	}
	

    //Unit will call this so that defense no it exists.
    //This also calculate numberValue * drainRate and stores the value.
    public void Initialize(Unit unit){
        this.unit = unit;
        spriteRenderer.flipX = (unit.facing == Direction.Left) ? true : false; 
         if(unit.facing == Direction.Right){
            
        }
        //this.drainRate = drainRate;
        //this.numberTimesDrain = numberValue * drainRate; //no point to doing it like this. Just
        //make drainRate its own thing independent of numberValue
    }
    public void destroySelf(){
        if(GameplayController.instance.useObjectPooling){
            this.transform.parent = null;
            this.gameObject.SetActive(false);
        }
        else{
            Destroy(this.gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if(unit){
//            print("got here");
            unit.addMP(-(drainRate *Time.deltaTime));
        }
        else
        {
            destroySelf();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.Fireball))
        {
            Offense fireball = collision.GetComponent<Offense>();
            if(fireball!=null && (compareToFireball(fireball) >= 0)){
                fireball.Explode(false);
            }
            else{
                //Have specific action for by how much the number the shield was trumped
                //for how much shield gauge is broken. For now simply stop the shield.
                unit.stopShield();
            }
        }
    }

    private int compareToFireball(Offense fireball){
        return numberValue - fireball.numberValue;
    }
}
