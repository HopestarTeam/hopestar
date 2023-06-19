using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDisplayScaler : MonoBehaviour
{
    [SerializeField]  GameObject objectives;
    [SerializeField] GameObject objectiveStrip;

    private void ScaleDisplay(){
        int numberOfObjectives = objectives.GetComponent<Objectives>().numberOfObjectives;
        RectTransform rectT = GetComponent<RectTransform>();
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50 * numberOfObjectives);   
    }

    private void InstantiateObjectiveStrips(){
        foreach (var obj in objectives.GetComponent<Objectives>().listOfObjectives){
            GameObject newResourceStrip = Instantiate<GameObject>(objectiveStrip);
            newResourceStrip.transform.SetParent(transform);

            newResourceStrip.GetComponent<ObjectiveStrip>().SetPropertiesAs(obj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ScaleDisplay();
        InstantiateObjectiveStrips();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
