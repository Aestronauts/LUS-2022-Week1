using System.Collections;
using UnityEngine;

// https://www.youtube.com/watch?v=d5nENoQN4Tw

public class ScreenshotHelper : MonoBehaviour
{
    public Texture2D screenshotTexture;

    public IEnumerator TakeScreenshot(string filepath, bool writeToTexture)
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        Rect rect = new Rect(0, 0, width, height);
        screenshot.ReadPixels(rect, 0, 0);
        screenshot.Apply();

        screenshotTexture = screenshot;

        if (writeToTexture)
        {
            byte[] png = screenshot.EncodeToPNG();
            System.IO.File.WriteAllBytes(filepath, png);
        }
    }

    public void TakeSimpleScreenshot(string filename, int superResolution)
    {
        ScreenCapture.CaptureScreenshot(filename, superResolution);
    }
}
