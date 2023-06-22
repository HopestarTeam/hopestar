using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveStrip : MonoBehaviour
{
    [SerializeField] GameObject icon;
    [SerializeField] GameObject iconGrid;

    GlobalVariableEnum theName;
    int target;
    public Sprite defaultSprite;
    public Sprite fulfilledSprite;

    int surplusProduction;

    TextMeshProUGUI nameOfStrip;
    List<GameObject> listOfIcons = new List<GameObject>();

    public void SetNameAs(string stripName){
        nameOfStrip = GetComponentInChildren<TextMeshProUGUI>();
        nameOfStrip.text = stripName;
    }
    
    public void SetPropertiesAs(Objectives.ResourceObjective obj){
        theName = obj.nameEnum;
        SetNameAs(theName.ToString());
        target = obj.target;
        defaultSprite = obj.unfulfilledSprite;
        fulfilledSprite = obj.fulfilledSprite;
    }

    private void DisplayObjectiveIcons(){
        for (int i = 0; i < target; i++){
            GameObject newIcon = Instantiate(icon);
            newIcon.transform.SetParent(iconGrid.transform);
            listOfIcons.Add(newIcon);
        }
    }
    private void UpdateObjectiveIcons(){
        surplusProduction = Mathf.Min(GameManager.gm.variables.variables[theName].GetSurplus(), target);
        if (listOfIcons != null){
            foreach (var icon in listOfIcons){icon.GetComponent<GoalIcon>().SetAsUnfulfilled();}
            for (int i = 0; i < surplusProduction; i++){
                listOfIcons[i].GetComponent<GoalIcon>().SetAsFulfilled();
            }
        }  
    }



    // Start is called before the first frame update
    void Start()
    {
       DisplayObjectiveIcons();
       UpdateObjectiveIcons();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateObjectiveIcons();
    }
}
