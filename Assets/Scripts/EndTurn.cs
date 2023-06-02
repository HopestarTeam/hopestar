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
            int currentItem = i;  //this should be the current item in the list
           // Debug.Log(currentItem);

            Tile current = myGrid[i].GetComponent<Tile>();
            if(current == null)
            {
                Debug.Log("No tile component found");
                continue;
            }
            if (current.card == null)
            {
                Debug.Log("There is no card");
            }
           
           if (current.card != null)
           {
                // myCardHandler = current.card; // Fetch card from current, activate this later.
                myCardHandler.ResolveCard();
           }

        }
    }

    private void OnMouseDown() {
        EndTurnFunction();
    }
}
