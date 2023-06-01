using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{   
    float halfWidth;
    float halfHeight;
    float currentCardCenterX;
    float currentCardCenterZ;


    private bool cardIsAbove(){
        if (GameObject.FindGameObjectWithTag("CurrentCard") != null){
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

    GameObject currentCard;
    // Start is called before the first frame update
    void Start()
    {
        halfWidth = GetComponent<Collider>().bounds.size.x / 2;
        halfHeight = GetComponent<Collider>().bounds.size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {   
        //this is for testing, comment out if not needed
        if (cardIsAbove()){GetComponent<Renderer>().material.SetColor("_Color", Color.red);}
        if (!cardIsAbove()){GetComponent<Renderer>().material.SetColor("_Color", new Color(255f, 218f, 218f, 255));}
    }
}
