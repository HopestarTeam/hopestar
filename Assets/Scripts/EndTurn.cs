using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NativeClassExtensions;

public class EndTurn : MonoBehaviour
{
    [SerializeField] Grid myGrid;


    GlobalVariables variables;

    int listSize;

    // Start is called before the first frame update
    void Start()
    {
        listSize = myGrid.count;
        variables = GameManager.gm.variables;
    }

    // Update is called once per frame
    void Update()
    {
       // On mouseclick, call the EndTurnFunction() 
    }

    bool CheckUpkeep()
    {
        foreach (KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> variable in GameManager.gm.variables.variables)
        {
            if(variable.Key != GlobalVariableEnum.CO2 && variable.Value.upkeep < 0)
            {
                return false;
            }
        }
        return true;
    }
    public void EndTurnFunction()
    {
        if(!CheckUpkeep())
        {
            Debug.Log("Upkeep fugd, no turn end for you :)");
            return;
        }
        
        GlobalVariableEnum[] enums = (GlobalVariableEnum[])Enum.GetValues(typeof(GlobalVariableEnum));
        
        foreach(GlobalVariableEnum current in EnumExtensions.GlobalVariableEnumAsArray())
        {
            if(current == GlobalVariableEnum.CO2)continue;
            GlobalVariables.ResourceVariable currentVar = variables.variables[current];
            currentVar.upkeep = 0;
            variables.variables[current] = currentVar;
        }

        for (int i = 0; i < listSize; i++)
        {

            Tile current = myGrid[i].GetComponent<Tile>();
            if(current == null)
            {
                Debug.Log("No tile component found");
                continue;
            }

            if (current.cardHandler != null)
            {
                // Fetch card from current, activate this later.
                current.cardHandler.ResolveCard();
                current.cardHandler.placedThisTurn = false;
            }

        }

        // Here the script should set Raw Resources, Food, Energy, ConsumerGoods and IndustryGoods to upkeep.
        // Turned this into a loop to make it more flexible
        foreach(GlobalVariableEnum current in EnumExtensions.GlobalVariableEnumAsArray())
        {
            if(current == GlobalVariableEnum.CO2)continue;
            GlobalVariables.ResourceVariable currentvar = variables.variables[current];
            currentvar.production = variables.variables[current].upkeep;
        }
        //variables.RawResources = variables.RawResourcesUpkeep;
        //variables.Food = variables.FoodUpkeep;
        //variables.Energy = variables.EnergyUpkeep;
        //variables.ConsumerGoods = variables.ConsumerGoodsUpkeep;
        //variables.IndustryGoods = variables.IndustryGoodsUpkeep;

        // Here should come a popup window with stats and a close button.
        GameManager.gm.menuManager.ShowInfoScreen(GameManager.gm.variables);
        GameManager.gm.menuManager.UpdateHud();

    }

    private void OnMouseDown() {
        EndTurnFunction();
    }
}
