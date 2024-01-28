using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float _points = 0f;
    private float _enemyCount;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    

    public float GetPoints()
    {
        return _points;
    }

    public void AddPoints(float amount)
    {
        _points += amount;
    }

    public void AddEnemy()
    {
        _enemyCount++;
    }

    public void RemoveEnemy()
    {
        _enemyCount--;
    }

    public float GetEnemyCount()
    {
        return _enemyCount;
    }
}
