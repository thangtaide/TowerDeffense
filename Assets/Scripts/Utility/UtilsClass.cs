using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    private static Camera cameraMain;
    public static Vector3 GetMousePosition()
    {
        if (cameraMain == null)
        {
            cameraMain = Camera.main;
        }
        Vector3 pos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }

    public static Vector3 GetRandomDir() {
        return new Vector3( 
            Random.Range(-1f,1f),
            Random.Range(-1f,1f) 
            ).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radian = Mathf.Atan2(vector.y, vector.x);
        float degrees = radian* Mathf.Rad2Deg;
        return degrees;
    }
}
