using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    public void Main()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.MenuScene);
    }

    public void Quit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
