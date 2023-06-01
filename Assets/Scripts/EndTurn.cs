using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
   int listSize = 11; // this should be the size of the grid list

    // Start is called before the first frame update
    void Start()
    {
        EndTurnFunction(); //this should be called when the player pressess the End Turn Button
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndTurnFunction()
    {
        for (int i =0; i < listSize; i++)
        {
            int currentItem = i;  //this should be the current item in the list
            // Debug.Log(currentItem);
        }
    }
}
