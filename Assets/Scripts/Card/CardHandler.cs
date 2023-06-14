using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHandler : MonoBehaviour
{
    public CardSO properties;

    GlobalVariables variables;
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
        variables = GameManager.gm.variables;
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
                        variables.ConsumerGoodsUpkeep -= definition.amount;
                        break;
                    case ResourceTypeDefinition.ResourceType.INDUSTRY:
                        variables.IndustryGoodsUpkeep -= definition.amount;
                        break;
                    default:
                        break;
                }
            }
            foreach(KeyValuePair<GlobalVariableEnum,float> kvp in variables.variables)
            {
                switch(kvp.Key)
                {
                    case GlobalVariableEnum.Food:
                        result = variables.variables[kvp.Key] >= foodCosts;
                        break;
                    case GlobalVariableEnum.RawResources:
                        result = variables.variables[kvp.Key] >= rawCosts;
                        break;
                    case GlobalVariableEnum.Energy:
                        result = variables.variables[kvp.Key] >= energyCosts;
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
        float flipper = 1, rawCosts = 0, foodCosts = 0, energyCosts = 0, consumerCosts = 0, industryCosts = 0;
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
            Dictionary<GlobalVariableEnum,float> tmp = new Dictionary<GlobalVariableEnum, float>();
            foreach(KeyValuePair<GlobalVariableEnum,float> kvp in variables.variables)
            {
                switch(kvp.Key)
                {
                    case GlobalVariableEnum.Food:
                        tmp.Add(kvp.Key,kvp.Value - foodCosts * flipper);
                        break;
                    case GlobalVariableEnum.RawResources:
                        tmp.Add(kvp.Key,kvp.Value - rawCosts * flipper);
                        break;
                    case GlobalVariableEnum.Energy:
                        tmp.Add(kvp.Key,kvp.Value - energyCosts * flipper);
                        break;
                    case GlobalVariableEnum.ConsumerGoods:
                        tmp.Add(kvp.Key,kvp.Value - consumerCosts * flipper);
                        break;
                    case GlobalVariableEnum.IndustryGoods:
                        tmp.Add(kvp.Key,kvp.Value - industryCosts * flipper);
                        break;
                    default:
                        tmp.Add(kvp.Key,kvp.Value);
                        break;
                }
            }
            variables.variables = tmp;
        }
    }

    public void ResolveCard()
    {
        variables.CO2 += emsissionAmount;
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
                    variables.RawResourcesUpkeep += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    variables.FoodUpkeep += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    variables.EnergyUpkeep += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    variables.ConsumerGoodsUpkeep += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    variables.IndustryGoodsUpkeep += definition.amount;
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
                    variables.RawResourcesUpkeep -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    variables.FoodUpkeep -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    variables.EnergyUpkeep -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    variables.ConsumerGoodsUpkeep -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    variables.IndustryGoodsUpkeep -= definition.amount;
                    break;
                default:
                    break;
            }
        }
    }
}
