using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Thinker : ScriptableObject
{
    //If the logic is too complicated for a particular Thinker, make this method call a coroutine. 
    public abstract void Think(AIController controller);

}