using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh}
    public DrawMode drawMode;


    const int chunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;
    public float scale;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public bool autoUpdate;

    public float meshHeightValue;
    public AnimationCurve meshHeightCurve;

    public int seed;
    public TerrainType[] region;


    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(chunkSize, chunkSize, seed, scale, octaves, persistance, lacunarity);

        Color[] colourMap = new Color[chunkSize * chunkSize];
        for(int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < region.Length; i++)
                {
                    if(currentHeight <= region[i].height)
                    {
                        colourMap[y * chunkSize + x] = region[i].colour;
                        break;
                    }
                }
            }
        }
        Display display = FindObjectOfType<Display>();

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawNoiseMap(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawNoiseMap(TextureGenerator.TextureFromColourMap(colourMap, chunkSize, chunkSize));
        } else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightValue, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, chunkSize, chunkSize));
        }

    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}
