using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyData : MonoBehaviour
{
    public static DontDestroyData data;
    private void Awake() {
        if(data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject);
            score = new float[numberOfLevels];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int levelNumber;
    public int numberOfLevels;
    public int levelsCompleted;
    public float[] score;
}
