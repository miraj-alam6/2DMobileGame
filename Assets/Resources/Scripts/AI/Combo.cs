using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Combo")]
public class Combo : ScriptableObject {
    public List<Action_> actions;

}
