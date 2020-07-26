using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MeshGenerator/Quad", fileName = "QuadGenerator", order = 0)]
public class QuadGenerator : ScriptableObject, IGenerateMesh
{
    [SerializeField] float size = 1;

    public Mesh Generate(){
        Mesh mesh = new Mesh();

        float size_2 = size / 2;

        Vector3[] verts = new Vector3[]{
            new Vector3(-size_2,  size_2),
            new Vector3( size_2,  size_2),
            new Vector3(-size_2, -size_2),
            new Vector3( size_2, -size_2)
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
}
