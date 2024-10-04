using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    public Renderer textureNoise;

    public void DrawNoiseMap(Texture2D texture)
    {
        textureNoise.sharedMaterial.mainTexture = texture;
        textureNoise.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}
