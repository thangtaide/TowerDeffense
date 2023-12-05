using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePosSortingOrder : MonoBehaviour
{
    [SerializeField] private float positionOffsetY;
    [SerializeField] float precisionMultiplier = 5f;
    [SerializeField] bool runOnce;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);
        if(runOnce) { Destroy(this); }
    }
}
