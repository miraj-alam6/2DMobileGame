using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Thinker/InfrequentAttacker")]
public class InfrequentAttacker : Thinker {
    //Set all of these public fields in the inspector. Make a different descriptive public
    //field name for each combo the thinker has
    public Combo infrequentWeakAttackCombo;  

    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
        controller.AddToActionQueue(infrequentWeakAttackCombo.commands);
        controller.thinking = false;
    }
}
