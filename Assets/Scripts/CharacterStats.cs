using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class CharacterStats : MonoBehaviour
{
    [SerializeField] protected float maxHappiness;
    [SerializeField] protected float happiness;
    [SerializeField] protected float characterSpeed;
    [SerializeField] protected bool isDead;

    private void Start()
    {
        InitStats();
    }

    private void Update()
    {
        CheckHealth();
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
        Debug.Log(gameObject.name + " has died.");
    }

    protected virtual void SetHappinessTo(float happinessToSetTo)
    {
        happiness = happinessToSetTo;
    }

    public virtual void TakeDamage(float amount) 
    {
        float happinessAfterDamage = happiness - amount;
        SetHappinessTo(happinessAfterDamage);
    }

    public virtual void Heal(int heal)
    {
        float happinessAfterHeal = happiness + heal;
        SetHappinessTo(happinessAfterHeal);
    }

    public void InitStats()
    {
        maxHappiness = 150;
        SetHappinessTo(maxHappiness);
        isDead = false;
    }

    public float GetSpeed()
    {
        return characterSpeed;
    }

    public float GetHappiness()
    {
        return happiness;
    }

}
