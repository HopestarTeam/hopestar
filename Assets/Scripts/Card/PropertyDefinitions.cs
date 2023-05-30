using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TilePropertyDefinition
{
    public enum TileProperty
    {
        HOT,
        COLD,
        DRY,
        WET
    }
    public TileProperty property;
}

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