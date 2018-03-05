using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Action/WaitAction")]
public class IdleAction : Action_ {
    public float waitTime;
    public override void Act(AIController controller){
        controller.waitTime = waitTime;
    }
}
