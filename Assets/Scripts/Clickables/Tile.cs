using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    public CardSO card;

    public List<TilePropertyDefinition.TileProperty> tileProperties;

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
        Debug.Log(IsCompatibleWith(card));
    }

    public void OnClick()
    {

    }

    public void OnHoverEnter()
    {

    }
    public void OnHoverExit()
    {

    }
    public void OnHoverStay()
    {

    }
}
