using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] GameObject levelButtonsRoot;
    Button[] levelButtons;
    
    private void Start() {
        levelButtons = levelButtonsRoot.GetComponentsInChildren<Button>();
        if(levelButtons.Length != DontDestroyData.data.numberOfLevels)
            Debug.LogWarning($"Number of buttons not the same as number of levels!\nNumber of buttons: {levelButtons.Length} ; Number of levels according to DontDestroyData: {DontDestroyData.data.numberOfLevels}");
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i<=DontDestroyData.data.levelsCompleted)
                levelButtons[i].interactable = true;
            else
                levelButtons[i].interactable = false;
        }
    }

    public void GoToLevel(int number)
    {
        SceneManager.LoadScene("level_"+number);
    }
}
