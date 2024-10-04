using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap}
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float scale;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public bool autoUpdate;

    public int seed;
    public TerrainType[] region;


    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity);

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for(int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < region.Length; i++)
                {
                    if(currentHeight <= region[i].height)
                    {
                        colourMap[y * mapWidth + x] = region[i].colour;
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
            display.DrawNoiseMap(TextureGenerator.TextureFromColourMap(colourMap,mapWidth,mapHeight));
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
