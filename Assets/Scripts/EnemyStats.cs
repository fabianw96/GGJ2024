using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private bool isBoss;
    public override void Die()
    {
        if (isBoss)
        {
            Debug.Log("YOU WON!");
        }
        base.Die();
        GameManager.Instance.AddPoints(50f);
        Destroy(gameObject);
    }
}
