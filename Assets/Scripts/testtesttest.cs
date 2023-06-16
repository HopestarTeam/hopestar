using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtesttest : MonoBehaviour
{
    public struct FancyList{
        public int first;
        public int second;
        public int third;

        public FancyList(int first, int second, int third){
            this.first = first;
            this.second = second;
            this.third = third;
        }

        public void AddToFirst(int value){
            first += value;
        }

        public void Print(){
            Debug.Log($"{first}, {second}, {third}");
        }
    }

    Dictionary<string, FancyList> theDictionary;
    FancyList theList;

    // Start is called before the first frame update
    void Start()
    {
        theList = new FancyList(1, 1, 1);
        theDictionary = new Dictionary<string, FancyList>(){{"structureOne", new FancyList(1, 1, 1)}};
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            theList.Print();
            theList.AddToFirst(5);
            Debug.Log("i was called outside dictionary");
            theList.Print();  
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            theDictionary["structureOne"].Print();
            theDictionary["structureOne"].AddToFirst(5);
            Debug.Log("i was called inside dictionary");
            theDictionary["structureOne"].Print();  
        }
    }
}
