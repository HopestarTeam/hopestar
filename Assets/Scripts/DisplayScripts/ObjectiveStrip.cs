using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveStrip : MonoBehaviour
{    
    [SerializeField] GameObject icon;
    [SerializeField] GameObject iconGrid;

    string name;
    int target;
    [HideInInspector] public Sprite defaultSprite;
    [HideInInspector] public Sprite fulfilledSprite;

    TextMeshProUGUI nameOfStrip;

    public void SetNameAs(string name){
        nameOfStrip = GetComponentInChildren<TextMeshProUGUI>();
        nameOfStrip.text = name;
    }
    
    public void SetPropertiesAs(Objectives.ResourceObjective obj){
        SetNameAs(obj.nameEnum.ToString());
        target = obj.target;
        defaultSprite = obj.unfulfilledSprite;
        fulfilledSprite = obj.fulfilledSprite;
    }

    private void DisplayObjectiveIcons(){
        for (int i = 0; i < target; i++){
            GameObject newIcon = Instantiate(icon);
            newIcon.transform.SetParent(iconGrid.transform);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
       DisplayObjectiveIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
