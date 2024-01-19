using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class BuildingLevelUpBtn : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float canLvUpAlpha, canNotLvUpAlpha;
    private LevelController levelController;
    private Image buttonImage, buttonBackground;
    private BuildingTypeHolder holder;
    private int lv;

    private void Start()
    {
        buttonBackground = transform.Find("Button").GetComponent<Image>();
        buttonImage = transform.Find("Button").transform.Find("Image").GetComponent<Image>();
        levelController = transform.parent.Find("LevelController").GetComponent<LevelController>();
        lv = levelController.Lv;
        holder = GetComponentInParent<BuildingTypeHolder>();

        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {

            if (CanUpgradeBuilding())
            {
                UpgradeBuilding();
            }
            else if(!CanAfford())
            {
                TooltipUI.Instance.Show("Cannot afford level up cost!\n" +
                    "Need: " + holder.buildingType.GetResourceAmountString(lv + 1),
                    new TooltipUI.TooltipTimer { timer = 2f });
            }else if (!levelController.CanLevelUp())
            {
                TooltipUI.Instance.Show("Building at Maximum Level, Cannot Upgrade Further!",
                    new TooltipUI.TooltipTimer { timer = 2f });
            }
        });
    }

    private void Update()
    {
        if (levelController.CanLevelUp())
        {
            ResourceAmount[] resourceAmountLevelUp = holder.buildingType.buildingInfos[lv].resourceCostArray;
            if (CanAfford())
            {
                ChangeColorButton(true);
            }
            else
            {
                ChangeColorButton(false);
            }
        }
    }

    private void ChangeColorButton(bool canLvUp)
    {
        float alpha = canLvUp ? canLvUpAlpha : canNotLvUpAlpha;
        Color tempColor;
        tempColor = buttonBackground.color;
        tempColor.a = alpha;
        buttonBackground.color = tempColor;

        tempColor = buttonImage.color;
        tempColor.a = alpha;
        buttonImage.color = tempColor;
    }

    private bool CanUpgradeBuilding()
    {
        if (!levelController.CanLevelUp()) return false;
        if (!CanAfford()) return false;


        return true;
    }

    private bool CanAfford()
    {
        ResourceAmount[] resourceAmountLevelUp = holder.buildingType.buildingInfos[lv].resourceCostArray;
        return ResourceManagerInstance.Instance.CanAfford(resourceAmountLevelUp);
    }

    private void UpgradeBuilding()
    {
        ResourceAmount[] resourceAmountLevelUp = holder.buildingType.buildingInfos[lv].resourceCostArray;
        ResourceManagerInstance.Instance.SpendResource(resourceAmountLevelUp);
        levelController.OnLevelUp();
        lv = levelController.Lv;
    }
}
