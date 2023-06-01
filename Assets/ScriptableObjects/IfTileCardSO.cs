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
            //passed = placedOn.CheckForProperty(property);
            if(!passed)
                break;
        }

        if(passed)
            resourceGains = newResourceGains;
        else
            resourceGains = oldResourceGains;
    }

    public new void RunCard()
    {
        Debug.Log("Ran If Variant With " + conditionalTileProperties[0]);
    }
}
