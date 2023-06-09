using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugResourceManagement : MonoBehaviour
{
    int energyProduction = 5;
    int energySpent = 0;    //include a line in 'end turn' function to reset all resources spent to 0   !!!!!!!!!!!!!!!!
    int energyInUpkeep = 0;

    public int GetEnegyProduction(){return energyProduction;}
    public int GetEnegySpent(){return energySpent;}
    public int GetEnegyInUpkeep(){return energyInUpkeep;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){energyProduction++;}
        if(Input.GetKeyDown(KeyCode.D)){energyProduction--;}
        if(Input.GetKeyDown(KeyCode.R)){energySpent++;}
        if(Input.GetKeyDown(KeyCode.F)){energySpent--;}
        if(Input.GetKeyDown(KeyCode.T)){energyInUpkeep++;}
        if(Input.GetKeyDown(KeyCode.G)){energyInUpkeep--;}
    }
}
