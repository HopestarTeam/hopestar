using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool IsThereSurplusProduction(){    //currently only checks for energy and material production
        int surplusProduction = 0;
        foreach (var keyValuePair in GameManager.gm.variables.variables){
            if (keyValuePair.Key == GlobalVariableEnum.Energy || keyValuePair.Key == GlobalVariableEnum.Material){
                surplusProduction += keyValuePair.Value.GetSurplus();
            }   
        }
        return surplusProduction > 0;
    }

    private void TheGameIsOver(){
        Debug.Log("game over, man, game over!");
    }

    private void CheckGameOver(){   //this should be called after the 'end turn' button is pressed and all the production is calculated
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

    }
}
