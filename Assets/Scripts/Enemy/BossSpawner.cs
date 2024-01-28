using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private EnemyStats bossEnemyPrefab;
    [SerializeField] private EnemySpawner enemySpawner;
    private bool _hasMaxPoints;
    private EnemyStats _bossEnemy;
    public bool isBossSpawned;


    private void Update()
    {
        _hasMaxPoints = GameManager.Instance.HasEnoughPoints;
        SpawnBoss();

        if (_bossEnemy.GetHappiness() <= 0)
        {
            Szeneloader.Instance.LoadScene(SceneIndicies.WinScene);
        }
    }

    private void SpawnBoss()
    {
        if (_hasMaxPoints && !isBossSpawned)
        {
            _bossEnemy = Instantiate(bossEnemyPrefab);
            isBossSpawned = true;
        }
    }
}
