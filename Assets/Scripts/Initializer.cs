using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    public LevelData data;
    public Grid grid;
    public TilePropertySetter tps;
    public GameManager gameManager;
    public DeckBehaviour deck;
    public Objectives objectives;

    public GameObject dropDownButton;

    void Initialize(LevelData data)
    {
        grid.size = data.grid.gridSize;
        grid.Generate();

        tps.tileProperties = new List<TilePropertyArea>(data.grid.gridTileProperties);
        deck.cardSOs = new List<CardSO>(data.Deck).ToArray();

        //Initialize the variables
        gameManager.variables.Initialize();
        //foreach(GlobalVariableAuthoringScript.AuthoringStruct current in data.initialVariableValues)
        //{
        //    gameManager.variables.variables[current.variable].production = current.value;
        //}

        foreach(GlobalVariableAuthoringScript.AuthoringStruct current in data.objectives)
        {
            switch(current.variable)
            {
                case GlobalVariableEnum.Energy:
                    objectives.energyTarget = current.value;
                    break;
                case GlobalVariableEnum.Consumer:
                    objectives.consumerTarget = current.value;
                    break;
                case GlobalVariableEnum.Food:
                    objectives.foodTarget = current.value;
                    break;
                case GlobalVariableEnum.Industry:
                    objectives.industryTarget = current.value;
                    break;
                case GlobalVariableEnum.Material:
                    objectives.materialTarget = current.value;
                    break;
                case GlobalVariableEnum.Science:
                    objectives.scienceTarget = current.value;
                    break;
                default:
                    Debug.LogWarning($"{current.variable} is an invalid objective target");
                    break;
            }
        }

        dropDownButton.SetActive(data.showExtendButton);
    }

    void Awake()
    {
        if(data)
        {
            Initialize(data);
        }
    }
}


