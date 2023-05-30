using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    public void OnHoverEnter();
    public void OnHoverStay();
    public void OnHoverExit();
    public void OnClick();
}
