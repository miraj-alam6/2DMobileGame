using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Thinker/BasicOffenseThinker")]
public class BasicOffenseThinker : Thinker {
    //Set all of these public fields in the inspector. Make a different descriptive public
    //field name for each combo the thinker has
    public Combo risingAction; 
    public Combo weakBarrage; 
    public Combo weakMediumBarrage;
    public Combo waitFullTurn; 


    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
        if(controller.unit.mp > 5 || Utility.RandomPercentageDecision(80)){
            
            float f = Random.Range(1,3);
            if(f > 2.5){
                controller.AddToActionQueue(risingAction.commands);    
            }
            else if(f > 1.5){
                controller.AddToActionQueue(weakMediumBarrage.commands);    

            }
            else if (f > 0.0f){
                controller.AddToActionQueue(weakBarrage.commands);    

            }

        }
        else{
            controller.AddToActionQueue(waitFullTurn.commands);
        }
        controller.thinking = false;
    }
}
