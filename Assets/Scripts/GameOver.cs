using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool IsThereSurplusProduction(){    //currently only checks for energy and material production
        int surplusProduction = 0;
        foreach (KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> kvp in GameManager.gm.variables.variables){
            if (kvp.Key == GlobalVariableEnum.Energy || kvp.Key == GlobalVariableEnum.Material){
                surplusProduction += kvp.Value.GetSurplus();
            }   
        }
        return surplusProduction > 0;
    }

    private void TheGameIsOver(){
        Debug.Log("game over, man, game over!");
    }

    public void CheckGameOver(){   //this should be called after the 'end turn' button is pressed and all the production is calculated
        if (!IsThereSurplusProduction()){
            TheGameIsOver();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //CheckGameOver();    //testing only, remove this line later
    }
}
