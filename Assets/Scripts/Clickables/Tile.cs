using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CardHandler))]
public class Tile : MonoBehaviour
{
    public CardHandler cardHandler;

    public List<TileProperty> tileProperties;

    //returns true if the tile meets the conditions for the card used as an argument
    public bool IsCompatibleWith(CardSO card)
    {
        bool result = true;
        if(card.blockedTileProperties.Length != 0)
            result = !HasProperties(card.blockedTileProperties);
        if(card.requiredTileProperties.Length != 0 && result)
            result = HasProperties(card.requiredTileProperties);
        return result;
    }

    //Returns true if the tile has the property
    public bool HasProperty(TileProperty property)
    {
        return tileProperties.Contains(property);
    }

    //Returns true if the tile has all of the tile properties
    public bool HasProperties(TileProperty[] properties)
    {
        foreach(TileProperty property in properties)
        {
            if(!tileProperties.Contains(property))return false;
        }
        return true;
    }

    public void ResolveTile()
    {
        if(GameManager.gm.tilePassiveProductionSettings)
        foreach(PassiveProductionSetting current in GameManager.gm.tilePassiveProductionSettings.settings)
        {
            if(tileProperties.Contains(current.property))
            {
                foreach(ProductionDefinition prod in current.Production)
                {
                    if(GameManager.gm.variables.variables[prod.variable].production + prod.amount < 0)
                        GameManager.gm.variables.variables[prod.variable].production = 0;
                    else
                    GameManager.gm.variables.variables[prod.variable].production += prod.amount;
                }
            }
        }
    }

    public void OnMouseEnter()
    {
        if(!GameManager.gm.menuManager.OnElement)
        {
            GameManager.gm.menuManager.toolTip.visible = true;
            string tilePropetyToolTip = "";
            foreach(TileProperty property in tileProperties)
            {
                tilePropetyToolTip += $"{property} \n";
            }
            GameManager.gm.menuManager.toolTip.text = tilePropetyToolTip;
        }
    }

    public void OnMouseStay()
    {
        if(GameManager.gm.menuManager.OnElement)
        {
            OnMouseExit();
        }
    }

    public void OnMouseExit()
    {
        GameManager.gm.menuManager.toolTip.visible = false;
    }

    public void CheckForProperties()
    {
        foreach(TileProperty tile in tileProperties)
        {
            if (tile == TileProperty.INDUSTRY)
            {
                // Instantiate prefab above tile
                Instantiate (GameManager.gm.variables.IndustryOverlay,transform.position+ new Vector3(0,0.001f,0),transform.rotation,transform);
            }
            
            if (tile == TileProperty.URBAN)
            {
                Instantiate (GameManager.gm.variables.UrbanOverlay,transform.position+ new Vector3(0,0.001f,0),transform.rotation,transform);
            }

            if (tile == TileProperty.RESOURCERICH)
            {
                Instantiate (GameManager.gm.variables.MineralOverlay,transform.position+ new Vector3(0,0.001f,0),transform.rotation,transform);
            }




        }
    }
  
}

[Serializable]
public struct TilePropertyPreset
{
    public List<TileProperty> properties;
}

