using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

// https://www.youtube.com/watch?v=d5nENoQN4Tw

public class ScreenshotHelper : MonoBehaviour
{
    [HideInInspector] public Texture2D screenshotTexture;
    [HideInInspector] public bool finished;
    bool takeScreenshot;

    void OnEnable() 
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    void OnDisable() 
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    void RenderPipelineManager_endCameraRendering(ScriptableRenderContext arg1, Camera arg2)
    {
        if (!takeScreenshot) return;
        takeScreenshot = false;

        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        Rect rect = new Rect(0, 0, width, height);
        screenshot.ReadPixels(rect, 0, 0);
        screenshot.Apply();

        screenshotTexture = screenshot;

        finished = true;
    }

    public void TakeSimpleScreenshot(string filename, int superResolution)
    {
        ScreenCapture.CaptureScreenshot(filename, superResolution);
    }

    /** Does not exculde UI*/
    public IEnumerator TakeRawScreenshot()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        Rect rect = new Rect(0, 0, width, height);
        screenshot.ReadPixels(rect, 0, 0);
        screenshot.Apply();

        screenshotTexture = screenshot;
    }

    /** Excludes UI*/
    public void TakeScreenshotWithoutUI()
    {
        takeScreenshot = true;
        finished = false;
    }
    
    public void WriteToDisk(string filepath)
    {
        byte[] png = screenshotTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(filepath, png);
    }
}
