using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


public enum EEnemyTypeRanged
{
    Sad,
    Scared,
}

public class BaseEnemyRanged : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Player player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform projectileSpawn;

    [SerializeField] private EEnemyTypeRanged enemyTypeRanged;
    [SerializeField] private float enemyRange = 5f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float shotDelay = 5f;
    [SerializeField] private float shotDecay = 5f;
    private float _enemyStoppingDistance;
    private bool _hasShot;
    
    private void Awake()
    {
        switch (enemyTypeRanged)
        {
            case EEnemyTypeRanged.Sad:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                _enemyStoppingDistance = enemyRange;
                break;
            case EEnemyTypeRanged.Scared:
                GetComponent<MeshRenderer>().material.color = Color.magenta;
                _enemyStoppingDistance = enemyRange * 2f;
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

        if (!agent.Raycast(player.transform.position, out NavMeshHit hit))
        {
            agent.stoppingDistance = _enemyStoppingDistance;
            transform.LookAt(player.transform);
            if (!_hasShot)
            {
                StartCoroutine(nameof(ShootPlayer));
            }
        }
    }

    private IEnumerator ShootPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), projectileSpawn.parent.GetComponent<Collider>());
        bullet.transform.position = projectileSpawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x , transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyProjectile(bullet, shotDecay));
        _hasShot = true;
        yield return new WaitForSeconds(shotDelay);
        _hasShot = false;
    }
    
    private IEnumerator DestroyProjectile (GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }
}
