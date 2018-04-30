using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class will deal with resizing the UI of the vitals. This never affects height, only width.
public class VitalsUI : MonoBehaviour {
    //Need both the Image and the Transform. Getting the Image just in case I wanna dynamically change
    //the color of any of the bars.
    //Need the Transform to change the size, AKA absolutely necessary. 
    public Image HPOuterBar;
    public Image HPBackgroundBar;
    public Image HPBar;
    public Image MPOuterBar;
    public Image MPBackgroundBar;
    public Image MPBar;
    public Image SPBar;
    public Image SPBackgroundBar;
    public Image SPOuterBar;
    public RectTransform HPOuterBarTransform;
    public RectTransform HPBackgroundBarTransform;
    public RectTransform HPBarTransform;
    public RectTransform MPOuterBarTransform;
    public RectTransform MPBackgroundBarTransform;
    public RectTransform MPBarTransform;
    public RectTransform SPBarTransform;
    public RectTransform SPBackgroundBarTransform;
    public CanvasGroup canvasGroup;

    public int scale = 20; //This is for MP and HP, because each point has a width equivalent
                      // Use this for initialization
    public int outlineOffset = 20;//This is for MP and HP, because each point has a width equivalent
                                  // Use this for initialization
    //public Vector2 SPBarDimensions = new Vector2(45, 85);

    void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
        hide();
    }
    void Start () {
        
	}

    public void InitializeVitals(float maxHP, float maxMP){
        //HPOuterBarTransform.rect.Set();
        if(!GameplayController.instance.entranceOn){
            show();
        }
        HPOuterBarTransform.sizeDelta = new Vector2(maxHP * scale+outlineOffset, HPOuterBarTransform.sizeDelta.y);
        HPBackgroundBarTransform.sizeDelta = new Vector2(maxHP * scale, HPBackgroundBarTransform.sizeDelta.y);
        HPBarTransform.sizeDelta = new Vector2(maxHP * scale, HPBarTransform.sizeDelta.y);

        MPOuterBarTransform.sizeDelta = new Vector2(maxMP * scale + outlineOffset, MPOuterBarTransform.sizeDelta.y);
        MPBackgroundBarTransform.sizeDelta = new Vector2(maxMP * scale, MPBackgroundBarTransform.sizeDelta.y);
        MPBarTransform.sizeDelta = new Vector2(maxMP * scale, MPBarTransform.sizeDelta.y);

    }

 
    public void UpdateVitals(VitalName vital, float current, float max){
        RectTransform vitalToUpdate = null;
        switch(vital){
            case VitalName.HP:
                vitalToUpdate = HPBarTransform;
                UpdateHorizontal(HPBarTransform,current,max);
                break;
            case VitalName.MP:
                vitalToUpdate = MPBarTransform;
                UpdateHorizontal(MPBarTransform, current, max);
                break;
            case VitalName.SP:
                vitalToUpdate = SPBarTransform;
                UpdateVerticalPercentage(SPBarTransform, SPBackgroundBarTransform, current, max);
                break;

        }

    }
    public void UpdateHorizontal(RectTransform rT, float current, float max){
        rT.sizeDelta = new Vector2(current * scale, rT.sizeDelta.y);
    }
    //In current design of game, this is not used because max shield appearance will never change.
    public void UpdateVertical(RectTransform rT,float current, float max){
        rT.sizeDelta = new Vector2(rT.sizeDelta.x, current * scale);
    }

    //No matter the max value, the width will be a percentage, thus no need for the
    //scale variable at top. We need backgroundRT for this as well because it gives a baseline
    //of what the max width is.
    public void UpdateHorizontalPercentage(RectTransform rT, RectTransform backgroundRT, float current, float max)
    {
        
        rT.sizeDelta = new Vector2(((float)current / max) * backgroundRT.sizeDelta.x, rT.sizeDelta.y);
    }
    //This is wht is used for SP update right now
    public void UpdateVerticalPercentage(RectTransform rT,RectTransform backgroundRT, float current, float max)
    {
        rT.sizeDelta = new Vector2( rT.sizeDelta.x, ((float)current / max) * backgroundRT.sizeDelta.y);
    }
                                  
    public void hide(){
        canvasGroup.alpha = 0;
    }
    public void show(){
        canvasGroup.alpha = 1f;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
