using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    GameObject instantiable;

    public Vector2 tileSize;
    public Vector2Int size;

    public Vector3 offset;

    void Start()
    {
        Generate();
    }
    
    public void Generate()
    {
        for(int i = 0; i < size.y; i++)
        {
            for(int j = 0; j < size.x; j++)
            {
                GameObject current;
                if(!instantiable) current = new GameObject();
                else current = GameObject.Instantiate(instantiable);
                current.name = $"GridElement {i}, {j}";
                current.transform.parent = transform;
                Vector3 offsetPos = new Vector3(tileSize.x * j, 0, tileSize.y * i) + offset;
                current.transform.localPosition = offsetPos;
                current.transform.rotation = transform.rotation;
                Debug.Log(current.transform.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(tileSize.x * 0.5f, 0, tileSize.y * 0.5f));
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        DrawGridGizmo();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(tileSize.x * 0.5f, 0, tileSize.y * 0.5f));
        Gizmos.color = Color.white;
        DrawGridGizmo();
       
    }

    private void DrawGridGizmo()
    {
        //Fixing grid display for the gizmos
        Vector3 RotatedOffset = transform.right * offset.x; 
        RotatedOffset += transform.up * offset.y;
        RotatedOffset += transform.forward * offset.z;

        Vector3 RotatedTileSizeOffset = transform.right * tileSize.x * 0.5f;
        RotatedTileSizeOffset += transform.forward * tileSize .y * 0.5f;
    

        //First the vertical lines
        for(int i = 0; i <= size.x; i++)
        {
            Vector3 lineStart = transform.position;
            lineStart += transform.right * tileSize.x * i + RotatedOffset;
            lineStart -= RotatedTileSizeOffset;
            Vector3 lineEnd = lineStart;
            lineEnd += transform.forward * tileSize.y * size.y;
            Gizmos.DrawLine(lineStart, lineEnd);
        }

        //Then the horizontal ones
        for(int i = 0; i <= size.y; i++)
        {
            Vector3 lineStart = transform.position; 
            lineStart += transform.forward * tileSize.y * i + RotatedOffset;
            lineStart -= RotatedTileSizeOffset;
            Vector3 lineEnd = lineStart;
            lineEnd += transform.right * tileSize.x * size.x;
            Gizmos.DrawLine(lineStart, lineEnd);
        }
    }

    
}
