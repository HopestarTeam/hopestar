using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject main, levelSelect;
    private void Start() {
        SwitchToMain();
    }
    public void SwitchToLevelSelect()
    {
        main.SetActive(false);
        levelSelect.SetActive(true);
    }
    public void SwitchToMain()
    {
        main.SetActive(true);
        levelSelect.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
