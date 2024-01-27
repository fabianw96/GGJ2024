using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoLevel1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI levelInfo;
    public TextMeshProUGUI enemyInfo;
    public void OnPointerEnter(PointerEventData eventData)
    {
        levelInfo.text = "Level 1 Anger Arena";
        enemyInfo.text = "Neutral & Anger Enemys";
        Debug.Log("Find");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //levelInfo.text = "  ";
        //enemyInfo.text = "  ";
        Debug.Log("Exit");
    }
}
