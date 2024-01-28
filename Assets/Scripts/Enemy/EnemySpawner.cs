using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySadPrefab;
    [SerializeField] private GameObject enemyAngryPrefab;
    [SerializeField] private GameObject enemyIndifferentPrefab;
    [SerializeField] private GameObject enemyScaredPrefab;
    [SerializeField] private GameObject bossEnemyAngryPrefab;
    [SerializeField] private float spawnCooldown = 5f;

    private bool _isEnemySpawned;

    private bool _hasMaxPoints;
    private bool _isBossSpawned;
    
    private void Update()
    {
        _hasMaxPoints = GameManager.Instance.HasEnoughPoints;
        SpawnEnemy();
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (_hasMaxPoints && !_isBossSpawned)
        {
            Instantiate(bossEnemyAngryPrefab);
            _isBossSpawned = true;
        }
    }
    
    private void SpawnEnemy()
    {
        if (!_isEnemySpawned && !_isBossSpawned)
        {
            StartCoroutine(nameof(CreateEnemy));
        }
    }

    private IEnumerator CreateEnemy()
    {
        int enemyToSpawn = Random.Range(0, 4);
        switch (enemyToSpawn)
        {
            case 0:
                Instantiate(enemySadPrefab);
                break;
            case 1:
                Instantiate(enemyAngryPrefab);
                break;
            case 2:
                Instantiate(enemyIndifferentPrefab);
                break;
            case 3:
                Instantiate(enemyScaredPrefab);
                break;
        }

        _isEnemySpawned = true;
        yield return new WaitForSeconds(spawnCooldown);
        _isEnemySpawned = false;
    }
}
