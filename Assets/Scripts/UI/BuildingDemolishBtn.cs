using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] BuildingTypeHolder buildingTypeHolder;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach(ResourceAmount resourceAmount in buildingTypeHolder.buildingType.resourceCostArray)
            {
                ResourceManagerInstance.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * 0.6f));
            }

            Destroy(buildingTypeHolder.gameObject);
        });
    }
}
