using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gm {get; private set;}
    [SerializeField] GameObject dontDestroyData;

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
            variables.CO2 = 0;
            //endTurnButton = menuManager.Hud.rootVisualElement.Q("EndTurnButton") as Button;
            //endTurnButton.RegisterCallback<ClickEvent>(ClickEndTurn);
            //menuManager.UpdateHud();
        }

        if(DontDestroyData.data == null)
        {
            Instantiate(dontDestroyData);
        }
        if(SceneManager.GetActiveScene().name.Contains("level_"))
        {
            string[] sceneName = SceneManager.GetActiveScene().name.Split("_");
            DontDestroyData.data.levelNumber = int.Parse(sceneName[1]);
        }
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().name.Contains("level_"))
        {
            string[] sceneName = SceneManager.GetActiveScene().name.Split("_");
            int levelNumber = int.Parse(sceneName[1]);
            if(DontDestroyData.data.score[levelNumber] == 0 || variables.CO2 < DontDestroyData.data.score[levelNumber])
                DontDestroyData.data.score[levelNumber] = variables.CO2;
            if(levelNumber >= DontDestroyData.data.numberOfLevels)
                SceneManager.LoadScene("SampleScene"); // Change this to main menu/game ended scene when that exists
            else
            {
                SceneManager.LoadScene("level_" + (DontDestroyData.data.levelNumber + 1));
                if(DontDestroyData.data.levelNumber > DontDestroyData.data.levelsCompleted)
                    DontDestroyData.data.levelsCompleted = DontDestroyData.data.levelNumber;
            }
        }
        else
        {
            SceneManager.LoadScene("level_1");
        }
    }

    public void LoadLevel(int levelNumber)
    {
        if(levelNumber > 0 && levelNumber <= DontDestroyData.data.numberOfLevels)
            SceneManager.LoadScene("level_" + levelNumber);
        else
            Debug.Log($"Level number {levelNumber} does not exist according to DontDestroyData.");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    void OnDestroy()
    {
        gm = null;
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
