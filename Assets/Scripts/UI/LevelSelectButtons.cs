using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectButtons : MonoBehaviour
{
    public static SceneIndicies currentScene;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public SceneIndicies selectedScene;

    public void Tutorial()
    {
        selectedScene = SceneIndicies.Level0Scene;
    }

    public void Level1()
    {
        selectedScene = SceneIndicies.Level1Scene;
    }

    public void Level2()
    {
        selectedScene = SceneIndicies.Level2Scene;
    }

    public void Level3()
    {
        selectedScene = SceneIndicies.Level1Scene;
    }

    public void Go()
    {
        currentScene = selectedScene;
        Szeneloader.Instance.LoadScene(selectedScene);
        
    }

    public void Back()
    {
       Szeneloader.Instance.LoadScene(SceneIndicies.MenuScene);
    }

  

   
}
