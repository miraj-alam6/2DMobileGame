using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//specific combos are not created by extending Combo class, instead they are just customized
//in terms of which actions are in the array for the differet Combo assets that are created.
[CreateAssetMenu(menuName = "EnemyAI/Combo")]
public class Combo : ScriptableObject {
    public Command[] commands;
    public float mpHeuristic; //a prediction of how much MP it will cost to complete this combo, it
    //considers the mp regen in the  wait between commands which is why it is a heuristic. probably
    //hard coded per combo asset.

}
