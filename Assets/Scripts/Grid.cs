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

    private List<GameObject> m_objects;
    public GameObject this[int i]
    {
        get
        {   
            if(i > m_objects.Capacity || i < 0)
            {
                Debug.LogError("index out of bounds"); 
                return null;
            }
            else return m_objects[i];
        }

        set
        {
            if(i > m_objects.Capacity || i < 0)
            {
                Debug.LogError("no value set: index out of bounds");
            }
            else m_objects[i] = value;
        }
    }
    public GameObject this[int x, int y]
    {
        get
        {
            if(x >= size.x || x < 0 || y >= size.y || y < 0)
            {
                Debug.LogError("index out of range");
                return null;
            }
            return m_objects[x + y*size.x];
        }
        set
        {
            if(x >= size.x || x < 0 || y >= size.y || y < 0)
            {
                Debug.LogError("no value set: index out of range");
            }
            m_objects[x+y*size.x] = value;
        }
    }

    void Start()
    {
        Generate();
    }
    
    public void Generate()
    {
        m_objects = new List<GameObject>(size.x * size.y);
        Debug.Log(m_objects.Capacity);
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
                Debug.Log($"{i * size.x + j}");
                m_objects.Add(current);
            }
            Debug.Log(m_objects.Capacity);
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
