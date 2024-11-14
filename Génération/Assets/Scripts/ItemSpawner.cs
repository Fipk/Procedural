using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount = 50;

    public void SpawnItems(float[,] heightMap, MeshData meshData)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        for (int i = 0; i < itemCount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            Vector3 itemPosition = meshData.vertices[Random.Range(0,meshData.vertices.Length)];

            Instantiate(itemPrefab, itemPosition, Quaternion.identity);
        }
    }
}
