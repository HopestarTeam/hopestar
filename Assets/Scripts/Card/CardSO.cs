using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSO : MonoBehaviour
{
    public TilePropertyDefinition[] requiredProperties;
    public TilePropertyDefinition[] blockedProperties;
    public enum TileType
    {
        FOODPRODUCTION,
        RAWPRODUCTION,
        ENERGYPRODUCTION,
        INDUSTRY,
        URBAN
    }
    public ResourceTypeDefinition[] resourceCosts;
    public ResourceTypeDefinition[] resourceGains;
    public int emsissionAmount;
    public TileType tileType;
    public int tileTimer;
}
