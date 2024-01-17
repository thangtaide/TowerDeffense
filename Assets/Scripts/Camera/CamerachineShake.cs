using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamerachineShake : MonoBehaviour
{
    private static CamerachineShake instance;
    public static CamerachineShake Instance {  get { return instance; } }

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin channelPerlin;
    private float timer, timerMax, startingIntensity;

    private void Awake()
    {
        instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        startingIntensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= timerMax)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startingIntensity,0f,timer/timerMax);
            channelPerlin.m_AmplitudeGain = amplitude;
        }
    }
    public void SharkCamera(float intensity, float timerMax)
    {
        this.timerMax = timerMax;
        timer = 0f;
        startingIntensity = intensity;
        channelPerlin.m_AmplitudeGain = intensity;
    }
}
