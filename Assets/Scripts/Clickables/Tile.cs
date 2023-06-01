using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    public CardSO card;

    public List<TileProperty> tileProperties;

    //returns true if the tile meets the conditions for the card used as an argument
    public bool IsCompatibleWith(CardSO card)
    {
        foreach(TileProperty property in card.requiredTileProperties)
        {
            if(!tileProperties.Contains(property)) return false;
        }
        foreach(TileProperty property in card.blockedTileProperties)
        {
            if(tileProperties.Contains(property)) return false;
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
