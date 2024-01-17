using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField]private ResourceTypeSO goldResource;

    private void Awake()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
        BuildingTypeHolder holder = GetComponentInParent<BuildingTypeHolder>();
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!healthSystem.IsHealing())
            {
                int healAmount = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
                int repairCost = healAmount / 2;

                ResourceAmount[] resourceAmountRepairCost =  new ResourceAmount[]{
                    new ResourceAmount{ resourceType = goldResource, amount = repairCost },
                };

                if(ResourceManagerInstance.Instance.CanAfford(resourceAmountRepairCost))
                {
                    healthSystem.HealFull(holder.buildingType.buildingInfos[0].constructionTimerMax);
                    ResourceManagerInstance.Instance.SpendResource(resourceAmountRepairCost);
                }
                else
                {
                    TooltipUI.Instance.Show("Cannot afford repair cost!\n" +
                        "Need: <color=#" + goldResource.colorHex + ">" +
                         repairCost + " " + goldResource.nameShort +
                        "</color>",
                        new TooltipUI.TooltipTimer { timer = 2f});
                }
            }
        });
    }
}
