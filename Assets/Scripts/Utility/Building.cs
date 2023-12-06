using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;

    private void Start()
    {
        buildingDemolishBtn = transform.Find("BuildingDemolishBtn");

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += HealthSystem_OnDied;

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);

        HideBuildingDemolishBtn();

    }
    private void Update()
    {
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
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
        if(buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
}
