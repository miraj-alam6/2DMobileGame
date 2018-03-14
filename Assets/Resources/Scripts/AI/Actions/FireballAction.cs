using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Action/FireballAction")]
public class FireballAction : Action_ {
    public int number;
    public override void Act(AIController controller){
        controller.unit.shootFireball(number);
//        Debug.Log("Shot a fireball");
    }
}
