using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : ScriptableObject {
    public float cost;
    Image image;
    Text text;

    public Effect[] effects;

    public void useCard(){

        foreach(Effect e in effects){
            e.applyEffect();
        }
    }
}
