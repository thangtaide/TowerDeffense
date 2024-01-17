using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Start()
    {
        buildingDemolishBtn = transform.Find("BuildingDemolishBtn");
        buildingRepairBtn = transform.Find("BuildingRepairBtn");

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;

        healthSystem.SetHealthAmountMax(buildingType.buildingInfos[0].healthAmountMax, true);

        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
    }

    private void HealthSystem_OnTakeDamage(object sender, System.EventArgs e)
    {
        ShowBuildingRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        if (healthSystem.IsFullHealth())
        {
            HideBuildingRepairBtn();
        }
    }

    private void Update()
    {
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        CamerachineShake.Instance.SharkCamera(10f, .2f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
        DestroyBuilding();
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }
    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn()
    {
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishBtn()
    {
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }

    private void ShowBuildingRepairBtn()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingRepairBtn()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }

    public void DestroyBuilding()
    {
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
