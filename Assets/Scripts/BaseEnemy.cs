using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EEnemyType
{
    Indifferent,
    Anger,
    Sad,
    Scared,
}

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private EEnemyType enemyType;
    [SerializeField] private float enemyRange = 5f;

    
    // Start is called before the first frame update

    private void Awake()
    {
        switch (enemyType)
        {
            case EEnemyType.Indifferent:
                GetComponent<MeshRenderer>().material.color = Color.gray;
                agent.stoppingDistance = 1f;
                break;
            case EEnemyType.Anger:
                GetComponent<MeshRenderer>().material.color = Color.red;
                agent.stoppingDistance = 1f;
                break;
            case EEnemyType.Sad:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                agent.stoppingDistance = enemyRange;
                break;
            case EEnemyType.Scared:
                GetComponent<MeshRenderer>().material.color = Color.magenta;
                agent.stoppingDistance = enemyRange * 2f;
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

        
        // if (!agent.Raycast(player.transform.position, out NavMeshHit hit) && agent.remainingDistance <= agent.stoppingDistance)
        // {
        //     transform.LookAt(player.transform);
        //     ShootPlayer();
        //     Debug.Log(hit.mask);
        // }
        agent.Raycast(player.transform.position, out NavMeshHit hit);
        if (hit.mask != playerLayer)
        {
            agent.stoppingDistance -= 1f;
        }
        
    }

    private void ShootPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        
    }
}
