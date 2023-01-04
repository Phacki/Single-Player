using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WorldCreator : MonoBehaviour
{
    Mesh floor;
    Vector3[] vertex;
    int[] sectors;

    public int xSize = 20;
    public int zSize = 20;
    // Start is called before the first frame update
    void Start()
    {
        floor = new Mesh();
        GetComponent<MeshFilter>().mesh = floor;

        CreateFloor();
        UpdateMesh();
    }

    void CreateFloor()  
    {
        vertex = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertex[i] = new Vector3(x, y, z);
                i++;
            }
        }

        sectors = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                sectors[tris + 0] = vert + 0;
                sectors[tris + 1] = vert + xSize + 1;
                sectors[tris + 2] = vert + 1;
                sectors[tris + 3] = vert + 1;
                sectors[tris + 4] = vert + xSize + 1;
                sectors[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        floor.Clear();
        floor.vertices = vertex;
        floor.triangles = sectors;
        floor.RecalculateNormals();
        floor.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = floor;
    }
}
