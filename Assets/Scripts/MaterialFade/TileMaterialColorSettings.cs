using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


[Serializable]
public struct TileMaterialColorSettings
{
    public Material defaultMaterial;
    public Color InCompatibleColor;
    public List<RuleMaterial> ruleMaterials;

    public TileMaterialColorSettings(TileMaterialColorSettings settings)
    {
        this.defaultMaterial = new Material(settings.defaultMaterial);
        this.InCompatibleColor = settings.InCompatibleColor;
        this.ruleMaterials = new List<RuleMaterial>(settings.ruleMaterials);
    }

    public Material GetMatchingMaterial(Tile tile)
    {   
        Material bestMatch =
            (from ruleMaterial in ruleMaterials
            where ruleMaterial.HasProperties(tile.tileProperties.ToArray()) && tile.HasProperties(ruleMaterial.associatedProperties.ToArray())
            select ruleMaterial.material).FirstOrDefault();
            if(bestMatch == null)
            {
                Debug.LogWarning($"No matching material found for property combination of \n {string.Join(',',tile.tileProperties)}. returning default material");
                return defaultMaterial;
            }
        //Debug.Log($"Tile property count: {tile.tileProperties.Count} \nMaterial property count: {ruleMaterials[0].associatedProperties.Count}");
        return bestMatch;
    }

    public void Initialize()
    {
        for(int i = 0; i < ruleMaterials.Count; i++)
        {
            ruleMaterials[i].material = new Material(defaultMaterial);
            ruleMaterials[i].material.color = ruleMaterials[i].InitialColor;
            if(ruleMaterials[i].texture != null)ruleMaterials[i].material.mainTexture = ruleMaterials[i].texture;
        }
    }
}

[Serializable]
public class RuleMaterial
{
    [HideInInspector]public Material material;
    public Color InitialColor;
    public Texture2D texture;
    public List<TileProperty> associatedProperties;

    //Returns true if the tile has all of the tile properties
    public bool HasProperties(TileProperty[] properties)
    {
        foreach(TileProperty property in properties)
        {
            if(!associatedProperties.Contains(property))return false;
        }
        return true;
    }

    public bool HasAnyProperty(TileProperty[] properties)
    {
        foreach(TileProperty property in properties)
        {
            if(associatedProperties.Contains(property))return true;
        }
        return false;
    }
}