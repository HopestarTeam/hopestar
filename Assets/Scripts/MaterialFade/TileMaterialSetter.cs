using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NativeClassExtensions;

[RequireComponent(typeof(Grid))]
public class TileMaterialSetter : MonoBehaviour
{
    Grid grid;
    [SerializeField] TileMaterialColorSettingsSO settings;
    public TileMaterialColorSettings runTimeSettings;
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        grid = GetComponent<Grid>();
        settings.settings.Initialize();
        if(settings)
        {
            runTimeSettings = new TileMaterialColorSettings(settings.settings);
        }
        runTimeSettings.Initialize();
        SetMaterials();
    }

    public void SetMaterials()
    {
            Renderer[] rend = new Renderer[grid.count];
            for(int i = 0; i < grid.count; i++)
            {
                rend[i] = grid[i].GetComponentInChildren<Renderer>();
                //Debug.Log(rend[i]);
            }
        for(int i = 0; i < grid.count; i++)
        {
            Tile tile = grid[i].GetComponent<Tile>();
            
                rend[i].sharedMaterial = runTimeSettings.GetMatchingMaterial(tile);
        }
    }

    public void UpdateMaterial(Tile tile)
    {
        Material output = runTimeSettings.GetMatchingMaterial(tile);
        tile.GetComponent<Renderer>().material = output;
    }
    public void GrayIncompatible(CardSO card)
    {
        Debug.Log($"Required: {string.Join(',',card.requiredTileProperties)}");
        Debug.Log($"Forbidden: {string.Join(',',card.blockedTileProperties)}");
        IEnumerable<RuleMaterial> grayed =
        from ruleMaterial in runTimeSettings.ruleMaterials
        where ruleMaterial.HasAnyProperty(card.blockedTileProperties) || !ruleMaterial.HasProperties(card.requiredTileProperties)
        select ruleMaterial;

        Debug.Log($"grayed material count: {grayed.Count()}");
        foreach(RuleMaterial current in grayed)
        {   
            current.material.color = runTimeSettings.InCompatibleColor;
        }
    }

    public void ReturnColor()
    {
        foreach(RuleMaterial current in runTimeSettings.ruleMaterials)
        {
            current.material.color = current.InitialColor;
        }
    }
}

