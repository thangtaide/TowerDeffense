using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingInfo
{
    public int damageAmount;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public ResourceAmount[] resourceCostArray;
    public int healthAmountMax;
    public float constructionTimerMax;
}

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public BuildingInfo[] buildingInfos;
    public string nameString;
    public Transform prefab;
    public bool hasResourceGenerator;
    public float minConstructionRadius;

    public string GetResourceAmountString(int level)
    {
        string str = "";
        foreach(ResourceAmount resourceAmount in buildingInfos[level-1].resourceCostArray)
        {
            str += "<color=#"+ resourceAmount.resourceType.colorHex+">"+
                resourceAmount.resourceType.nameString+": "+resourceAmount.amount+
                "</color>\n";
        }
        return str;
    }
}
