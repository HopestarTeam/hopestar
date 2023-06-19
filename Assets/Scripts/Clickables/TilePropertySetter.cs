using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TilePropertySetter : MonoBehaviour
{
    public Grid targetGrid;
    public List<TilePropertyArea> tileProperties;

    void Start()
    {
        foreach(TilePropertyArea current in tileProperties)
        {
            for(int x = current.area.x; x < current.area.width + current.area.x && x < targetGrid.size.x; x++)
            for(int y = current.area.y; y < current.area.height + current.area.y && y < targetGrid.size.y; y++)
            {
                targetGrid[x,y].GetComponent<Tile>().tileProperties =  new List<TileProperty>(current.properties);
                targetGrid[x,y].GetComponent<Tile>().CheckForProperties();
            }
        }
    }

    void OnValidate()
    {
        if(targetGrid)tileProperties.Capacity = targetGrid.size.x * targetGrid.size.y;
    }
}

[Serializable]
public struct TilePropertyArea
{
    public RectInt area;

    public List<TileProperty> properties;
}
