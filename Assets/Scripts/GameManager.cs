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

    public PassiveProductionSettings tilePassiveProductionSettings;
    
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
            if(!variables.initialized)
            {
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
            }
            foreach (Tile tile in FindObjectsByType(typeof(Tile),FindObjectsSortMode.None))
            {
                tile.CheckForProperties();
                tile.ResolveTile();
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

    //get and set functions that help manage the global variables
    public void AddValueToProduction(GlobalVariableEnum resource, int value){
        GlobalVariables.ResourceVariable newValue = new GlobalVariables.ResourceVariable(
                                                                    GameManager.gm.variables.variables[resource].production + value,
                                                                    GameManager.gm.variables.variables[resource].upkeep,
                                                                    GameManager.gm.variables.variables[resource].spent
        );
        GameManager.gm.variables.variables[resource] = newValue;

        //call update display and update objectives functions   !!!
    }
    public void AddValueToUpkeep(GlobalVariableEnum resource, int value){
        GlobalVariables.ResourceVariable newValue = new GlobalVariables.ResourceVariable(
                                                                    GameManager.gm.variables.variables[resource].production,
                                                                    GameManager.gm.variables.variables[resource].upkeep + value,
                                                                    GameManager.gm.variables.variables[resource].spent
        );
        GameManager.gm.variables.variables[resource] = newValue;

        //call update display and update objectives functions   !!!
    }
    public void AddValueToSpent(GlobalVariableEnum resource, int value){
        GlobalVariables.ResourceVariable newValue = new GlobalVariables.ResourceVariable(
                                                                    GameManager.gm.variables.variables[resource].production,
                                                                    GameManager.gm.variables.variables[resource].upkeep,
                                                                    GameManager.gm.variables.variables[resource].spent + value
        );
        GameManager.gm.variables.variables[resource] = newValue;

        //call update display and update objectives functions   !!!
    }

    public void SetProductionToValue(GlobalVariableEnum resource, int value){
        GlobalVariables.ResourceVariable newValue = new GlobalVariables.ResourceVariable(
                                                                    value,
                                                                    GameManager.gm.variables.variables[resource].upkeep,
                                                                    GameManager.gm.variables.variables[resource].spent
        );
        GameManager.gm.variables.variables[resource] = newValue;

        //call update display and update objectives functions   !!!
    }
    public void SetUpkeepToValue(GlobalVariableEnum resource, int value){
        GlobalVariables.ResourceVariable newValue = new GlobalVariables.ResourceVariable(
                                                                    GameManager.gm.variables.variables[resource].production,
                                                                    value,
                                                                    GameManager.gm.variables.variables[resource].spent
        );
        GameManager.gm.variables.variables[resource] = newValue;

        //call update display and update objectives functions   !!!
    }
    public void SetSpentToValue(GlobalVariableEnum resource, int value){
        GlobalVariables.ResourceVariable newValue = new GlobalVariables.ResourceVariable(
                                                                    GameManager.gm.variables.variables[resource].production,
                                                                    GameManager.gm.variables.variables[resource].upkeep,
                                                                    value
        );
        GameManager.gm.variables.variables[resource] = newValue;

        //call update display and update objectives functions   !!!
    }
}
