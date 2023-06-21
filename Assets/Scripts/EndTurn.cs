using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NativeClassExtensions;

public class EndTurn : MonoBehaviour
{
    [SerializeField] Grid myGrid;
    [SerializeField] GameObject objectives;

    GlobalVariables variables;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject finishGameScreen;

    int listSize;

    // Start is called before the first frame update
    void Start()
    {
        listSize = myGrid.count;
        variables = GameManager.gm.variables;
    }

    // Update is called once per frame
    void Update()
    {
       // On mouseclick, call the EndTurnFunction() 
    }

    bool CheckUpkeep()
    {
        foreach (KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> variable in GameManager.gm.variables.variables)
        {
            if(variable.Key != GlobalVariableEnum.CO2 && variable.Value.upkeep > variable.Value.production)
            {
                return false;
            }
        }
        return true;
    }
    public void EndTurnFunction()
    {
        if(!CheckUpkeep())
        {
            Debug.Log("Upkeep fugd, no turn end for you :)");
            return;
        }
        
        
        GlobalVariableEnum[] enums = (GlobalVariableEnum[])Enum.GetValues(typeof(GlobalVariableEnum));
        
        foreach(GlobalVariableEnum current in EnumExtensions.GlobalVariableEnumAsArray())
        {
            if(current == GlobalVariableEnum.CO2)continue;
            variables.variables[current].production = 0;
            variables.variables[current].upkeep = 0;
            variables.variables[current].spent = 0;
        }

        foreach (Tile tile in FindObjectsByType(typeof(Tile),FindObjectsSortMode.None))
        {
            tile.ResolveTile();
        }

        for (int i = 0; i < listSize; i++)
        {

            Tile current = myGrid[i].GetComponent<Tile>();
            if(current == null)
            {
                Debug.Log("No tile component found");
                continue;
            }

            if (current.cardHandler != null)
            {
                // Fetch card from current, activate this later.
                current.cardHandler.ResolveCard();
                current.cardHandler.placedThisTurn = false;
            }

        }

        // Here the script should set Raw Resources, Food, Energy, ConsumerGoods and IndustryGoods to upkeep.
        // Turned this into a loop to make it more flexible
        foreach(GlobalVariableEnum current in EnumExtensions.GlobalVariableEnumAsArray())
        {
            if(current != GlobalVariableEnum.CO2)continue;
            variables.variables[current].production = variables.variables[current].upkeep;
        }

        if(objectives.GetComponent<Objectives>().CheckObjectiveComplete()){
            if(DontDestroyData.data.levelNumber > DontDestroyData.data.levelsCompleted)
                DontDestroyData.data.levelsCompleted = DontDestroyData.data.levelNumber;
            if(DontDestroyData.data.levelNumber == DontDestroyData.data.numberOfLevels){
                finishGameScreen.SetActive(true);
                SoundPlayer.sm.GameOverSound();
            }
            else{
                victoryScreen.SetActive(true);
                SoundPlayer.sm.VictorySound();
                //Debug.Log("victoly");
            }       
        }
        else{
            if(GameOver.CheckGameOver()){
                gameOverScreen.SetActive(true);
                SoundPlayer.sm.GameOverSound();
                return;               
            }   
        }
        
        //variables.RawResources = variables.RawResourcesUpkeep;
        //variables.Food = variables.FoodUpkeep;
        //variables.Energy = variables.EnergyUpkeep;
        //variables.ConsumerGoods = variables.ConsumerGoodsUpkeep;
        //variables.IndustryGoods = variables.IndustryGoodsUpkeep;

        // Here should come a popup window with stats and a close button.
        //GameManager.gm.menuManager.ShowInfoScreen(GameManager.gm.variables);
        if(GameManager.gm.variables.CO2 < 0)
            GameManager.gm.variables.CO2 = 0;
        GameManager.gm.menuManager.UpdateHud();

    }

    private void OnMouseDown() {
        EndTurnFunction();
    }
}
