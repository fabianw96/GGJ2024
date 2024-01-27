using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public enum EEnemyTypeMelee
    {
        Indifferent,
        Angry,
    }

    public class BaseEnemyMelee : MonoBehaviour, IDamageableFoe
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Player player;
        [SerializeField] private EEnemyTypeMelee enemyTypeMelee;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private GameObject attackBox;
        [SerializeField] private float damage = 50f;
        [SerializeField] private LayerMask playerLayer;

        [SerializeField] private EnemyStats enemyStats;

        private bool _hasAttacked;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            // attackCollider.enabled = false;
            agent.speed = enemyStats.GetSpeed();
        
        
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
            MeleeHit();
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

        private void MeleeHit()
        {
            Collider[] hitCollider = Physics.OverlapBox(attackBox.transform.position, attackBox.transform.localScale,
                quaternion.identity, playerLayer);

            if (hitCollider.Length == 0 || _hasAttacked) return;
            StartCoroutine(AttackPlayer());
        }


        private IEnumerator AttackPlayer()
        {
            _hasAttacked = true;    
            player.TakeDamage(damage);
            yield return new WaitForSeconds(cooldown);
            _hasAttacked = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackBox.transform.position, attackBox.transform.localScale);
        }

        public void TakeDamage(float takenDamage)
        {
            enemyStats.TakeDamage(takenDamage);
        }
    }
}