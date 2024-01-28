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
    public void Restart()
    {
        GameManager.Instance.ResetPoints();
        Szeneloader.Instance.LoadScene(Szeneloader.GetCurrentScene(), LoadSceneMode.Single);

    }

    public void Menu()
    {
        if (Szeneloader.Instance == null)
        {
            Debug.Log("Scene");
        }
        Szeneloader.Instance.LoadScene(SceneIndicies.MenuScene);
    }

    public void Quit() 
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
