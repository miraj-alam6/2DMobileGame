using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Thinker/SingleAttacker")]
public class SingleAttacker : Thinker {
    //Set all of these public fields in the inspector. Make a different descriptive public
    //field name for each combo the thinker has
    public Combo combo;  

    //If the logic is too complicated, make this method call a coroutine. 
    public override void Think(AIController controller)
    {
        controller.AddToActionQueue(combo.commands);
        controller.thinking = false;
    }
}
