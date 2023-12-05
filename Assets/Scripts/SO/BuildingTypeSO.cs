using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public bool hasResourceGenerator;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] resourceCostArray;
    public int healthAmountMax;

    public string GetResourceAmountString()
    {
        string str = "";
        foreach(ResourceAmount resourceAmount in resourceCostArray)
        {
            str += "<color=#"+ resourceAmount.resourceType.colorHex+">"+
                resourceAmount.resourceType.nameString+": "+resourceAmount.amount+
                "</color>\n";
        }
        return str;
    }
}
