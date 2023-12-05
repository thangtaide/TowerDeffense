using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] float distance = 220f;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] BuildingTypeSO[] ignorBuildingType;
    Dictionary<BuildingTypeSO, Transform> btnBuildingDictionary;
    Transform arrowBtn;

    private void Awake()
    {
        btnBuildingDictionary = new Dictionary<BuildingTypeSO, Transform>();
        Transform btnTemplate = transform.Find("BtnTemplate");
        btnTemplate.gameObject.SetActive(false);
        Vector2 btnTemplatePos = btnTemplate.GetComponent<RectTransform>().anchoredPosition;

        int index = 0;
        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);
        arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(-20,-80);
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(btnTemplatePos.x + index * distance, btnTemplatePos.y);
        arrowBtn.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManagerInstance.Instance.ChangeBuilding(null);
            CameraHandlerSingleton.Instance.canMoveByMouse = true;
        });

        MouseEnterExitEvent mouseEnterExitEvent = arrowBtn.GetComponent<MouseEnterExitEvent>();
        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };
        index++;

        BuildingTypeListSO buildingTypeListSO = Resources.Load<BuildingTypeListSO>("BuildingTypeListSO");
        foreach (BuildingTypeSO buildingType in buildingTypeListSO.list)
        {
            if(ignorBuildingType.Contains(buildingType)) continue;
            Transform btnBuildingTransform = Instantiate(btnTemplate, transform);
            btnBuildingTransform.gameObject.SetActive(true);
            btnBuildingTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            btnBuildingTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(btnTemplatePos.x + index * distance, btnTemplatePos.y);

            btnBuildingTransform.GetComponent<Button>().onClick.AddListener(() => { 
                BuildingManagerInstance.Instance.ChangeBuilding(buildingType);
                CameraHandlerSingleton.Instance.canMoveByMouse = false;
            });
            mouseEnterExitEvent = btnBuildingTransform.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString+"\n"+buildingType.GetResourceAmountString());
            };
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            btnBuildingDictionary[buildingType] = btnBuildingTransform;
            index++;
        }
    }
    private void Start()
    {
        BuildingManagerInstance.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }
    void UpdateActiveBuildingTypeButton()
    {
        arrowBtn.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in btnBuildingDictionary.Keys)
        {
            btnBuildingDictionary[buildingType].Find("Selected").gameObject.SetActive(false);
        }
        BuildingTypeSO activeBuildingType = BuildingManagerInstance.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            arrowBtn.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            btnBuildingDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
        }
    }
}
