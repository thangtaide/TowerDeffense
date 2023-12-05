using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;

public class CreateController : MonoBehaviour
{
    public Transform prefabWoodHarvester;
    public Transform CreateWoodHarvester(Vector3 position)
    {
        return Instantiate(prefabWoodHarvester, position, Quaternion.identity);
    }
}
public class Create: SingletonMonoBehaviour<CreateController> { }
