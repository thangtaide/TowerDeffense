using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public event EventHandler OnLevelUpEvent;

    private TextMeshPro levelTxt;
    private BuildingTypeHolder buildingTypeHolder;
    private int lv;
    public int Lv { get { return lv; } }

    private void Awake()
    {
        lv = 1;
        buildingTypeHolder = GetComponentInParent<BuildingTypeHolder>();
        levelTxt = transform.GetComponentInChildren<TextMeshPro>();
        UpdateText();
    }

    public void OnLevelUp()
    {
        if (CanLevelUp())
        {
            lv++;
            UpdateText();
            OnLevelUpEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void UpdateText()
    {
        levelTxt.text = "Lv."+lv;
    }

    public bool CanLevelUp()
    {
        return lv < buildingTypeHolder.buildingType.buildingInfos.Length;
    }
}

