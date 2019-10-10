using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.XR.ARFoundation;
using ZXing;
using ZXing.QrCode;

public class QRCodeSearch : MonoBehaviour
{
    //AR Camera
    /*public ARCameraBackground aRCameraBackground;

    //Texture temporaire pour récupérer l'affichage caméra
    private RenderTexture rt;

    private Texture2D lastCameraTexture;

    [SerializeField] RawImage test;
    [SerializeField] Text qrReadTest;


    // Start is called before the first frame update
    void Start()
    {
        rt = new RenderTexture(Screen.width, Screen.height, 1, RenderTextureFormat.ARGB32);

       test.texture = generateQR("LOLTEST");

    }

    void Update()
    {
        Read();
    }

    public void Read()
    {
        Graphics.Blit(null, rt, aRCameraBackground.material);
        //Sauvegarde de la render texture courante
        RenderTexture rtScreen = RenderTexture.active;
        RenderTexture.active = rt;

        // operations
        if(lastCameraTexture==null)
            lastCameraTexture = new Texture2D(rt.width,rt.width, TextureFormat.RGB24,true);
        lastCameraTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        lastCameraTexture.Apply();
        //test.texture = lastCameraTexture;
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(lastCameraTexture.GetPixels32(),
              lastCameraTexture.width, lastCameraTexture.height);
            if (result != null)
            {
                Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                qrReadTest.text = result.Text;
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }


        RenderTexture.active = rtScreen;
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
    */


}
