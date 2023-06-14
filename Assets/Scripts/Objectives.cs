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
    List<ResourceObjective> listOfObjectives;

    public Dictionary<string, List<int>> resourceProduction = new Dictionary<string, List<int>>(){};

    private void UpdateResourceProduction(){
        /*the list has 4 values:
        the first three {production, spent, in_upkeep} are taken from the game manager (resource authority)
        the fourth value is surplus calculated as: surplus = production - spent - in_upkeep*/
        resourceProduction["Energy"] = new List<int>(){0, 0, 0, 0};
        resourceProduction["Food"] = new List<int>(){0, 0, 0, 0};
        resourceProduction["Material"] = new List<int>(){0, 0, 0, 0};
        resourceProduction["Industry"] = new List<int>(){0, 0, 0, 0};
        resourceProduction["Consumer"] = new List<int>(){0, 0, 0, 0};
        resourceProduction["Science"] = new List<int>(){0, 0, 0, 0};
    }

    class ResourceObjective{
        //a class that collects the name of a resource, the target objective and the two sprites (unfulfilled and fulfilled) for display
        public string name;
        public int target;
        public Sprite unfulfilledSprite;
        public Sprite fulfilledSprite;
        
        public ResourceObjective(string theName, int theTarget, Sprite spent, Sprite produced){
            name = theName;
            target = theTarget;
            unfulfilledSprite = spent;
            fulfilledSprite = produced;
        }

        public bool IsFulfilled(Dictionary<string, List<int>> resources){
            return  resources[name][3] >= target;
        }
    }

    private void CheckObjectiveComplete(){
        int numberOfObjectivesSatisfied = 0;
        foreach (ResourceObjective objective in listOfObjectives){
            if (objective.IsFulfilled(resourceProduction)){ numberOfObjectivesSatisfied++;}
        }

        if (numberOfObjectivesSatisfied == listOfObjectives.Count){objectiveComplete = true;}
        else {objectiveComplete = false;}
    }

    private void Awake() {
        //we create a list of the objectives for this scene
        if (energyTarget != 0){
            ResourceObjective energyObjective = new ResourceObjective("Energy", 
                                                                        energyTarget,
                                                                        Resources.Load<Sprite>("Icons/energy_spent"),
                                                                        Resources.Load<Sprite>("Icons/energy"));
            listOfObjectives.Add(energyObjective);
        }
        if (foodTarget != 0){
            ResourceObjective foodObjective = new ResourceObjective("Food", 
                                                                        foodTarget,
                                                                        Resources.Load<Sprite>("Icons/food_spent"),
                                                                        Resources.Load<Sprite>("Icons/food"));
            listOfObjectives.Add(foodObjective);
        }
        if (materialTarget != 0){
            ResourceObjective materialObjective = new ResourceObjective("Material", 
                                                                        materialTarget,
                                                                        Resources.Load<Sprite>("Icons/material_spent"),
                                                                        Resources.Load<Sprite>("Icons/material"));
            listOfObjectives.Add(materialObjective);
        }
        if (industryTarget != 0){
            ResourceObjective industryObjective = new ResourceObjective("Industry", 
                                                                        industryTarget,
                                                                        Resources.Load<Sprite>("Icons/industrial_spent"),
                                                                        Resources.Load<Sprite>("Icons/industrial"));
            listOfObjectives.Add(industryObjective);
        }
        if (consumerTarget != 0){
            ResourceObjective consumerObjective = new ResourceObjective("Consumer", 
                                                                        consumerTarget,
                                                                        Resources.Load<Sprite>("Icons/consumer_spent"),
                                                                        Resources.Load<Sprite>("Icons/consumer"));
            listOfObjectives.Add(consumerObjective);
        }
        if (scienceTarget != 0){
            ResourceObjective scienceObjective = new ResourceObjective("Science", 
                                                                        scienceTarget,
                                                                        Resources.Load<Sprite>("Icons/science_spent"),
                                                                        Resources.Load<Sprite>("Icons/science"));
            listOfObjectives.Add(scienceObjective);
        }
    }

    // Start is called before the first frame update
    void Start(){
        UpdateResourceProduction();
    }

    // Update is called once per frame
    void Update(){
        UpdateResourceProduction();
        CheckObjectiveComplete();        
    }
}
