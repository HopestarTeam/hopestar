using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHandler : MonoBehaviour
{
    public CardSO properties;

    GlobalVariables global;
    public DeckBehaviour deck;
    public Tile placedOn;
    public bool placedThisTurn;
    [SerializeField] TextMeshProUGUI cardName, costText, upkeepText, gainsText, emissionText, flavorText;
    MeshRenderer mesh;
    public ResourceTypeDefinition[] resourceCosts, resourceUpkeep, resourceGains;
    public int emsissionAmount, cardTimer;

    // Start is called before the first frame update
    void Start()
    {
        resourceCosts = properties.resourceCosts;
        resourceUpkeep = properties.resourceUpkeep;
        resourceGains = properties.resourceGains;
        emsissionAmount = properties.emsissionAmount;

        gameObject.name = properties.cardName;
        global = GameManager.gm.variables;
        deck = transform.parent.GetComponent<DeckBehaviour>();

        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        mesh.material.SetTexture("_MainTex",properties.cardImage.texture);
        
        if(properties.hasFunction)
        {
            switch (properties.functionType)
            {
                case CardSO.FunctionType.IF:
                    IfTileCardSO iCard = (IfTileCardSO)properties;
                    if(placedOn != null)
                    {
                        if(iCard.CheckCardIf(placedOn))
                        {
                            resourceGains = iCard.conditionalResourceGains;
                        }
                        else
                        {
                            resourceGains = iCard.resourceGains;
                        }
                    }
                    break;
                case CardSO.FunctionType.TIMER:
                    cardTimer = properties.cardTimer;
                    break;
                default:
                    break;
            }
        }
    }
    
    public bool CheckCard()
    {
        bool result = true;

        float  rawCosts = 0, foodCosts = 0, energyCosts = 0, consumerCosts = 0, industryCosts = 0;
        if(!placedThisTurn)
        {
            if(resourceCosts != null)
            foreach(ResourceTypeDefinition definition in resourceCosts)
            {
                switch(definition.resourceType)
                {
                    case ResourceTypeDefinition.ResourceType.RAW:
                        rawCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.FOOD:
                        foodCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.ENERGY:
                        energyCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.CONSUMER:
                        consumerCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.INDUSTRY:
                        industryCosts += definition.amount;
                        break;
                    default:
                        break;
                }
            }
            if(resourceUpkeep != null)
            foreach(ResourceTypeDefinition definition in resourceUpkeep)
            {
                switch(definition.resourceType)
                {
                    case ResourceTypeDefinition.ResourceType.RAW:
                        rawCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.FOOD:
                        foodCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.ENERGY:
                        energyCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.CONSUMER:
                        consumerCosts += definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.INDUSTRY:
                        industryCosts += definition.amount;
                        break;
                    default:
                        break;
                }
            }
            foreach(KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> kvp in global.variables)
            {
                switch(kvp.Key)
                {
                    case GlobalVariableEnum.Food:
                        result = global.variables[kvp.Key].GetSurplus() >= foodCosts;
                        break;
                    case GlobalVariableEnum.Material:
                        result = global.variables[kvp.Key].GetSurplus() >= rawCosts;
                        break;
                    case GlobalVariableEnum.Energy:
                        result = global.variables[kvp.Key].GetSurplus() >= energyCosts;
                        break;
                    case GlobalVariableEnum.Consumer:
                        result = global.variables[kvp.Key].GetSurplus() >= consumerCosts;
                        break;
                    case GlobalVariableEnum.Industry:
                        result = global.variables[kvp.Key].GetSurplus() >= industryCosts;
                        break;
                    default:
                        break;
                }
                if(!result)
                    break;
            }
        }
        return result;
    }

    public void RunCosts(bool runCosts = true)
    {
        int flipper = 1,
        rawCosts = 0, foodCosts = 0, energyCosts = 0, consumerCosts = 0, industryCosts = 0,
        rawUpkeep = 0, foodUpkeep = 0, energyUpkeep = 0, consumerUpkeep = 0, industryUpkeep = 0;
        if(!runCosts)
        {
            flipper = -1;
            placedThisTurn = false;
        }

        if(!placedThisTurn)
        {
            if(resourceCosts != null)
            foreach(ResourceTypeDefinition definition in resourceCosts)
            {
                switch(definition.resourceType)
                {
                    case ResourceTypeDefinition.ResourceType.RAW:
                        rawCosts = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.FOOD:
                        foodCosts = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.ENERGY:
                        energyCosts = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.CONSUMER:
                        consumerCosts = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.INDUSTRY:
                        industryCosts = definition.amount;
                        break;
                    default:
                        break;
                }
            }
            if(resourceUpkeep != null)
            foreach(ResourceTypeDefinition definition in resourceUpkeep)
            {
                switch(definition.resourceType)
                {
                    case ResourceTypeDefinition.ResourceType.RAW:
                        rawUpkeep = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.FOOD:
                        foodUpkeep = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.ENERGY:
                        energyUpkeep = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.CONSUMER:
                        consumerUpkeep = definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.INDUSTRY:
                        industryUpkeep = definition.amount;
                        break;
                    default:
                        break;
                }
            }
            foreach(KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> kvp in global.variables)
            {
                GlobalVariables.ResourceVariable newVars = new GlobalVariables.ResourceVariable();
                switch(kvp.Key)
                {
                    case GlobalVariableEnum.Food:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Food].production,
                            global.variables[GlobalVariableEnum.Food].upkeep + foodUpkeep * flipper,
                            global.variables[GlobalVariableEnum.Food].spent + foodCosts * flipper
                        );
                        global.variables.Remove(GlobalVariableEnum.Food);
                        global.variables.Add(GlobalVariableEnum.Food, newVars);
                        break;
                    case GlobalVariableEnum.Material:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Material].production,
                            global.variables[GlobalVariableEnum.Material].upkeep + rawUpkeep * flipper,
                            global.variables[GlobalVariableEnum.Material].spent + rawCosts * flipper
                        );
                        global.variables.Remove(GlobalVariableEnum.Material);
                        global.variables.Add(GlobalVariableEnum.Material, newVars);
                        break;
                    case GlobalVariableEnum.Energy:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Energy].production,
                            global.variables[GlobalVariableEnum.Energy].upkeep + energyUpkeep * flipper,
                            global.variables[GlobalVariableEnum.Energy].spent + energyCosts * flipper
                        );
                        global.variables.Remove(GlobalVariableEnum.Energy);
                        global.variables.Add(GlobalVariableEnum.Energy, newVars);
                        break;
                    case GlobalVariableEnum.Consumer:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Consumer].production,
                            global.variables[GlobalVariableEnum.Consumer].upkeep + consumerUpkeep * flipper,
                            global.variables[GlobalVariableEnum.Consumer].spent + consumerCosts * flipper
                        );
                        global.variables.Remove(GlobalVariableEnum.Consumer);
                        global.variables.Add(GlobalVariableEnum.Consumer, newVars);
                        break;
                    case GlobalVariableEnum.Industry:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Industry].production,
                            global.variables[GlobalVariableEnum.Industry].upkeep + industryUpkeep * flipper,
                            global.variables[GlobalVariableEnum.Industry].spent + industryCosts * flipper
                        );
                        global.variables.Remove(GlobalVariableEnum.Industry);
                        global.variables.Add(GlobalVariableEnum.Industry, newVars);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void ResolveCard()
    {
        //global.CO2 += emsissionAmount;
        int
        rawProduction = 0, foodProduction = 0, energyProduction = 0, consumerProduction = 0, industryProduction = 0,
        rawUpkeep = 0, foodUpkeep = 0, energyUpkeep = 0, consumerUpkeep = 0, industryUpkeep = 0;
        if(properties.hasFunction)
        {
            switch (properties.functionType)
            {
                case CardSO.FunctionType.IF:
                    IfTileCardSO iCard = (IfTileCardSO)properties;
                    if(iCard.CheckCardIf(placedOn))
                    {
                        resourceGains = iCard.conditionalResourceGains;
                    }
                    else
                    {
                        resourceGains = iCard.resourceGains;
                    }
                    break;
                case CardSO.FunctionType.TIMER:
                    if(cardTimer == 0)
                    {
                        TimerCardSO tCard = (TimerCardSO)properties;
                        if(placedOn.HasProperty(tCard.fromProperty) && tCard.fromProperty != TileProperty.NULL)
                            placedOn.tileProperties.Remove(tCard.fromProperty);
                        if(!placedOn.HasProperty(tCard.toProperty) && tCard.toProperty != TileProperty.NULL)
                            placedOn.tileProperties.Add(tCard.toProperty);
                        if(tCard.destroyedOnFinish)
                        {
                            //ToDo: Should update upkeep on ui
                            Destroy(gameObject);
                        }
                    }
                    cardTimer--;
                    break;
                default:

                    break;
            }
        }

        if(resourceGains != null)
        foreach(ResourceTypeDefinition definition in resourceGains)
        {
            switch(definition.resourceType)
            {
                case ResourceTypeDefinition.ResourceType.RAW:
                    rawProduction = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    foodProduction = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    energyProduction = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    consumerProduction = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    industryProduction = definition.amount;
                    break;
                default:
                    break;
            }
        }
        
        if(resourceUpkeep != null)
        foreach(ResourceTypeDefinition definition in resourceUpkeep)
        {
            switch(definition.resourceType)
            {
                case ResourceTypeDefinition.ResourceType.RAW:
                    rawUpkeep = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    foodUpkeep = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    energyUpkeep = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    consumerUpkeep = definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    industryUpkeep = definition.amount;
                    break;
                default:
                    break;
            }
        }
        foreach(KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> kvp in global.variables)
            {
                GlobalVariables.ResourceVariable newVars = new GlobalVariables.ResourceVariable();
                switch(kvp.Key)
                {
                    case GlobalVariableEnum.Food:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Food].production + foodProduction,
                            global.variables[GlobalVariableEnum.Food].upkeep + foodUpkeep,
                            0
                        );
                        global.variables.Remove(GlobalVariableEnum.Food);
                        global.variables.Add(GlobalVariableEnum.Food, newVars);
                        break;
                    case GlobalVariableEnum.Material:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Material].production + rawProduction,
                            global.variables[GlobalVariableEnum.Material].upkeep + rawUpkeep,
                            0
                        );
                        global.variables.Remove(GlobalVariableEnum.Material);
                        global.variables.Add(GlobalVariableEnum.Material, newVars);
                        break;
                    case GlobalVariableEnum.Energy:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Energy].production + energyProduction,
                            global.variables[GlobalVariableEnum.Energy].upkeep + energyUpkeep,
                            0
                        );
                        global.variables.Remove(GlobalVariableEnum.Energy);
                        global.variables.Add(GlobalVariableEnum.Energy, newVars);
                        break;
                    case GlobalVariableEnum.Consumer:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Consumer].production + consumerProduction,
                            global.variables[GlobalVariableEnum.Consumer].upkeep + consumerUpkeep,
                            0
                        );
                        global.variables.Remove(GlobalVariableEnum.Consumer);
                        global.variables.Add(GlobalVariableEnum.Consumer, newVars);
                        break;
                    case GlobalVariableEnum.Industry:
                        newVars = new GlobalVariables.ResourceVariable(
                            global.variables[GlobalVariableEnum.Industry].production + industryProduction,
                            global.variables[GlobalVariableEnum.Industry].upkeep + industryUpkeep,
                            0
                        );
                        global.variables.Remove(GlobalVariableEnum.Industry);
                        global.variables.Add(GlobalVariableEnum.Industry, newVars);
                        break;
                    default:
                        break;
                }
            }
    }
}
