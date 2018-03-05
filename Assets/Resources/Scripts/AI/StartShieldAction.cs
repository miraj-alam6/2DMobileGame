using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/StartShieldAction")]
public class StartShieldAction : Action_ {
    public int number;
    public override void Act(AIController controller){
        controller.unit.startShield(number);
    }
}
