using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthAmountMaxChanged;
    public event EventHandler OnTakeDamage;
    public event EventHandler OnHeal;
    public event EventHandler OnDied;
    [SerializeField] int healthAmountMax;
    private int healthAmount;
    private int healAmount;
    float healTimer, timer;
    int healPerSecond;
    private void Awake()
    {
        healthAmount = healthAmountMax;
        healAmount = 0;
        healTimer = 0f;
    }

    private void Update()
    {
        if (healAmount > 0)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                int heal = (int)(healPerSecond * timer);
                healTimer -= timer;
                timer = 0f;
                if (healAmount < heal || healTimer < 0)
                {
                    HealAmount(healAmount);
                    healAmount = 0;
                }
                else
                {
                    HealAmount(heal);
                    healAmount -= heal;
                }
            }
        }
    }

    public bool IsHealing()
    {
        return healAmount >0;
    }

    private int HealPerSecond(float timer)
    {
        return (int)(healthAmountMax / timer);
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

    public void HealAmount(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnHeal?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull(float timer)
    {
        healPerSecond = HealPerSecond(timer);
        healAmount = healthAmountMax - healthAmount;
        healTimer = (float) healAmount / healthAmountMax * timer;
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
    public int GetHealthAmountMax()
    {
        return healthAmountMax;
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

        OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
    }
}
