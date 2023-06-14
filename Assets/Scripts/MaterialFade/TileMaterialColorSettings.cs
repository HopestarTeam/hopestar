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
        foreach(RuleMaterial rMat in ruleMaterials)
        {
            bool compatible = false;
            foreach(TileProperty rProp in rMat.associatedProperties)
            {
                if(tile.tileProperties.Contains(rProp))compatible = true;
                else
                {
                    compatible = false;
                    break;
                }
            }
            if(compatible)return rMat.material;
        }
        //Debug.Log($"Tile property count: {tile.tileProperties.Count} \nMaterial property count: {ruleMaterials[0].associatedProperties.Count}");
        return defaultMaterial;
    }

    public void Initialize()
    {
        for(int i = 0; i < ruleMaterials.Count; i++)
        {
            ruleMaterials[i].material = new Material(defaultMaterial);
            ruleMaterials[i].material.color = ruleMaterials[i].InitialColor;
        }
    }
}

[Serializable]
public class RuleMaterial
{
    [HideInInspector]public Material material;
    public Color InitialColor;
    public List<TileProperty> associatedProperties;


}