using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Return()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.Level1Scene);
    }

    public void Menu()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.MenuScene);
    }

    public void Quit() 
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}