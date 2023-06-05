using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    float pickUpHeight = 0.5f;
    Vector3 mouseOffset;    //the mouse position relative to the object when it is clicked
    Vector3 initialPosition;    //this will be removed once cards are stored in the card pool

    Vector3 startPosition;  //for lerping
    Vector3 endPosition;
    float lerpDuration = 0.2f;  //play around with this value until it feels fine
    float lerpElapsedTime = 0f;
    bool moving = false;
 
    GameObject target;  //the card slot where it will be placed
    public void SetTarget(GameObject theTarget){target = theTarget;}    
    public void SetTargetToNull(){target = null;}

    public void ExecuteCardFunctions(){
        //this is where you call the card functions
        Debug.Log("card functions were called");
    }

    private void MoveCard(Vector3 targetPosition){   //this function should lerp in the final version
        endPosition = new Vector3(
                                targetPosition.x, 
                                GetComponent<Rigidbody>().position.y - pickUpHeight, 
                                targetPosition.z);
        startPosition = GetComponent<Rigidbody>().position;
        lerpElapsedTime = 0;
        moving = true;
    }

    private void MoveCardWithLerp(){
        if (moving){
            lerpElapsedTime += Time.deltaTime;
            float percentComplete = Mathf.Min(lerpElapsedTime / lerpDuration, 1f);

            GetComponent<Rigidbody>().position = Vector3.Lerp(startPosition, endPosition, percentComplete);
            if (percentComplete == 1f){moving = false;}
        }
    }

    private Vector3 GetMouseWorldPosition(){
        float cameraDistance = Mathf.Abs(Camera.main.GetComponent<Transform>().position.z);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, cameraDistance);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Awake() {
        SetTargetToNull();
        initialPosition = gameObject.transform.position;    //this will be removed once cards are stored in the card pool
    }

    private void OnMouseDown() {
        if (target != null){target.GetComponent<CardSlot>().RemoveCard();}

        mouseOffset = new Vector3(
            gameObject.transform.position.x - GetMouseWorldPosition().x,
            0f,
            gameObject.transform.position.z - GetMouseWorldPosition().z);
        
        GetComponent<Rigidbody>().position += Vector3.up * pickUpHeight;
        Cursor.visible = false;
        gameObject.tag = "CurrentCard";
    }

    private void OnMouseDrag() {
        GetComponent<Rigidbody>().position = new Vector3(
                                                    GetMouseWorldPosition().x, 
                                                    GetComponent<Rigidbody>().position.y, 
                                                    GetMouseWorldPosition().z) 
                                                + mouseOffset;

    }

    private void OnMouseUp() {
        Cursor.visible = true;
        if (target == null){
            MoveCard(initialPosition);
            //the card goes back to the card pool
        }
        if (target != null){
            MoveCard(target.transform.position);
            target.GetComponent<CardSlot>().AddCard(gameObject);
        }

        gameObject.tag = "Card";
    }

    private void Update() {
        MoveCardWithLerp();
    }
}
