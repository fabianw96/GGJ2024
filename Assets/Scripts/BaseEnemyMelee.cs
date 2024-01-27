using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EEnemyTypeMelee
{
    Indifferent,
    Angry,
}

public class BaseEnemyMelee : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private EEnemyTypeMelee enemyTypeMelee;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float attackActiveDuration;
    [SerializeField] private BoxCollider attackCollider;

    private bool _hasAttacked;

    private void Awake()
    {
        attackCollider.enabled = false;
        
        switch (enemyTypeMelee)
        {
            case EEnemyTypeMelee.Indifferent:
                GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case EEnemyTypeMelee.Angry:
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
        }
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

        if (!agent.Raycast(player.transform.position, out NavMeshHit hit))
        {
            // transform.LookAt(player.transform);
            if (!_hasAttacked && agent.remainingDistance <= agent.stoppingDistance)
            {
                StartCoroutine(nameof(AttackPlayer));
            }
        }
    }

    private IEnumerator AttackPlayer()
    {
        attackCollider.enabled = true;
        _hasAttacked = true;    
        yield return new WaitForSeconds(attackActiveDuration);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(cooldown);
        _hasAttacked = false;
    }
}
