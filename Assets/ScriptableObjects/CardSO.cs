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
        TIMER,
        IF,
        CUSTOM
    }
    public FunctionType functionType;
    public int cardTimer {get {return cardTimer;} set {if(value>0) cardTimer = value; else cardTimer = 0;}}
    public Tile placedOn;


    public void RunCard()
    {
        Debug.Log("Ran Normal Variant");
        //GameManager.gm.variables.CO2 += emsissionAmount;
    }
}
