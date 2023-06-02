using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardProperties", menuName = "ScriptableObjects/New Card Properties", order = 1)]
public class CardSO : ScriptableObject
{
    public string cardName {get {return _cardName;}}
    [SerializeField] private string _cardName;
    public CardType cardType {get {return _cardType;}}
    [SerializeField] CardType _cardType;
    public TileProperty[] requiredTileProperties {get {return _requiredTileProperties;}}
    public TileProperty[] blockedTileProperties {get {return _blockedTileProperties;}}
    [SerializeField] TileProperty[] _requiredTileProperties, _blockedTileProperties;
    public ResourceTypeDefinition[] resourceCosts {get {return _resourceCosts;}}
    public ResourceTypeDefinition[] resourceUpkeep {get {return _resourceUpkeep;}}
    public ResourceTypeDefinition[] resourceGains {get {return _resourceGains;}}
    [SerializeField] ResourceTypeDefinition[] _resourceCosts, _resourceUpkeep, _resourceGains;
    public int emsissionAmount {get {return _emsissionAmount;}}
    [SerializeField] int _emsissionAmount;
    public bool hasFunction {get {return _hasFunction;}}
    [SerializeField] bool _hasFunction;
    public enum FunctionType
    {
        DEFAULT,
        IF,
        TIMER
    }
    public FunctionType functionType {get {return _functionType;}}
    [SerializeField] FunctionType _functionType;
    public int cardTimer {get {return _cardTimer;}}
    [SerializeField] int _cardTimer;
    public Tile placedOn;


    public void RunCard()
    {
        GlobalVariables variables = GameManager.gm.variables;
        variables.CO2 += emsissionAmount;
        foreach(ResourceTypeDefinition definition in resourceGains)
        {
            switch(definition.resourceType)
            {
                case ResourceTypeDefinition.ResourceType.RAW:
                    variables.RawResources += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    variables.Food += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    variables.Energy += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    variables.ConsumerGoods += definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    variables.IndustryGoods += definition.amount;
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
                    variables.RawResources -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.FOOD:
                    variables.Food -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.ENERGY:
                    variables.Energy -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.CONSUMER:
                    variables.ConsumerGoods -= definition.amount;
                    break;
                case ResourceTypeDefinition.ResourceType.INDUSTRY:
                    variables.IndustryGoods -= definition.amount;
                    break;
                default:
                    break;
            }
        }
    }
}
