using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] GameObject resourceManager;    //this is for testing, the values hould be fetched from the GameManager instead !!!!!
    int resourceProduction = 0;
    int resourceSpent = 0;
    int resourceInUpkeep = 0;

    [SerializeField] GameObject theIcon;
    List<GameObject> listOfIcons = new List<GameObject>();

    private void GetResourceValues(){   //modify this function to get the right resource type   !!!!!!
        resourceProduction = Mathf.Min(GameManager.gm.variables.variables[GlobalVariableEnum.Energy].production, 30);
        resourceSpent = GameManager.gm.variables.variables[GlobalVariableEnum.Energy].spent;
        resourceInUpkeep = GameManager.gm.variables.variables[GlobalVariableEnum.Energy].upkeep;
    }

    private void UpdateDisplay(){    
        //we check if the values have changed, if they have not, we don't need to refresh the display
        if (resourceProduction !=  GameManager.gm.variables.variables[GlobalVariableEnum.Energy].production
         || resourceSpent != GameManager.gm.variables.variables[GlobalVariableEnum.Energy].spent
         || resourceInUpkeep != GameManager.gm.variables.variables[GlobalVariableEnum.Energy].upkeep
        ){
            GetResourceValues();
            ClearDisplay();
            DisplayIcons();
        }
        
    }

    private void DisplayIcons(){
        //the production icons
        for (int i = 0; i < resourceProduction; i++){
            GameObject newIcon = GameObject.Instantiate<GameObject>(theIcon);
            newIcon.transform.SetParent(transform);
            listOfIcons.Add(newIcon);
        }

        //the upkeep icons
        for (int i = 0; i < resourceInUpkeep; i++){
            listOfIcons[i].GetComponent<ResourceIcon>().SetAsUpkeep();
        }

        //the spent icons
        if (resourceSpent > 0){
            for (int i = listOfIcons.Count-1; i >= listOfIcons.Count-resourceSpent; i--){
                listOfIcons[i].GetComponent<ResourceIcon>().SetAsSpent();
            }
        }
    }

    private void ClearDisplay(){
        if (listOfIcons.Count != 0) {
            foreach (GameObject icon in listOfIcons){Destroy(icon);} 
            listOfIcons.Clear();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GetResourceValues();
        DisplayIcons();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();  
    }
}
