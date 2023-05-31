using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardProperties", menuName = "ScriptableObjects/Create New Card Properties", order = 1)]
public class CardSO : ScriptableObject
{
    public string cardName;
    public CardTypeDefinition.CardType cardType;
    public TilePropertyDefinition[] requiredTileProperties;
    public TilePropertyDefinition[] blockedTileProperties;
    public ResourceTypeDefinition[] resourceCosts;
    public ResourceTypeDefinition[] resourceUpkeep;
    public ResourceTypeDefinition[] resourceGains;
    public int emsissionAmount;
    public bool hasFunction;
    public int cardTimer {get {return cardTimer;} set {if(value>0) cardTimer = value; else cardTimer = 0;}}
}
