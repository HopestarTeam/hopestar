using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCoordTester : MonoBehaviour
{
    public Grid grid;

    Renderer[,] rend;
    void Start()
    {
        rend = new Renderer[grid.size.x, grid.size.y];
        for(int x = 0; x < grid.size.x; x++)
        for(int y = 0; y < grid.size.y; y++)
        {
            rend[x,y] = grid[x,y].transform.GetChild(0).GetComponent<Renderer>();
            Material mat = rend[x,y].sharedMaterial;
            rend[x,y].material = new Material(mat);
        }
        for(int x = 0; x < grid.size.x; x++)
        for(int y = 0; y < grid.size.y; y++)
        {
            rend[x,y].material.color = new Color(grid[x,y].transform.localPosition.x / (grid.size.x * grid.tileSize.x),grid[x,y].transform.position.z / (grid.size.y * grid.tileSize.y),0);
        }
    }
}
