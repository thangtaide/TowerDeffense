using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] BuildingConstruction buildingConstruction;
    private Transform mask;
    private Image image;
    private void Awake()
    {
        mask = transform.Find("Mask");
        image = mask.Find("Image").GetComponent<Image>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        image.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
