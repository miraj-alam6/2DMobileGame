using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/EnemyData")]
public class EnemyData : ScriptableObject {
    public Thinker AttackThinker;
    public Thinker DefenseThinker;
    public float MaxHP;
    public float MaxMP;
    public float HPRegenRate;
    public float MPRegenRate;
    public float MaxSP;
    public float ShieldBreakCooldown = 1.0f;
    public float ShieldGeneralCooldown = 0.1f;
    public float ShieldOverpoweredCooldown = 0.1f;
    public RuntimeAnimatorController animatorController;
    public Sprite sprite;
    public Color color;
    //Ray Distance is set by the thinker
}
