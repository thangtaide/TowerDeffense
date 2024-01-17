using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    [SerializeField] Color canSpawnBuildingColor;
    [SerializeField] Color canNotSpawnBuildingColor;
    private ResourceGhostOverlay resourceGhostOverlay;
    private void Awake()
    {
        resourceGhostOverlay = transform.Find("GhostResourceOverlay").GetComponent<ResourceGhostOverlay>();
        spriteGameObject = transform.Find("Sprite").gameObject;
        Hide();
    }
    void Start()
    {
        BuildingManagerInstance.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        if (e.activeBuildingType != null)
        {
            Show(e.activeBuildingType.buildingInfos[0].sprite);
            if (e.activeBuildingType.hasResourceGenerator)
            {
                resourceGhostOverlay.Show(e.activeBuildingType.buildingInfos[0].resourceGeneratorData);
            }
            else
            {
                resourceGhostOverlay.Hide();
            }
        }
        else
        {
            Hide();
            resourceGhostOverlay.Hide();
        }
    }

    void Update()
    {
        transform.position = UtilsClass.GetMousePosition();
        if (BuildingManagerInstance.Instance.activeBuildingType != null)
        {
            bool canAfford = ResourceManagerInstance.Instance.CanAfford(BuildingManagerInstance.Instance.activeBuildingType.buildingInfos[0].resourceCostArray);
            bool canSpawnBuilding = BuildingManagerInstance.Instance.CanSpawnBuilding(BuildingManagerInstance.Instance.activeBuildingType, UtilsClass.GetMousePosition(), out string errMessage);
            if ( canAfford && canSpawnBuilding)
            {
                CanSpawnBuildingColor();
            }
            else
            {
                CanNotSpawnBuildingColor();
            }
        }
    }
    void CanSpawnBuildingColor()
    {
        spriteGameObject.GetComponent<SpriteRenderer>().color = canSpawnBuildingColor;
    }
    void CanNotSpawnBuildingColor()
    {
        spriteGameObject.GetComponent<SpriteRenderer>().color = canNotSpawnBuildingColor;
    }
    void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
