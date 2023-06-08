using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clicker : MonoBehaviour
{
    Camera cam;

    IClickable clickable, lastClickable;

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
        if (!GameManager.gm.menuManager.OnElement && CursorWithinScreen())
        {

            Ray ray = GetRay();
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 0.01f);

            RaycastHit hit;
            clickable = null;
            if (Physics.Raycast(ray, out hit, distance, Physics.AllLayers))
            {
                clickable = hit.collider.GetComponent(typeof(IClickable)) as IClickable;
            }

            if (clickable == null)
            {
                if (clickable != lastClickable) lastClickable.OnHoverExit();
            }
            else
            {
                if (clickable != lastClickable)
                {
                    clickable.OnHoverExit();
                    if (clickable != null) clickable.OnHoverEnter();
                }
                else
                {
                    if (clickable != null) clickable.OnHoverStay();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (clickable != null) clickable.OnClick();
                }

                if(Input.GetMouseButton(0))
                {
                    if (clickable != null) clickable.OnClickHold();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if(clickable != null) clickable.OnClickRelease();
                }
            }


            lastClickable = clickable;
        }
        else
        {
            if(lastClickable != null)
            {
                lastClickable.OnHoverExit();
            }
            lastClickable = null;
        }
    }

    bool CursorWithinScreen()
    {
        return (Input.mousePosition.x >= 0|| Input.mousePosition.y <= 0 || Input.mousePosition.x <= Screen.width || Input.mousePosition.y <= Screen.height);
    }
}
