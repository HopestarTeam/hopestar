using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{   
    float halfWidth;
    float halfHeight;
    float currentCardCenterX;
    float currentCardCenterZ;
    GameObject currentCard;
    bool oldValue = false;  //we use this to check when the CardIsAbove valuse changes

    bool isFree = true;
    public bool SlotIsFree(){return isFree;}
    public void SetOccupied(){isFree = false;}
    public void SetFree(){isFree = true;}
        
    

    private bool CardIsAbove(){
        if (GameObject.FindGameObjectWithTag("CurrentCard") != null && isFree){
            currentCard = GameObject.FindGameObjectWithTag("CurrentCard");
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
        halfWidth = GetComponent<Collider>().bounds.size.x / 2;
        halfHeight = GetComponent<Collider>().bounds.size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {   
        if (CardIsAbove()){
            oldValue = true;
            GameObject.FindGameObjectWithTag("CurrentCard").GetComponent<DragAndDrop>().SetTarget(gameObject);
        }

        if (!CardIsAbove() && oldValue && GameObject.FindGameObjectWithTag("CurrentCard") != null){
            oldValue = false;
            GameObject.FindGameObjectWithTag("CurrentCard").GetComponent<DragAndDrop>().SetTargetToNull();
        }

        //this is for testing, comment out if not needed
        if (CardIsAbove()){GetComponent<Renderer>().material.SetColor("_Color", Color.red);}
        if (!CardIsAbove()){GetComponent<Renderer>().material.SetColor("_Color", new Color(255f, 218f, 218f, 255));}
    }
}
