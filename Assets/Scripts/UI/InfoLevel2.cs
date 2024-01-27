using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoLevel2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI levelInfo;
    public TextMeshProUGUI enemyInfo;
    public void OnPointerEnter(PointerEventData eventData)
    {
        levelInfo.text = "Level 2 Depressed Arena";
        enemyInfo.text = "Neutral & Depressed Enemys";
        Debug.Log("Find");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //levelInfo.text = "  ";
        //enemyInfo.text = "  ";
        Debug.Log("Exit");
    }
}
