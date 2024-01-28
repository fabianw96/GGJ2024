using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectButtons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public string selectedScene;

    public void Tutorial()
    {
        selectedScene = "Level 0 Scene";
    }

    public void Level1()
    {
        selectedScene = "Level 1 Scene";
    }

    public void Level2()
    {
        selectedScene = "Level 2 Scene";
    }

    public void Level3()
    {
        selectedScene = "Level 3 Scene";
    }

    public void Go()
    {
        SceneManager.LoadScene(selectedScene);

    }

    public void Back()
    {
       Szeneloader.Instance.LoadScene(SceneIndicies.MenuScene);
    }

  

   
}
