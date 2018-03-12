using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Thinker/RisingAttacker")]
public class RisingAttacker : Thinker {
    //Set all of these public fields in the inspector. Make a different descriptive public
    //field name for each combo the thinker has
    public Combo risingAction; 

    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
        controller.AddToActionQueue(risingAction.commands);
        controller.thinking = false;
    }
}
