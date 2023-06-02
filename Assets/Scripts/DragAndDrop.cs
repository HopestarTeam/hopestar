using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    float pickUpHeight = 0.5f;
    Vector3 mouseOffset;    //the mouse position relative to the object when it is clicked
    Vector3 initialPosition;    //this will be removed once cards are stored in the card pool
 
    GameObject target;  //the card slot where it will be placed
    public void SetTarget(GameObject theTarget){target = theTarget;}    
    public void SetTargetToNull(){target = null;}

    private void MoveCard(Vector3 targetPosition){   //this function should lerp in the final version
        GetComponent<Rigidbody>().position = new Vector3(
                                                    targetPosition.x, 
                                                    GetComponent<Rigidbody>().position.y - pickUpHeight, 
                                                    targetPosition.z);
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
        if (target != null){target.GetComponent<CardSlot>().SetFree();}

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
            target.GetComponent<CardSlot>().SetOccupied();
        }

        gameObject.tag = "Card";
    }

    private void Update() {
        Debug.Log(target);
    }
}
