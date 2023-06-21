using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NativeClassExtensions;

public class Initializer : MonoBehaviour
{
    public LevelData data;
    public Grid grid;
    public TilePropertySetter tps;
    public GameManager gameManager;
    public DeckBehaviour deck;
    public Objectives objectives;

    public GameObject dropDownButton;

    public void Initialize(LevelData data)
    {
        this.data = null;
        grid.size = data.grid.gridSize;
        grid.Generate();

        tps.tileProperties = new List<TilePropertyArea>(data.grid.gridTileProperties);
        tps.SetTileProperties();
        deck.cardSOs = new List<CardSO>(data.Deck).ToArray();

        //Initialize the variables
        gameManager.variables.Initialize();
        foreach(GlobalVariableEnum current in EnumExtensions.GlobalVariableEnumAsArray())
        {
            gameManager.variables.variables[current] = new GlobalVariables.ResourceVariable(0,0,0);
        }

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
        if(data != null)
        {
            Initialize(data);
        }
    }
}


