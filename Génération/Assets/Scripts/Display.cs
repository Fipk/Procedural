using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    public Renderer textureNoise;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        if (noiseMap == null) return; 

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] color = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                color[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        texture.SetPixels(color);
        texture.Apply();

        textureNoise.sharedMaterial.mainTexture = texture;

        textureNoise.transform.localScale = new Vector3(width, 1, height);
    }
}
