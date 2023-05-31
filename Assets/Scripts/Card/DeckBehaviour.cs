using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardSO[] cardSOs;
    
    //Deck Display stuff, feel free to delete when doing proper deck view
    [SerializeField] int deckMatrixMaxWidth = 2, widthSpace = 5, depthSpace = 10;
    int row;

    // Start is called before the first frame update
    void Start()
    {
        row = -1;
        for(int i = 0; i < cardSOs.Length; i++)
        {
            if(i%deckMatrixMaxWidth == 0)
                row++;
            GameObject newCard = Instantiate(cardPrefab, transform.position + new Vector3(widthSpace*(i%deckMatrixMaxWidth),-depthSpace*row,0), transform.rotation, transform);
            newCard.GetComponent<CardHandler>().properties = cardSOs[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
