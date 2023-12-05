using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGhostOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private void Awake()
    {
        Hide();
    }
    private void Update()
    {
        int nearByResourceAmount = ResourceGenerator.GetNearByResourceAmount(resourceGeneratorData,transform.position);
        int percent = Mathf.RoundToInt((float)nearByResourceAmount/resourceGeneratorData.maxResourceAmount*100f);
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(percent+"%");
    }
    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);    
    }
}
