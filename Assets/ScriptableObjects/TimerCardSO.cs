using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTimerCardProperties", menuName = "ScriptableObjects/New Timer Card Properties", order = 3)]
public class TimerCardSO : CardSO
{
    public TileProperty fromProperty;
    public TileProperty toProperty;

    public bool destroyedOnFinish;
}
