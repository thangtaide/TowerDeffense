using LTA.DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private List<ResourceAmount> startingResourceAmount;
    private Dictionary<ResourceTypeSO, int> resouceAmoutDictinary;
    private ResourceTypeListSO resourceTypeListSO;
    public event EventHandler onResourceAmountChange;
    private void Awake()
    {
        resouceAmoutDictinary = new Dictionary<ResourceTypeSO, int>();
        resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach(ResourceTypeSO type in resourceTypeListSO.list)
        {
            resouceAmoutDictinary[type] = 0;
        }
        foreach(ResourceAmount resourceAmount in startingResourceAmount)
        {
            AddResource(resourceAmount.resourceType,resourceAmount.amount);
        }
    }
    private void Update()
    {

        
    }

    public void AddResource(ResourceTypeSO resource, int amout)
    {
        onResourceAmountChange?.Invoke(this, EventArgs.Empty);
        resouceAmoutDictinary[resource] += amout;
    }

    public int GetResource(ResourceTypeSO resource)
    {
        return resouceAmoutDictinary[resource];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach(ResourceAmount resourceAmount in resourceAmountArray)
        {
            if(GetResource(resourceAmount.resourceType) < resourceAmount.amount) { 
                    return false;
            }
        }
        return true;
    }
    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            resouceAmoutDictinary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
public class ResourceManagerInstance: SingletonMonoBehaviour<ResourceManager> { }
