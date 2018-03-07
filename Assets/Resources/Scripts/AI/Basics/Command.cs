using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Command {
    public Action_ action;
    public float waitTime; //How much time is waited after the action is done, and the next action begins.

}
