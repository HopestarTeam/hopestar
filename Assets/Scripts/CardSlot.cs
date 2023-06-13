using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{   
    float halfWidth;
    float halfHeight;
    float currentCardCenterX;
    float currentCardCenterZ;
    Tile thisTile;
    GameObject currentCard;
    //bool oldValue = false;  //we use this to check when the CardIsAbove valuse changes : not needed anymore maybe

    GameObject slottedCard;
    public bool IsFree(){return slottedCard == null;}
    public void AddCard(GameObject card)
    {
        slottedCard = card;
        thisTile.cardHandler = card.GetComponent<CardHandler>();
    }
    public void RemoveCard()
    {
        slottedCard = null;
        thisTile.cardHandler = null;
    }
        
    

    private bool CardIsAbove(){
        if (currentCard != null && IsFree()){
            currentCardCenterX = currentCard.transform.position.x;
            currentCardCenterZ = currentCard.transform.position.z;
            return (
               gameObject.transform.position.x - halfWidth <= currentCardCenterX 
            && gameObject.transform.position.x + halfWidth > currentCardCenterX
            && gameObject.transform.position.z - halfHeight <= currentCardCenterZ
            && gameObject.transform.position.z + halfHeight > currentCardCenterZ
            );
        }
        else{return false;}
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
        thisTile = GetComponent<Tile>();
        halfWidth = GetComponent<Collider>().bounds.size.x / 2;
        halfHeight = GetComponent<Collider>().bounds.size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {   
        //if(true)
        //{
        //    if(currentCard == null)
        //        currentCard = GameObject.FindGameObjectWithTag("CurrentCard");
        //    
        //    if (CardIsAbove()){
        //        oldValue = true;
        //        currentCard.GetComponent<DragAndDrop>().SetTarget(gameObject);
        //    }
        //    else if (oldValue && currentCard != null){
        //        oldValue = false;
        //        currentCard.GetComponent<DragAndDrop>().SetTargetToNull();
        //    }
        //}
    }
}
