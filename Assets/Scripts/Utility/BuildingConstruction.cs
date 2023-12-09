using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform constructionPrefab = Resources.Load<Transform>("BuildingConstruction");
        Transform constructionTransform = Instantiate(constructionPrefab,position, Quaternion.identity);

        BuildingConstruction buildingConstruction = constructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }

    private BuildingTypeSO buildingType;
    private float constructionTimer, constructionTimerMax;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = sprite.material;
    }

    void Update()
    {
        constructionTimer -= Time.deltaTime;
        if(constructionTimer <= 0)
        {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;

        sprite.sprite = buildingType.sprite;
        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = constructionTimerMax;
        boxCollider.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        boxCollider.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        buildingTypeHolder.buildingType = buildingType;
    }
    
    public float GetConstructionTimerNormalized()
    {
        return 1- constructionTimer/constructionTimerMax;
    }
}
