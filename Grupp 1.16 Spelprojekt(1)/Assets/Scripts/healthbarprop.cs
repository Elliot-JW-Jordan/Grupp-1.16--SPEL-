using System.IO;
using UnityEngine;

public class ImageModifier : MonoBehaviour
{
    public string inputImagePath = "Assets/Images/input_image.png";  // Path to the raw image
    public string outputImagePath = "Assets/Images/output_image.png";  // Path to save the modified image
    public int targetWidth = 1920;  // Target width (horizontal)
    public int targetHeight = 1080; // Target height

    void Start()
    {
        ModifyImage(inputImagePath, outputImagePath, targetWidth, targetHeight);
    }

    void ModifyImage(string inputPath, string outputPath, int targetWidth, int targetHeight)
    {
        // Load the image from file
        byte[] imageData = File.ReadAllBytes(inputPath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        // Ensure the image is horizontal, rotate if necessary
        if (texture.width < texture.height) // Image is vertical
        {
            texture = RotateTexture(texture);
        }

        // Resize the texture to fill the target size
        Texture2D resizedTexture = ResizeTexture(texture, targetWidth, targetHeight);

        // Save the modified texture back to a file
        byte[] resizedImageData = resizedTexture.EncodeToPNG(); // Or use EncodeToJPG if needed
        File.WriteAllBytes(outputPath, resizedImageData);

        Debug.Log("Image modified and saved at: " + outputPath);
    }

    Texture2D RotateTexture(Texture2D originalTexture)
    {
        // Rotate the texture 90 degrees clockwise
        Texture2D rotatedTexture = new Texture2D(originalTexture.height, originalTexture.width);
        for (int x = 0; x < originalTexture.width; x++)
        {
            for (int y = 0; y < originalTexture.height; y++)
            {
                rotatedTexture.SetPixel(y, originalTexture.width - 1 - x, originalTexture.GetPixel(x, y));
            }
        }
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    Texture2D ResizeTexture(Texture2D originalTexture, int width, int height)
    {
        Texture2D resizedTexture = new Texture2D(width, height);
        Color[] pixels = originalTexture.GetPixels(0, 0, originalTexture.width, originalTexture.height);
        resizedTexture.SetPixels(0, 0, width, height, pixels);
        resizedTexture.Apply();
        return resizedTexture;
    }
}
