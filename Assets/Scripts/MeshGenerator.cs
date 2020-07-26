using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    void Start()
    {
        GameObject quad = new GameObject("Quad");
        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        meshFilter.mesh = GenerateQuad();
        quad.AddComponent<MeshRenderer>();
    }

    Mesh GenerateQuad(){

        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[]{
            new Vector3(-1, 1),
            new Vector3(1, 1),
            new Vector3(-1, -1),
            new Vector3(1, -1)
        };

        Color[] colors = new Color[]{
            new Color(0,1,1,1),
            new Color(1,1,1,1),
            new Color(0,0,1,1),
            new Color(1,0,1,1)
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
        mesh.colors = colors;
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
