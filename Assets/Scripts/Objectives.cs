using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    [HideInInspector] public bool objectiveComplete = false;

    //remove serializefield after testing
    [SerializeField] int energyTarget = 0;
    [SerializeField] int foodTarget = 3;
    [SerializeField] int materialTarget = 0;
    [SerializeField] int industryTarget = 0;
    [SerializeField] int consumerTarget = 0;
    [SerializeField] int scienceTarget = 0;
    //int happinessTarget = 0;
    //int emissionsTarget = 0;
    [HideInInspector] public List<ResourceObjective> listOfObjectives = new List<ResourceObjective>();
    [HideInInspector] public int numberOfObjectives;

    public class ResourceObjective{
        //a class that collects the name of a resource, the target objective and the two sprites (unfulfilled and fulfilled) for display
        public GlobalVariableEnum nameEnum;
        public int target;
        public Sprite unfulfilledSprite;
        public Sprite fulfilledSprite;
        
        public ResourceObjective(GlobalVariableEnum theName, int theTarget, Sprite unfulfilled, Sprite fulfilled){
            nameEnum = theName;
            target = theTarget;
            unfulfilledSprite = unfulfilled;
            fulfilledSprite = fulfilled;
        }

        public bool IsFulfilled(){
            int surplus = GameManager.gm.variables.variables[nameEnum].GetSurplus();
            return  surplus >= target;
        }
    }

    private void CheckObjectiveComplete(){
        if (listOfObjectives == null){Debug.Log("you don't have any objectives");}
        else {
            int numberOfObjectivesSatisfied = 0;
            foreach (ResourceObjective objective in listOfObjectives){
                if (objective.IsFulfilled()){numberOfObjectivesSatisfied++;}
            }

            if (numberOfObjectivesSatisfied == listOfObjectives.Count){objectiveComplete = true;}
            else {objectiveComplete = false;}
        }
        
    }

    private void Awake() {
        //we create a list of the objectives for this scene
        if (energyTarget != 0){
            ResourceObjective energyObjective = new ResourceObjective(GlobalVariableEnum.Energy, 
                                                                        energyTarget,
                                                                        Resources.Load<Sprite>("Icons/energy_spent"),
                                                                        Resources.Load<Sprite>("Icons/energy"));
            listOfObjectives.Add(energyObjective);
        }
        if (foodTarget != 0){
            ResourceObjective foodObjective = new ResourceObjective(GlobalVariableEnum.Food, 
                                                                        foodTarget,
                                                                        Resources.Load<Sprite>("Icons/food_spent"),
                                                                        Resources.Load<Sprite>("Icons/food"));
            listOfObjectives.Add(foodObjective);
        }
        if (materialTarget != 0){
            ResourceObjective materialObjective = new ResourceObjective(GlobalVariableEnum.Material, 
                                                                        materialTarget,
                                                                        Resources.Load<Sprite>("Icons/material_spent"),
                                                                        Resources.Load<Sprite>("Icons/material"));
            listOfObjectives.Add(materialObjective);
        }
        if (industryTarget != 0){
            ResourceObjective industryObjective = new ResourceObjective(GlobalVariableEnum.Industry, 
                                                                        industryTarget,
                                                                        Resources.Load<Sprite>("Icons/industry_spent"),
                                                                        Resources.Load<Sprite>("Icons/industry"));
            listOfObjectives.Add(industryObjective);
        }
        if (consumerTarget != 0){
            ResourceObjective consumerObjective = new ResourceObjective(GlobalVariableEnum.Consumer, 
                                                                        consumerTarget,
                                                                        Resources.Load<Sprite>("Icons/consumer_spent"),
                                                                        Resources.Load<Sprite>("Icons/consumer"));
            listOfObjectives.Add(consumerObjective);
        }
        if (scienceTarget != 0){
            ResourceObjective scienceObjective = new ResourceObjective(GlobalVariableEnum.Science, 
                                                                        scienceTarget,
                                                                        Resources.Load<Sprite>("Icons/science_spent"),
                                                                        Resources.Load<Sprite>("Icons/science"));
            listOfObjectives.Add(scienceObjective);
        }
        numberOfObjectives = listOfObjectives.Count;
    }

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        CheckObjectiveComplete(); 

        ForDebugDisplayObjectives(); 
        ForDebugAddEnergyProduction();     
    }

    void ForDebugDisplayObjectives(){
        if (Input.GetKeyDown(KeyCode.Space)){
            foreach (ResourceObjective objective in listOfObjectives){
                Debug.Log($"{objective.nameEnum}: {GameManager.gm.variables.variables[objective.nameEnum].GetSurplus()}/{objective.target}");
            }
            Debug.Log($"Objective complete: {objectiveComplete}");
        }
    }
    void ForDebugAddEnergyProduction(){
        if (Input.GetKeyDown(KeyCode.E)){
            GameManager.gm.AddValueToProduction(GlobalVariableEnum.Energy, 1);
            Debug.Log("added 1 energy production");
            Debug.Log(GameManager.gm.variables.variables[GlobalVariableEnum.Energy].production);
        }
    }
}
