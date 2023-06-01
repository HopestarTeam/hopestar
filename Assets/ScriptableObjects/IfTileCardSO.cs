using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIfCardProperties", menuName = "ScriptableObjects/New If Card Properties", order = 2)]
public class IfTileCardSO : CardSO
{
    [SerializeField] TileProperty[] conditionalTileProperties;
    [SerializeField] ResourceTypeDefinition[] newResourceGains;

    private void Awake() {
        functionType = FunctionType.IF;
    }

    public void CheckCardIf()
    {
        foreach(TileProperty property in conditionalTileProperties)
        {
            //placedOn.CheckForProperty(property);
        }
    }

    public new void RunCard()
    {
        GameManager.gm.variables.CO2 += emsissionAmount;
    }

    public void RunCardFunction()
    {

    }
}
