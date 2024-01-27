using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int maxHappiness;
    [SerializeField] protected int happiness;
    [SerializeField] protected int charaterSpeed;
    [SerializeField] protected bool isDead;

    private void Start()
    {
        InitStats();
    }
    protected virtual void CheckHealth() 
    {
        if (happiness < 0)
        {
            happiness = 0;
            Die();
        }
        if (happiness > maxHappiness)
        {
            happiness = maxHappiness;
        }
    }
    public virtual void Die()
    {
        isDead = true;
    }

    protected virtual void SetHappinessTo(int happinessToSetTo)
    {
        happiness = happinessToSetTo;
    }

    public virtual void TakeDamage(int amount) 
    {
        int happinessAfterDamage = happiness - amount;
        SetHappinessTo(happinessAfterDamage);
    }

    public virtual void Heal(int heal)
    {
        int happinessAfterHeal = happiness + heal;
        SetHappinessTo(happinessAfterHeal);
    }

    public void InitStats()
    {
        maxHappiness = 150;
        SetHappinessTo(maxHappiness);
        isDead = false;
    }

}
