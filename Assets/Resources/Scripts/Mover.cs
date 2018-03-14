using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public Direction direction;
    [SerializeField]
    private Rigidbody2D rb2D;
	// Use this for initialization
	void Start () {
       

       
	}
    public void InitDirection(){
        rb2D = GetComponent<Rigidbody2D>();
        if(direction == Direction.Right){
            if(rb2D){
                rb2D.velocity = new Vector2(speed, 0);
            }
           
        }
        else if(direction == Direction.Left){
            if(rb2D){
                rb2D.velocity = new Vector2(-speed, 0);
            }
            Transform spriteTransform = null;
            print("HYA");

            if(gameObject.CompareTag(TagNames.Fireball)){
                spriteTransform = Utility.GetChildByTag(transform, TagNames.FireballSprite);
            }
            else if(gameObject.CompareTag(TagNames.Shield)){
                print("HYAA");
                spriteTransform = Utility.GetChildByTag(transform, TagNames.ShieldSprite);
            }
            //spriteTransform.localScale = Vector3.Scale(transform.localScale, 
            //                                           new Vector3(-1, 1, 1));  //this just sets the scale
            ////doesn't multiply
            //Multiplies one vector by another's corresponding. 
            if(spriteTransform){
                print("HYAAA " +spriteTransform.gameObject);
                spriteTransform.localScale = new Vector3(-1, 1, 1);
                print(spriteTransform.localScale);
                spriteTransform.localScale = new Vector3(-1,1,1);
                spriteTransform.localScale = 
                    Vector3.Scale(spriteTransform.localScale, new Vector3(-1, 1, 1));
                spriteTransform.localScale = new Vector3(-1, 1, 1);
                spriteTransform.localScale = new Vector3(-1, 1, 1);
                spriteTransform.localScale = new Vector3(-1, 1, 1);
            }
            //spriteTransform.localScale =
                               //spriteTransform.localScale *  new Vector3(-1, 1, 1); 
                           //spriteTransform.localScale.x, 
                           //spriteTransform.localScale.y, 
                           //spriteTransform.localScale.z);
        }
        else if(direction == Direction.Up){
            if(rb2D){
                rb2D.velocity = new Vector2(0, speed);
            }
        }
        else if (direction == Direction.Down)
        {
            if (rb2D){
                rb2D.velocity = new Vector2(0, -speed);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
