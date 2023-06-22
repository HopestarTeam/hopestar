using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesBackgroundScaler : MonoBehaviour
{
    [SerializeField]  GameObject objectives;
    private void ScaleBackground(){
        int numberOfObjectives = objectives.GetComponent<Objectives>().numberOfObjectives + 1;  //we need the extra 1 to display "Produsce:"
        RectTransform rectT = GetComponent<RectTransform>();
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50 * numberOfObjectives + 60);   
    }

    // Start is called before the first frame update
    void Start()
    {
        ScaleBackground();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
