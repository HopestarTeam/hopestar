using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GlobalVariables
{   
    public GameObject IndustryOverlay;
    public GameObject UrbanOverlay;
    public GameObject MineralOverlay;
    [Serializable] public struct ResourceVariable{
        //structure that collects the production, upkeep and spent values of a single resource type
        public int production;
        public int upkeep;
        public int spent;


        public ResourceVariable(int production, int upkeep, int spent){
            this.production = production;
            this.upkeep = upkeep;
            this.spent = spent;
        }

        public int GetSurplus(){
            return production - upkeep - spent;
        }
    }

    public Dictionary<GlobalVariableEnum, ResourceVariable> variables;
    public void Initialize()
    {
        //Get an array of the enums
        GlobalVariableEnum[] enums = (GlobalVariableEnum[])Enum.GetValues(typeof(GlobalVariableEnum));
        
        variables = new Dictionary<GlobalVariableEnum, ResourceVariable>(enums.Length);
        foreach(GlobalVariableEnum current in enums)
        {
            variables.Add(current, new ResourceVariable(0, 0, 0));
            //Debug.Log($"{current}: {variables[current]}");
        }
    }

    

    /*public float CO2
    {
        get
        {
            return variables[GlobalVariableEnum.CO2];
        }
        set
        {
            variables[GlobalVariableEnum.CO2] = value;
        }
    }
    public float RawResources
    {
        get
        {
            return variables[GlobalVariableEnum.RawResources];
        }
        set
        {
            variables[GlobalVariableEnum.RawResources] = value;
        }
    }
    public float Food
    {
        get
        {
            return variables[GlobalVariableEnum.Food];
        }
        set
        {
            variables[GlobalVariableEnum.Food] = value;
        }
    }
    public float Energy
    {
        get
        {
            return variables[GlobalVariableEnum.Energy];
        }
        set
        {
            variables[GlobalVariableEnum.Energy] = value;
        }
    }
    public float ConsumerGoods
    {
        get
        {
            return variables[GlobalVariableEnum.ConsumerGoods];
        }
        set
        {
            variables[GlobalVariableEnum.ConsumerGoods] = value;
        }
    }
    public float IndustryGoods
    {
        get
        {
            return variables[GlobalVariableEnum.IndustryGoods];
        }
        set
        {
            variables[GlobalVariableEnum.IndustryGoods] = value;
        }
    }
    public float RawResourcesUpkeep
    {
        get
        {
            return variables[GlobalVariableEnum.RawResourcesUpkeep];
        }
        set
        {
            variables[GlobalVariableEnum.RawResourcesUpkeep] = value;
        }
    }
    public float FoodUpkeep
    {
        get
        {
            return variables[GlobalVariableEnum.FoodUpkeep];
        }
        set
        {
            variables[GlobalVariableEnum.FoodUpkeep] = value;
        }
    }
    public float EnergyUpkeep
    {
        get
        {
            return variables[GlobalVariableEnum.EnergyUpkeep];
        }
        set
        {
            variables[GlobalVariableEnum.EnergyUpkeep] = value;
        }
    }
    public float ConsumerGoodsUpkeep
    {
        get
        {
            return variables[GlobalVariableEnum.ConsumerGoodsUpkeep];
        }
        set
        {
            variables[GlobalVariableEnum.ConsumerGoodsUpkeep] = value;
        }
    }
    public float IndustryGoodsUpkeep
    {
        get
        {
            return variables[GlobalVariableEnum.IndustryGoodsUpkeep];
        }
        set
        {
            variables[GlobalVariableEnum.IndustryGoodsUpkeep] = value;
        }
    }

    public float CitizenUnrest
    {
        get
        {
            return variables[GlobalVariableEnum.CitizenUnrest];
        }
        set
        {
            variables[GlobalVariableEnum.CitizenUnrest] = value;
        }
    }
    public float CO2Upkeep
    {
        get
        {
            return variables[GlobalVariableEnum.CO2Upkeep];
        }
        set
        {
            variables[GlobalVariableEnum.CO2Upkeep] = value;
        }
    }
    public float CitizenUnrestUpkeep
    {
        get
        {
            return variables[GlobalVariableEnum.CitizenUnrestUpkeep];
        }
        set
        {
            variables[GlobalVariableEnum.CitizenUnrestUpkeep] = value;
        }
    }*/
}

public enum GlobalVariableEnum
{
    CO2,
    Food,
    Energy,
    Material,
    Industry,
    Consumer,
    Science
}
