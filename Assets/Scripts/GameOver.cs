using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOver
{
    private static bool IsThereSurplusProduction(){    //currently only checks for energy and material production
        int surplusProduction = 0;
        foreach (KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> kvp in GameManager.gm.variables.variables){
            if (kvp.Key == GlobalVariableEnum.Energy || kvp.Key == GlobalVariableEnum.Material){
                surplusProduction += kvp.Value.GetSurplus();
            }   
        }
        Debug.Log($"Total Surplus: {surplusProduction}");
        return surplusProduction > 0;
    }

    public static void TheGameIsOver(){
        Debug.Log("game over, man, game over!");
    }

    public static bool CheckGameOver(){   //this should be called after the 'end turn' button is pressed and all the production is calculated
        if (!IsThereSurplusProduction()){
            TheGameIsOver();
        }
        return !IsThereSurplusProduction();
    }

}

public class WinConditionChecker
{
    public void CheckWinCondition()
    {

    }
}
