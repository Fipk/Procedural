using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float meshHeightValue, AnimationCurve HeightCurve, int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / -2f;

        int meshIncrement = levelOfDetail == 0?1:levelOfDetail * 2;
        int newVertices = (width - 1) / meshIncrement + 1; // nombres de vertices par ligne en fonction du LOD
        /* chose identique à la ternaire au dessus
        if (levelOfDetail == 0)
        {
            meshIncrement = 1;
        } else
        {
            meshIncrement = levelOfDetail * 2;
        }*/

        MeshData meshData = new MeshData(newVertices, newVertices);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += meshIncrement)
        {
            for (int x = 0; x < width; x += meshIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, HeightCurve.Evaluate(heightMap[x, y]) * meshHeightValue, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if (x < width -1 && y < height - 1 )
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + newVertices + 1, vertexIndex + newVertices);
                    meshData.AddTriangle(vertexIndex + newVertices + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth,  int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
