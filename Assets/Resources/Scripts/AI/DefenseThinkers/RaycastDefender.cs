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
    private Command[] commandArr = new Command[1];
    private int currentShieldLevel; //0 means no shield

    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
       Utility.CoroutineStarter(ThinkRoutine(controller));
    }


    //If the logic is too complicated, make this method call a coroutine. 
    public IEnumerator ThinkRoutine(AIController controller)
    {
        RaycastHit2D hitFireball =  controller.CheckRayCast(lookDistance);
        yield return null;

        if (controller.unit.currentShield)
        {
            currentShieldLevel = controller.unit.currentShield.numberValue;
        }
        else
        {
            currentShieldLevel = 0;
        }

        if(hitFireball){
            Debug.Log("Got here hit fireball");
            Offense fireball = hitFireball.collider.GetComponent<Offense>();
           
            if (currentShieldLevel == 0 || currentShieldLevel != fireball.numberValue)
            {
                switch(fireball.numberValue){
                    case 1:
                        commandArr[0] = shield1;
                        break;
                    case 2:
                        commandArr[0] = shield2;
                        break;
                    case 3:
                        commandArr[0] = shield3;
                        break;
                }
                controller.AddToActionQueue(commandArr);

            }

        }
        else{
            if(currentShieldLevel != 0){
                commandArr[0] = stopShield;
                controller.AddToActionQueue(commandArr);       
            }
        }
     
        controller.thinking = false;
    }


}
