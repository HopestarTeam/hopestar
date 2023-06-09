using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePropertySetter : MonoBehaviour
{
    public Grid targetGrid;
    public List<TilePropertyPreset> tileProperties;

    void Start()
    {
        int i = 0;
        foreach(GameObject current in targetGrid)
        {
            Tile CurrentTile = current.GetComponent<Tile>();
            if(CurrentTile != null)
            {
                CurrentTile.tileProperties = tileProperties[i%tileProperties.Count].properties;
            }
            i++;
        }
    }

    void OnValidate()
    {
        if(targetGrid)tileProperties.Capacity = targetGrid.size.x * targetGrid.size.y;
    }
}
