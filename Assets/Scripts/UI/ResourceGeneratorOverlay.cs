using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform bar;
    private void Awake()
    {
        resourceGenerator.OnChangeTimeMax += ResourceGenerator_OnChangeTimeMax;
    }

    private void ResourceGenerator_OnChangeTimeMax(object sender, System.EventArgs e)
    {
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratorPerSecond().ToString("F1"));
    }

    void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        bar = transform.Find("Bar");
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }
    void Update()
    {
        bar.transform.localScale = new Vector3(1-resourceGenerator.GetTimeNormalized(), 1, 1);
    }
}
