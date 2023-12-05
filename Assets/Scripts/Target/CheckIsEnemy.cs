using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsEnemy :  MonoBehaviour,ICheckTarget
{
    public bool CheckTarget(Transform target)
    {
        EnemyController enemy = target.GetComponent<EnemyController>();
        return enemy != null;
    }

}
