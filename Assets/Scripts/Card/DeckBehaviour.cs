using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardSO[] cardSOs;
    
    //Deck Display stuff, feel free to delete when doing proper deck view
    [SerializeField] float deckMatrixMaxWidth = 2, widthSpace = 5, depthSpace = 10;
    int row;

    // Start is called before the first frame update
    void Start()
    {
        row = -1;
        for(int i = 0; i < cardSOs.Length; i++)
        {
            if(i%deckMatrixMaxWidth == 0)
                row++;
            GameObject newCard = Instantiate(cardPrefab,
                        transform.position + new Vector3(widthSpace*(i%deckMatrixMaxWidth),0,-depthSpace*row),
                        transform.rotation,
                        transform);
            newCard.GetComponent<CardHandler>().properties = cardSOs[i];
        }
    }

    public void AddCards(CardSO[] addedCards)
    {
        List<CardSO> tmpList = new List<CardSO>();
        foreach(CardSO cardSO in cardSOs)
        {
            tmpList.Add(cardSO);
            foreach(CardSO add in addedCards)
            {
                if(add != cardSO)
                    tmpList.Add(add);
            }
        }
        cardSOs = tmpList.ToArray();
    }

    public void RemoveCards(CardSO[] removedCards)
    {
        List<CardSO> tmpList = new List<CardSO>();
        foreach(CardSO cardSO in cardSOs)
        {
            tmpList.Add(cardSO);
            foreach(CardSO rmv in removedCards)
            {
                if(rmv == cardSO)
                {
                    tmpList.Remove(cardSO);
                    break;
                }
            }
        }
        cardSOs = tmpList.ToArray();
    }
}
