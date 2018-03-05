using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    
    public State attackState;
    public State defenseState;
    public Queue<Action_> actionQueue;
    public bool doingAction = false;
    public bool thinking = false;
    //Wait time is a field that dynamically changes due to actions. It basically a cooldown
    public float waitTime = 0; //waiting is an AI only action, so it is here instead of in Unit
    public Action_ tempAction; //Just a single action for testing.
    public Action_ tempWaitAction; //Just a single wait for testing.

    public Unit unit;
	// Use this for initialization
	void Start () {
        actionQueue = new Queue<Action_>();
        unit = GetComponent<Unit>(); //will use this to know if it is attacking or defending.
        //Unit does not have handle to AI, because unit is also used by the player
        GameplayController.instance.currentEnemy = unit;
	}
	
	// Update is called once per frame
	void Update () {
        if(!doingAction){
            if(actionQueue.Count > 0){
                if(waitTime <=0){
                    actionQueue.Dequeue().Act(this);
                }
                else{
                    waitTime -= Time.deltaTime;
                }
            }
            else{
                if(!thinking && unit.currentTurnType == TurnType.Attack){
                    thinking = true;
                    tempAttackThink();
                    //Need to Think here, and fill up the actionQueue,
                    //There is some randomness to how far the AI will think.
                    //Think will be a coroutine. Once coroutine is done, it will
                    //turn the boolean off. The AI can continue thinking as it is acting as
                    //well. But it only ever starts to think once the queue is empty. Thinking
                    //itself has randomness to how long it last, thus it may keep adding actions to 
                    //the queue. 
                    //Thinking also depends on the state that the AI is in currently.
                }
       
            }
        }
	}

    public void tempAttackThink(){
        actionQueue.Enqueue(tempAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction); actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction); actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction);
        actionQueue.Enqueue(tempWaitAction); actionQueue.Enqueue(tempWaitAction);
        thinking = false;
    }

    //Game Manager calls this when time is up
    public void PreventAttackAction(){
        
    }




}
