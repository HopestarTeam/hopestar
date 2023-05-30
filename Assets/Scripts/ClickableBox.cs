using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBox : Clickable
{
    public override void OnClick()
    {
        Debug.Log($"Clicked {gameObject.name}");
    }

    public override void OnHoverEnter()
    {
        Debug.Log($"Entering hover over {gameObject.name}");
    }

    public override void OnHoverExit()
    {
        Debug.Log($"Exiting hover over {gameObject.name}");
    }

    public override void OnHoverStay()
    {
    }
}
