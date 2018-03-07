using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Action/IdleAction")]
public class IdleAction : Action_ {
    public override void Act(AIController controller){
        ; //This function literally does nothing. Possibly make it that it
        //sets a state on the unit to indicate it is idle. Keep this class here
        //for that purpose at least. And the combo system needs a way to just wait as well.
    }
}
