using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    [SerializeField] Grid myGrid;
    [SerializeField] CardHandler myCardHandler;

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

    public void EndTurnFunction()
    {
        variables.ConsumerGoodsUpkeep = 0f;
        variables.EnergyUpkeep = 0f;
        variables.FoodUpkeep = 0f;
        variables.RawResourcesUpkeep = 0f;
        variables.IndustryGoodsUpkeep = 0f;

        for (int i = 0; i < listSize; i++)
        {
            //int currentItem = i;  //this should be the current item in the list ; you can just use i instead
            // Debug.Log(currentItem);

            Tile current = myGrid[i].GetComponent<Tile>();
            if(current == null)
            {
                Debug.Log("No tile component found");
                continue;
            }

            if (current.cardHandler != null)
            {
                 // Fetch card from current, activate this later.
                 myCardHandler = current.cardHandler;
                 myCardHandler.ResolveCard();
            }

        }
     
     /*
        Debug.Log(GameManager.gm.variables.CO2);
        Debug.Log(GameManager.gm.variables.CitizenUnrest);
        Debug.Log(GameManager.gm.variables.RawResources);
        Debug.Log(GameManager.gm.variables.Food);
        Debug.Log(GameManager.gm.variables.Energy);
        Debug.Log(GameManager.gm.variables.ConsumerGoods);
        Debug.Log(GameManager.gm.variables.IndustryGoods);
     */   


        // Here the script should set Raw Resources, Food, Energy, ConsumerGoods and IndustryGoods to upkeep.
        variables.RawResources = variables.RawResourcesUpkeep;
        variables.Food = variables.FoodUpkeep;
        variables.Energy = variables.EnergyUpkeep;
        variables.ConsumerGoods = variables.ConsumerGoodsUpkeep;
        variables.IndustryGoods = variables.IndustryGoodsUpkeep;

        // Here should come a popup window with stats and a close button.
        GameManager.gm.menuManager.ShowInfoScreen(GameManager.gm.variables);
        GameManager.gm.menuManager.UpdateHud();

    }

    private void OnMouseDown() {
        EndTurnFunction();
    }
}
