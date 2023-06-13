using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    const string cardTag = "Card";
    const string currentCardTag = "CurrentCard";
    float pickUpHeight = 0.2f;
    Vector3 mouseOffset;    //the mouse position relative to the object when it is clicked
    Vector3 initialPosition;    //this will be removed once cards are stored in the card pool

    //Components
    Rigidbody rb;

    //Variables related to lerping the position
    Vector3 startPosition;  //for lerping
    Vector3 endPosition;
    float lerpDuration = 0.2f;  //play around with this value until it feels fine
    float lerpElapsedTime = 0f;
    bool moving = false;
    bool dragged = false;
 
    GameObject target;  //the card slot where it will be placed
    CardHandler handler;
    Grid currentGrid;
    public void SetTarget(GameObject theTarget){target = theTarget;}    
    public void SetTargetToNull(){target = null;}

    //A coroutine for lerping the cards(In the same manner as MoveCardWithLerp)
    private IEnumerator MoveCardWithLerp(Vector3 EndPosition, float duration, Action EndAction)
    {
        Vector3 initialPosition = transform.position;
        moving = true;
        //x/y = x*(1/y) so we only need to do the division just once 
        float OneDividedByDuration = 1/duration;

        //Go through this each frame until the time's elapsed
        for(float elapsed = 0; elapsed <= duration; elapsed += Time.deltaTime)
        {
            rb.position = Vector3.Lerp(initialPosition, EndPosition, elapsed * OneDividedByDuration);
            yield return null;
        }

        EndAction();
        Debug.Log("Stopped Moving");
        moving = false;
    }
    private void MoveCard(Vector3 targetPosition){   //this function should lerp in the final version
        endPosition = new Vector3(
                                targetPosition.x, 
                                targetPosition.y + 0.1f, 
                                targetPosition.z);
        startPosition = GetComponent<Rigidbody>().position;
        lerpElapsedTime = 0;
        moving = true;
    }

    private void MoveCardWithLerp(){
        if (moving){
            lerpElapsedTime += Time.deltaTime;
            float percentComplete = Mathf.Min(lerpElapsedTime / lerpDuration, 1f);

            rb.position = Vector3.Lerp(startPosition, endPosition, percentComplete);
            if (percentComplete == 1f){moving = false;}
        }
    }

    private Vector3 GetMouseWorldPosition(){
        float cameraDistance = Mathf.Abs(Camera.main.transform.position.y);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, cameraDistance);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Awake() {
        SetTargetToNull(); //Unnecessary to do this here, as uninitialized targets are null by default
        handler = GetComponent<CardHandler>();
        rb = GetComponent<Rigidbody>();
        initialPosition = gameObject.transform.position;    //this will be removed once cards are stored in the card pool
    }

    public void OnMouseDown() {
        if(!moving && !GameManager.gm.menuManager.OnElement){
            GameManager.gm.tileMaterialSetter.GrayIncompatible(handler.properties);
            if (target != null) // = if card is in a slot
            {
                target.GetComponent<CardSlot>().RemoveCard();
            }
            else
            {
                Instantiate(gameObject, transform.position, transform.rotation, handler.deck.transform);
            }

            

            mouseOffset = new Vector3(
                                gameObject.transform.position.x - GetMouseWorldPosition().x,
                                0f,
                                gameObject.transform.position.z - GetMouseWorldPosition().z);
        
            GetComponent<Rigidbody>().position += Vector3.up * pickUpHeight;
            Cursor.visible = false;
            gameObject.tag = currentCardTag;
            dragged = true;
        }
        
    }

    public void OnMouseDrag() {
        if(!moving && dragged){
            target = TryGetSlotBelow();
            rb.position = new Vector3(
                                        GetMouseWorldPosition().x, 
                                        rb.position.y, 
                                        GetMouseWorldPosition().z) 
                                        + mouseOffset;
        }
    }

    public void OnMouseUp() {
        if(!moving && dragged)
        {
            GameManager.gm.tileMaterialSetter.ReturnColor();
            Cursor.visible = true;
            if (target == null){
                StartCoroutine(MoveCardWithLerp(initialPosition, lerpDuration, () => Destroy(gameObject)));
                //MoveCard(initialPosition);
                //Destroy(gameObject,lerpDuration*0.9f);
                //the card goes back to the card pool
            }
            else{
                 // if enough resources to place the card and tile accepts
                if(handler.CheckCard() && target.GetComponent<Tile>().IsCompatibleWith(handler.properties))
                {
                    handler.RunCosts();
                    StartCoroutine(MoveCardWithLerp(target.transform.position, lerpDuration, () => {}));
                    //MoveCard(target.transform.position);
                    target.GetComponent<CardSlot>().AddCard(gameObject);
                    GameManager.gm.menuManager.UpdateHud();
                }
                else
                {
                    StartCoroutine(MoveCardWithLerp(initialPosition, lerpDuration, () => Destroy(gameObject)));
                    //MoveCard(initialPosition);
                    //Destroy(gameObject,lerpDuration*0.9f);
                }
            }

            gameObject.tag = cardTag;
            dragged = false;
        }
        
    }

    GameObject TryGetSlotBelow()
    {
        Ray ray = new Ray(transform.position,Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10f,LayerMask.GetMask("CardSlot")))
        {
            if(hit.collider != null)
            {
                Tile targetTile = hit.collider.GetComponent<Tile>();
                if(targetTile != null && targetTile.cardHandler == null)
                {
                    Debug.Log("Slot found!");
                    return hit.collider.gameObject;
                }
            }
        }
        Debug.Log("Slot Not Found");
        return null;
    }

    //private void Update() {
    //    MoveCardWithLerp();
    //}
}
