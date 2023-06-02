using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    float pickUpHeight = 0.5f;
    Vector3 mouseOffset;    //the mouse position relative to the object when it is clicked
    GameObject target;

    public void SetTarget(GameObject theTarget){
        target = theTarget;
    }
    public void SetTargetToNull(){
        target = null;
    }

    private Vector3 GetMouseWorldPosition(){
        float cameraDistance = Mathf.Abs(Camera.main.GetComponent<Transform>().position.z);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, cameraDistance);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Awake() {
        SetTargetToNull();
    }

    private void OnMouseDown() {
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
        GetComponent<Rigidbody>().position -= Vector3.up * pickUpHeight;
        Cursor.visible = true;
        gameObject.tag = "Card";
    }

    private void Update() {
        Debug.Log(target);
    }
}
