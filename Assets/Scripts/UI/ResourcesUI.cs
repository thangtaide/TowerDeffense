using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField]float distance = -220f;
    Dictionary<ResourceTypeSO, Transform> resourceDictionary;
    ResourceTypeListSO resourceTypeList;
    private void Awake()
    {
        resourceDictionary = new Dictionary<ResourceTypeSO, Transform>();
        Transform resourceTemplate = transform.Find("ResourcesTemplate");
        resourceTemplate.gameObject.SetActive(false);
        resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourceTypeListSO");
        int index = 0;
        foreach(ResourceTypeSO resourceTypeSO in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(distance * index, 0f);
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceTypeSO.sprite;
            resourceTransform.gameObject.SetActive(true);

            resourceDictionary[resourceTypeSO] = resourceTransform;
            index++;
        }
    }

    private void Start()
    {
        ResourceManagerInstance.Instance.onResourceAmountChange += ResourceManager_onResourceAmountChange;
        UpdateResourceAmount();
    }

    private void ResourceManager_onResourceAmountChange(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    void UpdateResourceAmount()
    {
        foreach(ResourceTypeSO typeSO in resourceTypeList.list)
        {
            resourceDictionary[typeSO].Find("Text").GetComponent<TextMeshProUGUI>().text = ResourceManagerInstance.Instance.GetResource(typeSO).ToString();

        }
    }
}
