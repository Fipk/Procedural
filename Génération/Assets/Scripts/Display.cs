using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    public Renderer textureNoise;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawNoiseMap(Texture2D texture)
    {
        textureNoise.sharedMaterial.mainTexture = texture;
        textureNoise.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
