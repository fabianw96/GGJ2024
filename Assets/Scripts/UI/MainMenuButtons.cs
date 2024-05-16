using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void LevelSelectButton()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.LevelselectScene);
    }

    public void OptionsButton()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.OptionsScene,LoadSceneMode.Additive);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
