using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These are helper classes 
public class Utility:MonoBehaviour {


    //takes parent transform, returns child transform
    public static Transform GetChildByTag(Transform parent, string childTag){
        for (int i = 0; i < parent.childCount;i++){
            if(parent.GetChild(i).gameObject.CompareTag(childTag)){
                return parent.GetChild(i).transform;
            }
        }
        return null;
    }


    public static bool RandomPercentageDecision(int percent){
        float n = Random.value * 100;
        if(n < percent){
            return true;
        }
        return false;
    }

    public static bool RandomDecisionGeneral(int number, int max)
    {
        float n = Random.value * max;
        if (n < number)
        {
            return true;
        }
        return false;
    }
    public static void AddToParticleSystemContainer(Transform transform){
        transform.SetParent(GameplayController.instance.particleSystemHolder);
    }

    public static void CoroutineStarter(IEnumerator coroutine)
    {
        //Can't call StartCoroutine without an instance monobehavior
        //so just use GameplayController since that is always guaranteed to exist
        GameplayController.instance.StartCoroutine(coroutine); 
    }
}
