using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIfCardProperties", menuName = "ScriptableObjects/New If Card Properties", order = 2)]
public class IfTileCardSO : CardSO
{
    [SerializeField] TileProperty[] conditionalTileProperties;
    [SerializeField] ResourceTypeDefinition[] newResourceGains;
    ResourceTypeDefinition[] oldResourceGains;

    private void Awake() {
        functionType = FunctionType.IF;
        oldResourceGains = resourceGains;
    }

    public void CheckCardIf()
    {
        bool passed = true;
        foreach(TileProperty property in conditionalTileProperties)
        {
            //switch passed to be true on default when uncomment this
            //passed = placedOn.CheckForProperty(property);
            if(!passed)
                break;
        }

        //Have to change this so it changes the values in the handler instead
        //This changes the whole SO, meaning for all instances of this card
        if(passed)
            resourceGains = newResourceGains;
        else
            resourceGains = oldResourceGains;
    }

    public void RunFunction()
    {
        CheckCardIf();
        Debug.Log("Ran If Variant With " + conditionalTileProperties[0]);
        RunCard();
    }
}
