using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoalIcon : MonoBehaviour
{
    //[SerializeField] GameObject objectiveStrip;
    [SerializeField] Sprite unfulfilledSprite;
    [SerializeField] Sprite fulfilledSprite;

    Image myImage;

    public void SetAsFulfilled(){
        myImage = GetComponent<Image>();
        myImage.overrideSprite = fulfilledSprite;
        }
    public void SetAsUnfulfilled(){
        myImage = GetComponent<Image>();
        myImage.overrideSprite = null;
        }

    public void SetIcons(){
        myImage = GetComponent<Image>();
        
        unfulfilledSprite = GetComponentInParent<ObjectiveStrip>().defaultSprite;
        fulfilledSprite = GetComponentInParent<ObjectiveStrip>().fulfilledSprite;

        myImage.sprite = unfulfilledSprite;
        //Debug.Log("SetIcons was called");
    }

    void Awake(){
        
    }

    void Start() {
        
        SetIcons();
    }

    void Update()
    {
        
    }
}
