using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm {get; private set;}

    public GlobalVariables variables;

    public MenuManager menuManager;

    void Awake()
    {
        if(gm)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
            menuManager.Initialize();
        }
    }

    void Update()
    {
        menuManager.Update();
    }
}
