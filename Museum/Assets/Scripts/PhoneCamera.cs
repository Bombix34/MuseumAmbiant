using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backCamera;
    private Texture defaultBackground;      //go back to previous background is something go wrong

    [SerializeField] RawImage background;
    [SerializeField] AspectRatioFitter fit;

    [SerializeField] Text debugTextArea;

//BASE FUNCTION_____________________________________________________________________________________________________________

    private void Start()
    {
        if(InitCamera())
        {
            DisplayCamera();
        }
    }

    private void Update()
    {
      //  UpdateCameraDisplay();
    }

//CAMERA PART________________________________________________________________________________________________________________

    private bool InitCamera()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            DisplayDebugText("No camera detected");
            camAvailable = false;
            return false;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }
        if (backCamera == null)
        {
            DisplayDebugText("Unable to find back camera");
            return false;
        }
        return true;
    }

    private void DisplayCamera()
    {
        backCamera.Play();
        background.texture = backCamera;

        camAvailable = true;
        UpdateCameraDisplay();
    }

    private void UpdateCameraDisplay()
    {
        if (!camAvailable)
        {
            return;
        }
        float ratio = (float)backCamera.width / (float)backCamera.height;
        fit.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);
    }

//DEBUG___________________________________________________________________________________________________

    private void DisplayDebugText(string text)
    {
        debugTextArea.text = text;
    }
}
