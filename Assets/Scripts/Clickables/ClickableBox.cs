using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBox : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}");
    }

    public void OnHoverEnter()
    {
        Debug.Log($"Entering hover over {gameObject.name}");
    }

    public void OnHoverExit()
    {
        Debug.Log($"Exiting hover over {gameObject.name}");
    }

    public void OnHoverStay()
    {
    }

    public void OnClickHold()
    {

    }

    public void OnClickRelease()
    {
        
    }
}
