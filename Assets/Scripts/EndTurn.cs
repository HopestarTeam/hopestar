using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
   [SerializeField] Grid myGrid;
   [SerializeField] CardHandler myCardHandler;

     int listSize;

    // Start is called before the first frame update
    void Start()
    {
        listSize = myGrid.count;
       
    }

    // Update is called once per frame
    void Update()
    {
       // On mouseclick, call the EndTurnFunction() 
    }

    void EndTurnFunction()
    {
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
            else
            {
                Debug.Log("There is no card");
            }

        }
     
        Debug.Log(GameManager.gm.variables.CO2);
        Debug.Log(GameManager.gm.variables.CitizenUnrest);
        Debug.Log(GameManager.gm.variables.RawResources);
        Debug.Log(GameManager.gm.variables.Food);
        Debug.Log(GameManager.gm.variables.Energy);
        Debug.Log(GameManager.gm.variables.ConsumerGoods);
        Debug.Log(GameManager.gm.variables.IndustryGoods);
        
        // Here should come a popup window with stats and a close button.


        // Here the script should clear Raw Resources, Food, Energy, ConsumerGoods and IndustryGoods.
        GameManager.gm.variables.RawResources = 0f;
        GameManager.gm.variables.Food = 0f;
        GameManager.gm.variables.Energy = 0f;
        GameManager.gm.variables.ConsumerGoods = 0f;
        GameManager.gm.variables.IndustryGoods = 0f;

    

    }

    private void OnMouseDown() {
        EndTurnFunction();
    }
}
