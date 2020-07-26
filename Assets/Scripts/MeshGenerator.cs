using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilterPrefab;

    void Start()
    {
        MeshFilter meshFilter = Instantiate(meshFilterPrefab);
        meshFilter.mesh = GenerateQuad();
    }

    Mesh GenerateQuad(){

        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[]{
            new Vector3(-1, 1),
            new Vector3(1, 1),
            new Vector3(-1, -1),
            new Vector3(1, -1)
        };

        Vector2[] uvs = new Vector2[]{
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        Vector3[] normals = new Vector3[]{
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1)
        };

        int[] triangles = new int[]{
            0, 1, 3,
            0, 3, 2
        };

        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.normals = normals;
        mesh.triangles = triangles;

        //mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }


    void Update()
    {
        
    }
}
