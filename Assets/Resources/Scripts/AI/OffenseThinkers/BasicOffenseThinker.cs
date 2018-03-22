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

    public int mpThresholdToWait;
    public int chanceOfNotWaiting;


    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
        Utility.CoroutineStarter(ThinkRoutine(controller));
    }
    //If the logic is too complicated, make this method call a coroutine. 
    public IEnumerator ThinkRoutine(AIController controller)
    {
//        Debug.Log("Got here");
        if(controller.unit.mp > mpThresholdToWait || Utility.RandomPercentageDecision(chanceOfNotWaiting)){
          
            int i = Random.Range(1,8); // Be careful This only returns integers!
           
            yield return null;
            if(i > 5){
  //              Debug.Log("Think rising" + i );
                controller.AddToActionQueue(risingAction.commands);    
            }
            else if(i > 3){
//                Debug.Log("Think weakmedium" + i);
                controller.AddToActionQueue(weakMediumBarrage.commands);    

            }
            else if (i > 0){
//                Debug.Log("Think weak" + i);
                controller.AddToActionQueue(weakBarrage.commands);    
            }
            else{
//                Debug.Log("Think" + i);
            }

        }
        else{
            controller.AddToActionQueue(waitFullTurn.commands);
        }
        controller.thinking = false;
    }
}
