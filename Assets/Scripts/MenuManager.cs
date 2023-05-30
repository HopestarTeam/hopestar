using System;
using UnityEngine.Events;

[Serializable]
public class MenuManager
{
    public bool inMenu;

    public UnityEvent OnMenuEnter, OnMenuExit;

    public void EnterMenu()
    {
        inMenu = true;
        OnMenuEnter.Invoke();
    }

    public void ExitMenu()
    {
        inMenu = false;
        OnMenuExit.Invoke();
    }
}
