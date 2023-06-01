using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   float pickUpHeight = 0.5f;
    Vector3 mouseOffset;    //the mouse position relative to the object when it is clicked

    private Vector3 GetMouseWorldPosition(){
        float cameraDistance = Mathf.Abs(Camera.main.GetComponent<Transform>().position.z);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, cameraDistance);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDown() {
        mouseOffset = new Vector3(
            gameObject.transform.position.x - GetMouseWorldPosition().x,
            0f,
            gameObject.transform.position.z - GetMouseWorldPosition().z);
        
        GetComponent<Rigidbody>().position += Vector3.up * pickUpHeight;
    }

    private void OnMouseDrag() {
        GetComponent<Rigidbody>().position = new Vector3(GetMouseWorldPosition().x, pickUpHeight, GetMouseWorldPosition().z) 
                                                + mouseOffset;

    }

    private void OnMouseUp() {
        GetComponent<Rigidbody>().position -= Vector3.up * pickUpHeight;
    }
}
