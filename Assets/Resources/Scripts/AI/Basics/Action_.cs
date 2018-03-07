using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The name Action has a _ after it in order to avoid any name conflict with System.Action
public abstract class Action_ : ScriptableObject
{
    public abstract void Act(AIController controller);
}