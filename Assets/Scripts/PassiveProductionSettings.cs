using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName="PassiveProductionSettings", menuName = "HopeStar/Passive Production Settings Asset")]
public class PassiveProductionSettings : ScriptableObject
{
    public PassiveProductionSetting[] settings;
}

[Serializable]
public struct PassiveProductionSetting
{
    public TileProperty property;
    public ProductionDefinition[] Production;
}

[Serializable]
public struct ProductionDefinition
{
    public GlobalVariableEnum variable;
    public int amount;
}