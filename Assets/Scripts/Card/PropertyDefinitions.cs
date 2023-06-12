using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileProperty : byte
{
    HOT,
    COLD,
    DRY,
    WET,
    URBAN,
    INDUSTRY,
    RESOURCERICH
}

/*[Serializable]
public class TilePropertyDefinition
{
    public TileProperty property;
}*/

[Serializable]
public class ResourceTypeDefinition
{
    public enum ResourceType
    {
        ENERGY,
        RAW,
        FOOD,
        INDUSTRY,
        CONSUMER,
        RESEARCH
    }
    public ResourceType resourceType;
    public int amount;
}

public enum CardType
{
    ENERGY,
    RAW,
    FOOD,
    INDUSTRY,
    URBAN
}