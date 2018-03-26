using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Thinker/RaycastDefender")]
public class RaycastDefender : Thinker {
    //Set all of these public fields in the inspector. Make a different descriptive public
    //field name for each combo the thinker has
  //  public Combo weakDefenseCombo;
    public float lookDistance;
    public Command shield1;
    public Command shield2;
    public Command shield3;
    public Command stopShield;
    public Command idle;
    public bool smartShield; //will continue holding down shield instead of restarting shield if hit
    //by a constant barrage of fireballs.
    public float waitInterval; //How much to wait between each think iteration, the smaller it is
    //the quicker the enemy will react by changing shield

    //this was private before, but means if we modify its size you need to make it public,
    //and then change size there and then make it private again because by the size usually
    //remainds at what was set when the asset was created rather than what you change it to.
    public Command[] commandArr = new Command[2];
    private int currentShieldLevel; //0 means no shield


    //IMPORTANT: this doesn't work because this is a child class,
    //and only base class can do it.
    public void Awake(){
        Debug.Log("Awaken");
        if(smartShield){
            shield1.waitTime = Constants.TurnTime;
            shield2.waitTime = Constants.TurnTime;
            shield3.waitTime = Constants.TurnTime;
        }
    }

    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
        if (smartShield)
        {
            shield1.waitTime = Constants.TurnTime;
            shield2.waitTime = Constants.TurnTime;
            shield3.waitTime = Constants.TurnTime;
        }
       Utility.CoroutineStarter(ThinkRoutine(controller));
    }



    //If the logic is too complicated, make this method call a coroutine. 
    public IEnumerator ThinkRoutine(AIController controller)
    {
        while(controller.thinking){
            GameplayController.instance.iterationDebugger++;
            if (controller.unit.currentTurnType == TurnType.Defense)
            {
                
                RaycastHit2D hitFireball = controller.CheckRayCast(lookDistance);
                if(!smartShield){
                    yield return null;
                }

                if (controller.unit.currentShield)
                {
                    currentShieldLevel = controller.unit.currentShield.numberValue;
                }
                else
                {
                    currentShieldLevel = 0;
                }

                if (hitFireball)
                {
    //                Debug.Log("Got here hit fireball");
                    Offense fireball = hitFireball.collider.GetComponent<Offense>();

                    if (currentShieldLevel == 0 || currentShieldLevel != fireball.numberValue)
                    {
                        switch (fireball.numberValue)
                        {
                            case 1:
                                commandArr[1] = shield1;
                                break;
                            case 2:
                                commandArr[1] = shield2;
                                break;
                            case 3:
                                commandArr[1] = shield3;
                                break;
                        }
                        if(smartShield){
                            controller.ClearActionQueue();
                        }
                        controller.AddSingleToActionQueue(commandArr[1]); //only pass in a shield command

                    }
                    else if (currentShieldLevel == fireball.numberValue){
                        if(smartShield){
                            controller.ExtendWait(0.1f,0.1f);
                        }
                    }

                }
                else
                {
                    if (currentShieldLevel != 0)
                    {
                        if(smartShield){
                            yield return new WaitForSeconds(0.1f);
                            commandArr[0] = idle;
                            commandArr[1] = stopShield;
                            controller.AddToActionQueue(commandArr);
                        }
                        else{
                            controller.AddSingleToActionQueue(stopShield);
                        }
                    }
                }

                //Defense thinkers will continue keep running because they need to be able to raycast and
                //extend actions. Thus comment on the following.
                //controller.thinking = false;

            }
            GameplayController.instance.iterationDebugger--;
            yield return new WaitForSeconds(waitInterval);
        }
    }


}
