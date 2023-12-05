using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnTakeDamage;
    public event EventHandler OnDied;
    [SerializeField] int healthAmountMax;
    private int healthAmount;
    private void Awake()
    {
        healthAmount = healthAmountMax;
    }
    public void TakeDamage(int damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }
    public bool IsDead()
    {
        return healthAmount == 0;
    }
    public int GetHealthAmount()
    {
        return healthAmount;
    }
    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount/healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if(updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }
    }
}
