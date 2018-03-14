﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Action/StartShieldAction")]
public class StartShieldAction : Action_ {
    public int number;
    public override void Act(AIController controller){
        if(controller.unit.canUseShield){
            controller.unit.startShield(number);
        }
    }
}
