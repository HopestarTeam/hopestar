using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour
{
    public abstract void OnHoverEnter();
    public abstract void OnHoverStay();
    public abstract void OnHoverExit();
    public abstract void OnClick();
}
