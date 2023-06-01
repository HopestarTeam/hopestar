using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileProperty
{
    HOT,
    COLD,
    DRY,
    WET,
    URBAN,
    INDUSTRY
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
        CONSUMER
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