using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
   [SerializeField] Grid myGrid;
  int listSize;

    // Start is called before the first frame update
    void Start()
    {
        listSize = myGrid.count;
        EndTurnFunction();  //this should be called when the player pressess the End Turn Button
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
            
        }
    }
}
