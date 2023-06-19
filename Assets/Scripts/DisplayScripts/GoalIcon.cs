using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoalIcon : MonoBehaviour
{
    [SerializeField] GameObject objectiveStrip;
    [SerializeField] Sprite unfulfilledSprite;
    [SerializeField] Sprite fulfilledSprite;

    Image myImage;

    public void SetAsFulfilled(){myImage.overrideSprite = fulfilledSprite;}
    public void SetAsUnfulfilled(){myImage.overrideSprite = null;}

    /*public void SetIcons(Sprite unfulfilled, Sprite fulfilled){
        unfulfilledSprite = unfulfilled;
        fulfilledSprite = fulfilled;

        myImage.sprite = unfulfilledSprite;
    }*/

    void Awake(){

        myImage = GetComponent<Image>();

        unfulfilledSprite = objectiveStrip.GetComponent<ObjectiveStrip>().defaultSprite;
        fulfilledSprite = objectiveStrip.GetComponent<ObjectiveStrip>().fulfilledSprite;

        myImage.sprite = unfulfilledSprite;
    }

    void Update()
    {

    }
}
