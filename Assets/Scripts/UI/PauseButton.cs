using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void OptionsButton()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.OptionsScene, LoadSceneMode.Additive);
    }
    public void PlayButton()
    {
        Szeneloader.Instance.UnLoadScene(SceneIndicies.PauseScene);
    }
    public void QuitButton()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
