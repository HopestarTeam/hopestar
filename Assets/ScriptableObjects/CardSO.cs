using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardProperties", menuName = "ScriptableObjects/New Card Properties", order = 1)]
public class CardSO : ScriptableObject
{
    public string cardName {get {return _cardName;}}
    [SerializeField] private string _cardName;
    public Sprite cardImage;
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
    public string flavorText;
    
}
