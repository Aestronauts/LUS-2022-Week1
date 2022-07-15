using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraMovement : MonoBehaviour
{
    
    public TMPro.TMP_Dropdown expDropdown;

    private int currentExp;
    private int previousExp;

    CameraController _cameraController;


    // Start is called before the first frame update
    void Awake()
    {
        _cameraController = GameObject.Find("Camera System").GetComponent<CameraController>();
        if (_cameraController != null) Debug.Log("Camera System in Null");
        
    }

    // Update is called once per frame
    void Update()
    {
        currentExp = expDropdown.value;


        if (expDropdown.value == 0)
        {

            _cameraController.SetSlowZoom_VCAM(false);
            _cameraController.SetSlowZoomOut_VCAM(false);
            _cameraController.SetFOVQuickZoom_VCAM(false);
            _cameraController.SetCameraShake_VCAM(false);
            previousExp = 0;

        }


        if (expDropdown.value == 1)
        {
            _cameraController.SetSlowZoom_VCAM(false);
            _cameraController.SetSlowZoomOut_VCAM(false);
            _cameraController.SetFOVQuickZoom_VCAM(false);
            _cameraController.SetCameraShake_VCAM(true);
            previousExp = 1;
        }

        if (expDropdown.value == 2)
        {
            _cameraController.SetCameraShake_VCAM(false);
            _cameraController.SetSlowZoom_VCAM(false);
            _cameraController.SetSlowZoomOut_VCAM(false);
            _cameraController.SetFOVQuickZoom_VCAM(true);
            previousExp = 2;
        }

        if (expDropdown.value == 3)
        {
            _cameraController.SetCameraShake_VCAM(false);
            _cameraController.SetSlowZoomOut_VCAM(false);
            _cameraController.SetFOVQuickZoom_VCAM(false);
            _cameraController.SetSlowZoom_VCAM(true);

            previousExp = 3;
        }

        if (expDropdown.value == 4)
        {
            _cameraController.SetCameraShake_VCAM(false);
            _cameraController.SetSlowZoom_VCAM(false);
            _cameraController.SetFOVQuickZoom_VCAM(false);
            _cameraController.SetSlowZoomOut_VCAM(true);
            previousExp = 4;
        }

 



    }
}
