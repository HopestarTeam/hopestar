using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceIcon : MonoBehaviour
{
    Sprite resourceProductionSprite;
    Sprite resourceSpentSprite;
    Sprite resourceInUpkeepSprite;

    Image myImage;

    public void SetAsSpent(){myImage.overrideSprite = resourceSpentSprite;}
    public void SetAsUpkeep(){myImage.overrideSprite = resourceInUpkeepSprite;}
    public void SetAsProduction(){myImage.overrideSprite = null;}

    public void SetIcons(Sprite production, Sprite spent, Sprite inUpkeep){
        resourceProductionSprite = production;
        resourceSpentSprite = spent;
        resourceInUpkeepSprite = inUpkeep;

        myImage.sprite = resourceProductionSprite;
    }

    void Awake(){
        myImage = GetComponent<Image>();

        resourceProductionSprite = Resources.Load<Sprite>("Icons/energy");
        resourceSpentSprite = Resources.Load<Sprite>("Icons/energy_spent");
        resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/energy_locked");

        myImage.sprite = resourceProductionSprite;
    }

    // Start is called before the first frame update
    /*void Start(){
        switch (transform.parent.name)
        {
            case "MaterialDisplay":
                resourceProductionSprite = Resources.Load<Sprite>("Icons/material");
                resourceSpentSprite = Resources.Load<Sprite>("Icons/material_spent");
                resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/material_locked");
            break;

            case "FoodDisplay":
                resourceProductionSprite = Resources.Load<Sprite>("Icons/food");
                resourceSpentSprite = Resources.Load<Sprite>("Icons/food_spent");
                resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/food_locked");
            break;

            case "IndustryDisplay":
                resourceProductionSprite = Resources.Load<Sprite>("Icons/industry");
                resourceSpentSprite = Resources.Load<Sprite>("Icons/industry_spent");
                resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/industry_locked");
            break;

            case "ConsumerDisplay":
                resourceProductionSprite = Resources.Load<Sprite>("Icons/consumer");
                resourceSpentSprite = Resources.Load<Sprite>("Icons/consumer_spent");
                resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/consumer_locked");
            break;

            case "ScienceDisplay":
                resourceProductionSprite = Resources.Load<Sprite>("Icons/science");
                resourceSpentSprite = Resources.Load<Sprite>("Icons/science_spent");
                resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/science_locked");
            break;
            
            default:
                resourceProductionSprite = Resources.Load<Sprite>("Icons/energy");
                resourceSpentSprite = Resources.Load<Sprite>("Icons/energy_spent");
                resourceInUpkeepSprite = Resources.Load<Sprite>("Icons/energy_locked");
            break;
        }

        myImage.sprite = resourceProductionSprite;
    }*/

    // Update is called once per frame
    void Update()
    {

    }
}
