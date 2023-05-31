using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSO : MonoBehaviour
{
    public string cardName;
    public TileType tileType;
    public TilePropertyDefinition[] requiredProperties;
    public TilePropertyDefinition[] blockedProperties;
    public ResourceTypeDefinition[] resourceCosts;
    public ResourceTypeDefinition[] resourceUpkeep;
    public ResourceTypeDefinition[] resourceGains;
    public enum TileType
    {
        PRODUCTION,
        INDUSTRY,
        URBAN
    }
    public int emsissionAmount;
    public bool hasFunction;
    public int tileTimer;
}
