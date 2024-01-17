using System;
using UnityEngine;

[Serializable]
public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timeMax;
    public event EventHandler OnChangeTimeMax;

    public static int GetNearByResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.radiusDirectionResource);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resource = collider2D.GetComponent<ResourceNode>();
            if (resource != null)
            {
                if (resource.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        return Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
    }
    
    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.buildingInfos[0].resourceGeneratorData;
        timeMax = resourceGeneratorData.timeMax;
        timer = 0;
    }
    private void Start()
    {

        int nearbyResourceAmount = GetNearByResourceAmount(resourceGeneratorData, transform.position);
        if (nearbyResourceAmount == 0) {
            enabled = false;
        }
        else
        {
            timeMax = resourceGeneratorData.timeMax/(float)nearbyResourceAmount;
        }
        OnChangeTimeMax?.Invoke(this, EventArgs.Empty);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer += timeMax;
            ResourceManagerInstance.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }
    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }
    public float GetTimeNormalized()
    {
        return timer / timeMax;
    }
    public float GetAmountGeneratorPerSecond()
    {
        if (enabled)
        {
            return 1f / timeMax;
        }
        return 0;
    }
}
