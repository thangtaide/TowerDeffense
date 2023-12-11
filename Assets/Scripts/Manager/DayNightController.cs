using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightController : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] float secondPerDay = 10f;
    private float dayTime;
    private float dayTimeSpeed;

    private Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondPerDay;
    }

    private void Update()
    {
        dayTime += Time.deltaTime*dayTimeSpeed;

        light2D.color = gradient.Evaluate(dayTime % 1f);
    }
}
