using UnityEngine;

public class TextureColorInverter : MonoBehaviour
{
    public Texture2D texture;
    [Range(0, 1)] public float invert = 0; // Inspector에서 제어
    private Texture2D invertedTexture;

    void Start()
    {
        invertedTexture = new Texture2D(texture.width, texture.height);
        ApplyInvert();
    }

    void ApplyInvert()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color originalColor = texture.GetPixel(x, y);
                Color newColor = Color.Lerp(originalColor, new Color(1 - originalColor.r, 1 - originalColor.g, 1 - originalColor.b, originalColor.a), invert);
                invertedTexture.SetPixel(x, y, newColor);
            }
        }
        invertedTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = invertedTexture;
    }
}
