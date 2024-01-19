using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    static Building selectedBuilding = null;
    private HealthSystem healthSystem;
    BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;
    private Transform buildingLevelUpBtn;
    private LevelController levelController;
    private void Awake()
    {
    }

    private void Start()
    {
        buildingDemolishBtn = transform.Find("BuildingDemolishBtn");
        buildingRepairBtn = transform.Find("BuildingRepairBtn");
        buildingLevelUpBtn = transform.Find("LevelUpBtn");
        levelController = transform.Find("LevelController").GetComponent<LevelController>();

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
        levelController.OnLevelUpEvent += LevelController_OnLevelUp;

        UpdateInfoBuilding(levelController.Lv, true);

        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
        HideLevelUpBuildingBtn();
    }

    private void LevelController_OnLevelUp(object sender, System.EventArgs e)
    {
        // effect
        UpdateInfoBuilding(levelController.Lv, false);
    }

    private void ShowLevelUpBuildingBtn()
    {
        if (buildingLevelUpBtn != null)
        {
            buildingLevelUpBtn.gameObject.SetActive(true);
        }
    }

    private void HideLevelUpBuildingBtn()
    {
        if (buildingLevelUpBtn != null)
        {
            buildingLevelUpBtn.gameObject.SetActive(false);
        }
    }

    private void UpdateInfoBuilding(int lv, bool updateHealthAmout)
    {
        healthSystem.SetHealthAmountMax(buildingType.buildingInfos[lv-1].healthAmountMax, updateHealthAmout);

        // sprite, pfVisual...
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

    private void OnMouseDown()
    {
        ShowLevelUpBuildingBtn();
        if (selectedBuilding != null && selectedBuilding != this)
        {
            selectedBuilding.HideLevelUpBuildingBtn();
        }
        selectedBuilding = this;
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
