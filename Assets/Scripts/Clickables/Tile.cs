using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    public List<CardSO> cards;

    public List<TilePropertyDefinition.TileProperty> tileProperties;

    //returns true if the tile meets the conditions for the card used as an argument
    bool IsCompatibleWith(CardSO card)
    {
        foreach(TilePropertyDefinition definition in card.requiredProperties)
        {
            if(!tileProperties.Contains(definition.property)) return false;
        }
        foreach(TilePropertyDefinition definition in card.blockedProperties)
        {
            if(tileProperties.Contains(definition.property)) return false;
        }
        return true;
    }

    void Start()
    {
        foreach(CardSO card in cards)
        {
            Debug.Log(IsCompatibleWith(card));
        }
    }

    public void OnClick()
    {

    }

    public void OnHoverEnter()
    {
        GameManager.gm.menuManager.toolTip.visible = true;
        string tilePropetyToolTip = "";
        foreach(TilePropertyDefinition.TileProperty property in tileProperties)
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
