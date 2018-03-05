using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/StopShieldAction")]
public class StopShieldAction : Action_ {
    
    public override void Act(AIController controller){
        controller.unit.stopShield();
    }
}
