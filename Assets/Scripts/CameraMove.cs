using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    float speed = 10;

    bool panning = false;

    Vector2 PanningMouseStartPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(panning)
        {
            Vector2 MouseDelta = ToNormalizedVectorWithinScreen((Vector2)Input.mousePosition) - ToNormalizedVectorWithinScreen(PanningMouseStartPos);
            transform.Translate(new Vector3(MouseDelta.x, 0, MouseDelta.y) * Time.deltaTime * speed * (MouseDelta * 10).sqrMagnitude*0.1f, Space.World);
            
        }

        if(Input.GetMouseButtonDown(1))
        {
            panning = true;
            Cursor.lockState = CursorLockMode.Confined;
            PanningMouseStartPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(1))
        {
            panning = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }



    Vector2 ToNormalizedVectorWithinScreen(Vector2 input)
    {
        float xPos = Mathf.Clamp01(input.x / Screen.width) * 2 - 1;
        float yPos = Mathf.Clamp01(input.y / Screen.height) * 2 - 1;
        
//        Debug.Log($"{xPos}, {yPos}");

        return Vector2.ClampMagnitude(new Vector2(xPos, yPos), 1);
    }
}
