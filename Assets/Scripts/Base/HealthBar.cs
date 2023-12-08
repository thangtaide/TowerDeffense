using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    private Transform barTransform;
    private void Awake()
    {
        barTransform = transform.Find("Bar");
    }
    private void Start()
    {
        healthSystem.OnTakeDamage += HealthSystem_OnChangeHealth;
        healthSystem.OnHeal += HealthSystem_OnChangeHealth;
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnChangeHealth(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible() ;
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1f, 1f);
    }
    private void UpdateHealthBarVisible()
    {
        gameObject.SetActive(!healthSystem.IsFullHealth());
    }
}
