using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] public CardSO[] cardSOs;
    public List<GameObject> cardObjects;

    DeckScroll scrollScript;
    
    //Deck Display stuff, feel free to delete when doing proper deck view
    int row;

    // Start is called before the first frame update
    void Start()
    {
        scrollScript = GetComponent<DeckScroll>();
        for(int i = 0; i < cardSOs.Length; i++)
        {
            InstantiateCard(cardSOs[i]);
        }
        scrollScript.ArrangeCards();
    }

    public void InstantiateCard(CardSO newCardProperties)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);
        newCard.GetComponent<CardHandler>().properties = newCardProperties;
        cardObjects.Add(newCard);
    }
    public void InstantiateCard(GameObject newCardObject)
    {
        int index = cardObjects.Count;
        if(cardObjects.Contains(newCardObject))
        {
            index = cardObjects.IndexOf(newCardObject);
            cardObjects.Remove(newCardObject);
        }
        GameObject newCard = Instantiate(newCardObject, transform);
        cardObjects.Insert(index, newCard);
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
                {
                    tmpList.Add(add);
                    InstantiateCard(add);
                }
            }
        }
        cardSOs = tmpList.ToArray();
        scrollScript.ArrangeCards();
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
                    //Destroy the card object
                    break;
                }
            }
        }
        cardSOs = tmpList.ToArray();
        scrollScript.ArrangeCards();
    }
}
