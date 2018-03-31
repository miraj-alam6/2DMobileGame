using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Todo: don't need this in its own class, temporary. Move the coroutine into Unity possibly.
//Or keep it here so that Unit is less of a bloated class
public class Entrance : MonoBehaviour {
    public float preEntranceTime; //Bosses will take longer to arrive
    public float postEntranceTime; //Bosses will have a longer pause before battle starts
    public float speed; //Speed that the unit falls down onto the platform
    public Transform startTransform;
    public Transform endTransform;
    private bool beginCoroutine = false;
    public bool entranceDone = false;

	// Use this for initialization
	void Start () {
        entranceDone = false;
        //BeginEntrance();
    }
    public void BeginEntrance(){
       
        StartCoroutine("EnterArena");

    }
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator EnterArena(){
        float animationProgress = 0.0f;
        transform.position = Vector3.Lerp(startTransform.position, endTransform.position, animationProgress);
        yield return new WaitForSeconds(preEntranceTime);
        while(animationProgress < 1.0f){
//            print("Doing it");
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, animationProgress);
            animationProgress += speed*Time.deltaTime;
            yield return null; //resume coroutine in next frame
        }
        transform.position = Vector3.Lerp(startTransform.position, endTransform.position, 1.0f);
        yield return new WaitForSeconds(postEntranceTime);
        entranceDone = true;
        GameplayController.instance.FinishSpawning(this.tag);
    }
}
