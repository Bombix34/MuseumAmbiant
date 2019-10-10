using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class PhoneCamera : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backCamera;
    private Texture defaultBackground;      //go back to previous background is something go wrong

    [SerializeField] RawImage background;
    [SerializeField] AspectRatioFitter fit;


    [SerializeField] RawImage test;
    [SerializeField] Text debugTextArea;

    bool isInit = false;

//BASE FUNCTION_____________________________________________________________________________________________________________

    private void Start()
    {
        test.texture = generateQR("LOLTEST");
        InitCamera();
    }

    private void Update()
    {
        if(Input.touchCount>0&&!isInit)
        {
            isInit = true;
            DisplayCamera();
        }
        if (!isInit)
            return;
        //UpdateCameraDisplay();
        CheckQRCode();
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
                //break;
            }
        }
        if (backCamera == null)
        {
            DisplayDebugText("Unable to find back camera");
            return false;
        }
        backCamera.requestedFPS = 10f;
        backCamera.requestedHeight = 600;
        backCamera.requestedWidth = 600;
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



//QR CODE_______________________________________________________________________________________________


    private void CheckQRCode()
    {
        if (!camAvailable)
        {
            return;
        }
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(backCamera.GetPixels32(),
              backCamera.width, backCamera.height);
            if (result != null)
            {
                Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                debugTextArea.text = result.Text;
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

//DEBUG___________________________________________________________________________________________________

    private void DisplayDebugText(string text)
    {
        debugTextArea.text = text;
    }
}
