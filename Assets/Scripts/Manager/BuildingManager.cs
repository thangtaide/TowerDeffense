using LTA.DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] float maxConstrucRadius = 25f;
    private BuildingTypeListSO buildingTypeList;
    public BuildingTypeSO activeBuildingType;
    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;
    [SerializeField]private Building hqBuilding;

    public class OnActiveBuildingTypeChangeEventArgs: EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }
    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        hqBuilding.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    private void HQ_OnDied(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
        GameOverUI.Instance.Show();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, UtilsClass.GetMousePosition(),out string errMessage))
                {
                    if (ResourceManagerInstance.Instance.CanAfford(activeBuildingType.resourceCostArray))
                    {
                        //Instantiate(activeBuildingType.prefab, UtilsClass.GetMousePosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMousePosition(),activeBuildingType);
                        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                        ResourceManagerInstance.Instance.SpendResource(activeBuildingType.resourceCostArray);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Cannot afford \n"+activeBuildingType.GetResourceAmountString(), new TooltipUI.TooltipTimer { timer = 2f});
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errMessage, new TooltipUI.TooltipTimer { timer = 2f });
                }
            }
        }
    }

    public void ChangeBuilding(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeChangeEventArgs { activeBuildingType = this.activeBuildingType});
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
    public bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errMessage = "Area is not clear!";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position + (Vector3)boxCollider2D.offset, buildingType.minConstructionRadius);
        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder colliderBuildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (colliderBuildingTypeHolder != null)
            {
                if (colliderBuildingTypeHolder.buildingType == buildingType)
                {
                    errMessage = "Too close to another building of same type!";
                    return false;
                }
            }
        }

        collider2DArray = Physics2D.OverlapCircleAll(position + (Vector3)boxCollider2D.offset, maxConstrucRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder colliderBuildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (colliderBuildingTypeHolder != null)
            {
                errMessage = "";
                return true;
            }
        }
        errMessage = "Too far from any other building!";
        return false;
    }
    /*public Building GetHQBuilding()
    {
        return hqBuilding;
    }*/
}
public class BuildingManagerInstance: SingletonMonoBehaviour<BuildingManager>
{

}
