using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private CinemachineVirtualCamera matchUICam;
    [SerializeField] private CinemachineVirtualCamera matchCam;
    private CinemachineBasicMultiChannelPerlin currentCam;

    private void Start()
    {
        currentCam = mainCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CameraShake()
    {
        StartCoroutine(CameraShakeTime(0.3f));
    }

    private IEnumerator CameraShakeTime(float time)
    {
        currentCam.m_AmplitudeGain = 1;
        yield return new WaitForSeconds(time);
        currentCam.m_AmplitudeGain = 0;
    }

    public void CameraPositionChange(int value)
    {
        switch (value)
        {
            case 0:
                mainCam.Priority = 10;
                matchUICam.Priority = 1;
                matchCam.Priority = 1;
                break;
            case 1:
                mainCam.Priority = 1;
                matchUICam.Priority = 10;
                matchCam.Priority = 1;
                break;
            case 2:
                mainCam.Priority = 1;
                matchUICam.Priority = 1;
                matchCam.Priority = 10;
                break;
        }
    }
}