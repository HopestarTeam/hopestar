using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGuide : MonoBehaviour
{
    Image guide;
    // Start is called before the first frame update
    void Start()
    {
        guide = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!guide.enabled)
            {
            guide.enabled = true;
            }
            else
            {
            guide.enabled = false;
            }
        }

       
    }
}
