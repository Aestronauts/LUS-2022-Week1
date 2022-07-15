using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    private CinemachineVirtualCamera _originVCAM;
    private CinemachineVirtualCamera _SlowZoomVCAM;
    private CinemachineVirtualCamera _SlowZoomOutVCAM;
    private CinemachineVirtualCamera _fovQuickZoomVCAM;

    CinemachineBasicMultiChannelPerlin _vcamNoise;

    

    [SerializeField]
    bool _isCameraShake, _isSlowZoom, _isSlowZoomOut, _isFOVquickZoom;
    public bool loopStarted = false;

    void Awake()
    {

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
        CameraBlendHandeler(); 
    }

    public void SetSlowZoom_VCAM(bool isActive)
    {
        _isSlowZoom = isActive;
    }
    public void SetSlowZoomOut_VCAM(bool isActive)
    {
        _isSlowZoomOut = isActive;
    }

    public void SetFOVQuickZoom_VCAM(bool isActive)
    {
        _isFOVquickZoom = isActive;
    }

    public void SetCameraShake_VCAM(bool isActive)
    {
        _isCameraShake = isActive;
    }

    void CameraBlendHandeler()
    {
        if (_isSlowZoom)
        {
           _SlowZoomVCAM.Priority = 11;
            StartLoop();
        }
        else if (!_isSlowZoomOut && !_isFOVquickZoom)
        {
            ResetCamera();
            _SlowZoomVCAM.Priority = 9;
        }

        if (_isSlowZoomOut)
        {
            _SlowZoomOutVCAM.Priority = 11;
            StartLoop();
        }
        else if (!_isSlowZoom && !_isFOVquickZoom)
        {
            ResetCamera();
            _SlowZoomOutVCAM.Priority = 9;
        }

        if (_isFOVquickZoom)
        {
            _fovQuickZoomVCAM.Priority = 11;
            StartLoop();
        }
        else if (!_isSlowZoom && !_isSlowZoomOut)
        {
            _fovQuickZoomVCAM.Priority = 9;
            ResetCamera();
        }
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

    void StartLoop()
    {
        if (loopStarted == false)
        {
            StopAllCoroutines();
            StartCoroutine(LoopCameraFX());
        }
    }

    void ResetCamera()
    {
        _originVCAM.Priority = 10;
        loopStarted = false;
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
