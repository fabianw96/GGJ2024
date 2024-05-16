using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Main()
    {
        Szeneloader.Instance.LoadScene(SceneIndicies.MenuScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
