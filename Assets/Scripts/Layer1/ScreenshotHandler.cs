using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{

    private static ScreenshotHandler instance;

    private Camera mainCamera;
    private RenderTexture renderTexture;
    private Texture2D renderResult;
    private Rect screenshotCanvas;
    private byte[] byteArray;


    private bool takeScreenshotOnNextFrame;

    private void Awake()
    {
        instance = this;
        mainCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;

            //Sets render texture to what main camera sees
            renderTexture = mainCamera.targetTexture;

            //Creates a 2D texture with the same formatting as the normal screen
            renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);

            //Creates a rectangle to act as a canvas for the screenshot
            screenshotCanvas = new Rect(0, 0, renderTexture.width, renderTexture.height);

            //Copies the screenshot from the render texture to the rectangle
            renderResult.ReadPixels(screenshotCanvas, 0, 0);

            //Encodes the texture into PNG format
            byteArray = renderResult.EncodeToPNG();

            //Creates/Overwrites the screenshot in the current directory under a new folder called CameraScreenshot
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");

            //Deletes the render texture
            RenderTexture.ReleaseTemporary(renderTexture);

            //Sets the main camera back to normal
            mainCamera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        mainCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshotStatic(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}

