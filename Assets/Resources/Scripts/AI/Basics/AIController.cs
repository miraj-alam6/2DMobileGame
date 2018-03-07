using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    
    public Thinker attackThinker;
    public Thinker defenseThinker;
    public Queue<Command> commandQueue;
    public Queue<Action_> actionQueue;
    public bool doingAction = false;
    public bool thinking = false;
    //Wait time is a field that dynamically changes due to actions. It basically a cooldown
    public float waitTime = 0; //waiting is an AI only action, so it is here instead of in Unit
    public Action_ tempAction; //Just a single action for testing.
    public Action_ tempWaitAction; //Just a single wait for testing.
    private Command lastCommand; //made this here so that Update doesn't keep creating and destroying
    //objects. Check if that actually can be a problem for performance.

    public Unit unit;
	// Use this for initialization
	void Start () {
        actionQueue = new Queue<Action_>();
        commandQueue = new Queue<Command>();
        unit = GetComponent<Unit>(); //will use this to know if it is attacking or defending.
        //Unit does not have handle to AI, because unit is also used by the player
        GameplayController.instance.currentEnemy = unit;
	}
	
	// Update is called once per frame
	void Update () {
        if(!doingAction){
            if(commandQueue.Count > 0){
                if(waitTime <=0){
                    lastCommand = commandQueue.Dequeue();
                    waitTime = lastCommand.waitTime;
                    lastCommand.action.Act(this);
                }
                else{
                    waitTime -= Time.deltaTime;
                }
            }
            else{
                if(!thinking && unit.currentTurnType == TurnType.Attack){
                    thinking = true;
                    attackThinker.Think(this);
                    // tempAttackThink();
                    //TODO: need to implement complex thinking that requires being a coroutine.
                }
                //This is defense thinking
                else{
                    
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

    public void AddToActionQueue(Command[] commands){
        foreach(Command cm in commands){
            commandQueue.Enqueue(cm);    
        }

    }
    public void ClearActionQueue(){
        actionQueue.Clear();
    }

    //Game Manager calls this when time is up
    public void PreventAttackAction(){
        
    }




}
