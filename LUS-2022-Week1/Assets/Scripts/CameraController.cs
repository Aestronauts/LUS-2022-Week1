using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController Instance { get; private set; }

    private CinemachineVirtualCamera _originVCAM;
    private CinemachineVirtualCamera _SlowZoomVCAM;
    private CinemachineVirtualCamera _SlowZoomOutVCAM;
    private CinemachineVirtualCamera _fovQuickZoomVCAM;

    CinemachineBasicMultiChannelPerlin _vcamNoise;

    Coroutine _cameraLoop;

    [SerializeField]
    bool _isCameraShake, _isSlowZoom, _isSlowZoomOut, _isFOVquickZoom;
    bool loopStarted = false;

    void Awake()
    {
        Instance = this;

        List<Transform> childObjects = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects.Add(transform.GetChild(i));
        }

        _originVCAM = childObjects[0].GetComponent<CinemachineVirtualCamera>();
        if (_originVCAM == null) Debug.Log("VCAM NULL");

        _SlowZoomVCAM = childObjects[1].GetComponent<CinemachineVirtualCamera>();
        if (_SlowZoomVCAM == null) Debug.Log("Slow Zoom NULL");

        _SlowZoomOutVCAM = childObjects[2].GetComponent<CinemachineVirtualCamera>();
        if (_SlowZoomOutVCAM == null) Debug.Log("Slow Zoom Out NULL");

        _fovQuickZoomVCAM = childObjects[3].GetComponent<CinemachineVirtualCamera>();
        if (_fovQuickZoomVCAM == null) Debug.Log("FOV Quick Zoom Null");

        _vcamNoise = _originVCAM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraShake(5f, 5f);
        SlowZoom();
        SlowZoomOut();
        FOVQuickZoom();
    }

    public void CameraShake(float amplitude, float frequency)
    {
        if (_isCameraShake == true)
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
        TogglePriorty_VCAM(_isSlowZoom, _SlowZoomVCAM);
    }

    public void SlowZoomOut()
    {
        TogglePriorty_VCAM(_isSlowZoomOut, _SlowZoomOutVCAM);     
    }

    public void FOVQuickZoom()
    {
        TogglePriorty_VCAM(_isFOVquickZoom, _fovQuickZoomVCAM);
    }

    public void TogglePriorty_VCAM(bool isActive, CinemachineVirtualCamera vcam)
    {

        if (isActive == true)
        {
            vcam.Priority = 11;

            if(loopStarted == false)
            {
                _cameraLoop = StartCoroutine(LoopCameraFX());
            }    
        }
        else
        {
            vcam.Priority = 9;
        }      
    }
    IEnumerator LoopCameraFX()
    {
        loopStarted = true;
        yield return new WaitForSeconds(6);
        _originVCAM.Priority = 12;
        yield return new WaitForSeconds(1);
        _originVCAM.Priority = 10;
        loopStarted = false;  
    }
}
