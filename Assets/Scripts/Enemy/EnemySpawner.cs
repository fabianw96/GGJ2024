using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private BossSpawner bossSpawner;

    private bool _isEnemySpawned;
    private bool _isBossSpawned;
    
    private void Update()
    {
        SpawnEnemy();
        _isBossSpawned = bossSpawner.isBossSpawned;
    }
    
    private void SpawnEnemy()
    {
        if (!_isEnemySpawned && !_isBossSpawned)
        {
            StopCoroutine(nameof(CreateEnemy));
            StartCoroutine(nameof(CreateEnemy));
        }
    }

    private IEnumerator CreateEnemy()
    {
        _isEnemySpawned = true;
        int enemyToSpawn = Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[enemyToSpawn], gameObject.transform);
        yield return new WaitForSeconds(spawnCooldown); 
        _isEnemySpawned = false;
    }
}
