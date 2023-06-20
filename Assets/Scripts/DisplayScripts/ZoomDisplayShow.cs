using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomDisplayShow : MonoBehaviour
{
    [SerializeField] Image frame;
    [SerializeField] Image picture;

    private void ChangeDisplayStatus(){
        frame.enabled = !frame.enabled;
        picture.enabled = !picture.enabled; 
    }

    private void WhenClicked(){
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1)){
            ChangeDisplayStatus();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeDisplayStatus();
    }

    // Update is called once per frame
    void Update()
    {
        WhenClicked();
    }
}
