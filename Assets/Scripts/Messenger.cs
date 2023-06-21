using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Messenger : MonoBehaviour
{
    public string scene = "InitializerProto";
    public bool levelInitialized = false;
    public static Messenger msngr;
    private LevelData level;
    public void Start()
    {
        msngr = this;
    }

    public void ChangeLevel(LevelData level)
    {
        StartCoroutine(LoadLevel(level));
    }

    void OnLoadScene(Scene scene, LoadSceneMode mode)
        {
            Initializer initializer = FindObjectOfType<Initializer>();
            initializer.Initialize(this.level);
            levelInitialized = true;
        }

    IEnumerator LoadLevel(LevelData level)
    {
        this.level = level;   
        SceneManager.sceneLoaded += OnLoadScene;
        SceneManager.LoadScene(scene);
        yield return null;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadScene;
    }


}
