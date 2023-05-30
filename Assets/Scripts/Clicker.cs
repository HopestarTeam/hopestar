using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    Camera cam;

    Clickable lastClickable;

    [SerializeField] float distance = 100;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray GetRay()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = cam.nearClipPlane;
            Vector3 WorldPosition = cam.ScreenToWorldPoint(mousePosition * -1);
    
            return cam.ScreenPointToRay(mousePosition);
        }

        //TODO: Make this more readable
        if(!GameManager.gm.menuManager.inMenu)
        {
            
            Ray ray = GetRay();
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 0.01f);
    
            RaycastHit hit;
            Clickable clickable = null;
            if (Physics.Raycast(ray, out hit, distance, Physics.AllLayers))
            {
                clickable = hit.collider.GetComponent<Clickable>();
            }
            if (clickable != lastClickable)
            {
                if (clickable != null) clickable.SendMessage("OnHoverEnter", SendMessageOptions.DontRequireReceiver);
                if (lastClickable != null) lastClickable.SendMessage("OnHoverExit", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                if (clickable != null) clickable.SendMessage("OnHoverStay", SendMessageOptions.DontRequireReceiver);
            }
    
            if (Input.GetMouseButtonDown(0))
            {
                if (clickable != null) clickable.SendMessage("OnClick", hit.point, SendMessageOptions.DontRequireReceiver);
            }
            lastClickable = clickable;
        }
    }
}
