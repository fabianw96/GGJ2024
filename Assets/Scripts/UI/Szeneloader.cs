using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndicies
{
    InitScene = 0,
    LevelScene,
    MenuScene = 2,
    LevelselectScene,
    PauseScene,
    OptionsScene,
    Level0Scene,
    Level1Scene,
    Level2Scene,
    Level3Scene,
    Fabian


}
public class Szeneloader : MonoBehaviour
{
    [SerializeField]
    private SceneIndicies startScene;
    public static Szeneloader Instance;

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

    }
    private void Start()
    {
        LoadScene(startScene);
    }

    public void LoadScene(SceneIndicies _indicies, LoadSceneMode _mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene((int)_indicies, _mode);
    }

    public void UnLoadScene(SceneIndicies _indicies)
    {
        SceneManager.UnloadSceneAsync((int)_indicies);
    }
}
