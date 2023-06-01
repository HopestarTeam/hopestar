using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardProperties", menuName = "ScriptableObjects/New Card Properties", order = 1)]
public class CardSO : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public TileProperty[] requiredTileProperties;
    public TileProperty[] blockedTileProperties;
    public ResourceTypeDefinition[] resourceCosts;
    public ResourceTypeDefinition[] resourceUpkeep;
    public ResourceTypeDefinition[] resourceGains;
    public int emsissionAmount;
    public bool hasFunction;
    public enum FunctionType
    {
        DEFAULT,
        IF,
        TIMER
    }
    [HideInInspector]
    public FunctionType functionType;
    public int cardTimer {get {return cardTimer;} set {if(value>0) cardTimer = value; else cardTimer = 0;}}
    public Tile placedOn;


    public void RunCard()
    {
        GameManager.gm.variables.CO2 += emsissionAmount;
        foreach(ResourceTypeDefinition definition in resourceGains)
        {
            switch(definition.resourceType)
            {
                case ResourceTypeDefinition.ResourceType.RAW:
                    GameManager.gm.variables.RawResources += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    GameManager.gm.variables.Food += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    GameManager.gm.variables.Energy += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    GameManager.gm.variables.ConsumerGoods += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    GameManager.gm.variables.IndustryGoods += definition.amount;
                    break;
                default:
                    break;
            }
        }
        foreach(ResourceTypeDefinition definition in resourceUpkeep)
        {
            switch(definition.resourceType)
            {
                case ResourceTypeDefinition.ResourceType.RAW:
                    GameManager.gm.variables.RawResources -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    GameManager.gm.variables.Food -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    GameManager.gm.variables.Energy -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    GameManager.gm.variables.ConsumerGoods -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    GameManager.gm.variables.IndustryGoods -= definition.amount;
                    break;
                default:
                    break;
            }
        }
    }
}
