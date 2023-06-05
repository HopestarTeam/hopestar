using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager gm {get; private set;}

    public GlobalVariables variables;

    public MenuManager menuManager;
    EndTurn end;
    Button endTurnButton;

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
            end = GetComponent<EndTurn>();
            endTurnButton = menuManager.Hud.rootVisualElement.Q("EndTurnButton") as Button;
            endTurnButton.RegisterCallback<ClickEvent>(ClickEndTurn);
        }
    }

    void ClickEndTurn(ClickEvent evt)
    {
        end.EndTurnFunction();
    }

    void Update()
    {
        menuManager.Update();
    }
}
