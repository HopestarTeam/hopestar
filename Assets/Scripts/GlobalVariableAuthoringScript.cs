using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script for initializing the global variables at the beginning (This is necessary, because unity's inspector doesn't allow us to author dictionaries)
public class GlobalVariableAuthoringScript : MonoBehaviour
{
    public List<AuthoringStruct> values;
    public void Author()
    {
        foreach(AuthoringStruct current in values)
        {
            GameManager.gm.variables.variables[current.variable] = current.initialValue;
        }
        Destroy(this);
    }

    [Serializable]
    public struct AuthoringStruct
    {
        public GlobalVariableEnum variable;
        public float initialValue;
    }
}
