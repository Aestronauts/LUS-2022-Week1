using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController Instance { get; private set; }
    
    private CinemachineVirtualCamera _originVCAM;
    private CinemachineVirtualCamera _SlowZoomVCAM;
    CinemachineBasicMultiChannelPerlin _vcamNoise;

    [SerializeField]
    bool _isCameraShake, _isSlowZoom;

    void Awake()
    {
        Instance = this;
        
        List<Transform> childObjects = new List<Transform>();
        for(int i = 0; i<transform.childCount; i++)
        {
            childObjects.Add(transform.GetChild(i));    
        }

        _originVCAM = childObjects[0].GetComponent<CinemachineVirtualCamera>();
        if (_originVCAM == null) Debug.Log("VCAM NULL");

        _SlowZoomVCAM = childObjects[1].GetComponent<CinemachineVirtualCamera>();
        if (_SlowZoomVCAM == null) Debug.Log("Slow Zoom NULL");

        _vcamNoise = _originVCAM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraShake(5f, 5f);
        SlowZoom();
      
    }

    public void CameraShake(float amplitude, float frequency)
    {
       if(_isCameraShake == true)
        {
            _vcamNoise.m_AmplitudeGain = amplitude;
            _vcamNoise.m_FrequencyGain = frequency;
        }
        else
        {
            _vcamNoise.m_AmplitudeGain = 0;
            _vcamNoise.m_FrequencyGain = 0;
        }
    }
       
    public void SlowZoom()
    {
        if (_isSlowZoom == true)
            _SlowZoomVCAM.Priority = 11;
        else
            _SlowZoomVCAM.Priority = 9;
    }

    
}
