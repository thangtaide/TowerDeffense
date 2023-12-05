using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsBuilding : MonoBehaviour,ICheckTarget
{
    public bool CheckTarget(Transform target)
    {
        Building buiding = target.GetComponent<Building>();
        return buiding != null;

    }
}
