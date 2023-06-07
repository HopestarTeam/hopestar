using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardHandler))]
public class Tile : MonoBehaviour, IClickable
{
    public CardHandler cardHandler;

    public List<TileProperty> tileProperties;

    //returns true if the tile meets the conditions for the card used as an argument
    public bool IsCompatibleWith(CardSO card)
    {
        bool result = true;
        if(card.blockedTileProperties != null)
            result = !HasProperties(card.blockedTileProperties);
        if(card.requiredTileProperties != null)
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

    public void OnClick()
    {

    }

    public void OnHoverEnter()
    {
        GameManager.gm.menuManager.toolTip.visible = true;
        string tilePropetyToolTip = "";
        foreach(TileProperty property in tileProperties)
        {
            tilePropetyToolTip += $"{property} \n";
        }
        GameManager.gm.menuManager.toolTip.text = tilePropetyToolTip;
    }

    public void OnHoverExit()
    {
        GameManager.gm.menuManager.toolTip.visible = false;
    }
    
    public void OnHoverStay()
    {

    }
}
