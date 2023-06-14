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

    public bool CheckCardIf(Tile placedOn)
    {
        return placedOn.HasProperties(conditionalTileProperties);
    }
}
