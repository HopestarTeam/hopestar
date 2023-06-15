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
    
    [SerializeField]public TileMaterialSetter tileMaterialSetter;

    void Start()
    {
        if(gm)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
            menuManager.Initialize();
            variables.Initialize();
            GlobalVariableAuthoringScript authorer;
            if(TryGetComponent<GlobalVariableAuthoringScript>(out authorer))
            {
                authorer.Author();
                //foreach(KeyValuePair<GlobalVariableEnum,float> current in variables.variables)
                //{
                //    Debug.Log($"{current.Key}: {current.Value}");
                //}
            }
            end = GetComponent<EndTurn>();
            //endTurnButton = menuManager.Hud.rootVisualElement.Q("EndTurnButton") as Button;
            //endTurnButton.RegisterCallback<ClickEvent>(ClickEndTurn);
            //menuManager.UpdateHud();
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
