using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberrationEffect : MonoBehaviour
{
    public static ChromaticAberrationEffect Instance { get; private set; }

    [SerializeField] private float decreaseSpeed = 1.0f;
    private Volume volume;
    private void Awake()
    {
        Instance = this;

        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (volume.weight>0f)
        {
            volume.weight -= Time.deltaTime*decreaseSpeed;
        }
    }

    public void SetWeight(float weight)
    {
        volume.weight = weight; 
    }
}
