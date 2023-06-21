using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0,Screen.currentResolution.height*0.75f,Screen.currentResolution.width,Screen.currentResolution.height*0.25f));
        foreach(KeyValuePair<GlobalVariableEnum, GlobalVariables.ResourceVariable> current in GameManager.gm.variables.variables)
        {
            GUILayout.Label($"{current.Key}:\tproduction {current.Value.production}, spent {current.Value.spent}, upkeep {current.Value.upkeep}");
        }
        GUILayout.EndArea();
    }
}
