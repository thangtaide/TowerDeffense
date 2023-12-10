using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] int healthAmountPerSeparator = 10;

    private Transform barTransform;
    private Transform separatorTemplate;
    private Transform separatorContainer;
    private void Awake()
    {
        barTransform = transform.Find("Bar");
        separatorContainer = transform.Find("SeparatorContainer");
    }

    private void Start()
    {
        separatorTemplate = separatorContainer.Find("SeparatorTemplate");
        separatorTemplate.gameObject.SetActive(false);
        
        ConstructHealthBarSeparator();

        healthSystem.OnTakeDamage += HealthSystem_OnChangeHealth;
        healthSystem.OnHeal += HealthSystem_OnChangeHealth;
        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
        UpdateBar();
        UpdateHealthBarVisible();
        gameObject.SetActive(true);
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e)
    {
        ConstructHealthBarSeparator();
    }

    private void ConstructHealthBarSeparator()
    {
        foreach(Transform separatorTransform in separatorContainer)
        {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }

        float barSize = transform.Find("Bar").transform.Find("Bar").localScale.x;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();

        int healthSeparatorCount = healthSystem.GetHealthAmountMax() / healthAmountPerSeparator;
        for (int i = 1; i < healthSeparatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
        }
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
