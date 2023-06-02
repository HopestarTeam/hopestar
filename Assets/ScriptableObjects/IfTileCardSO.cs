using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIfCardProperties", menuName = "ScriptableObjects/New If Card Properties", order = 2)]
public class IfTileCardSO : CardSO
{
    public TileProperty[] conditionalTileProperties {get {return _conditionalTileProperties;}}
    [SerializeField] TileProperty[] _conditionalTileProperties;
    public ResourceTypeDefinition[] conditionalResourceGains {get {return _conditionalResourceGains;}}
    [SerializeField] ResourceTypeDefinition[] _conditionalResourceGains;

    public bool CheckCardIf()
    {
        bool passed = true;
        foreach(TileProperty property in conditionalTileProperties)
        {
            //switch passed to be true on default when uncomment this
            //passed = placedOn.CheckForProperty(property);
            if(!passed)
                break;
        }
        return passed;
        //Have to change this so it changes the values in the handler instead
        //This changes the whole SO, meaning for all instances of this card
    }

    public void RunFunction()
    {

    }
}
