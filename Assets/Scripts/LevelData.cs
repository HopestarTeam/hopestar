using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "HopeStar/LevelData", fileName = "new LevelData")]
public class LevelData : ScriptableObject
{
    public GridInitData grid;

    public List<GlobalVariableAuthoringScript.AuthoringStruct> initialVariableValues;
    
    public List<GlobalVariableAuthoringScript.AuthoringStruct> objectives;

    public List<CardSO> Deck;
    
    [Serializable]
    public struct GridInitData
    {
        public Vector2Int gridSize;
        public List<TilePropertyArea> gridTileProperties;
    }
}
