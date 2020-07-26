using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MeshGenerator/Plane", fileName = "PlaneGenerator", order = 0)]
public class PlaneGenerator : ScriptableObject, IGenerateMesh
{
    [SerializeField] Vector2 size = new Vector2(5, 5);
    [SerializeField] Vector2Int numQuads = new Vector2Int(4, 4);

    public Mesh Generate(){
        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[(numQuads.x + 1) * (numQuads.y + 1)];
        for(int i = 0; i < numQuads.y + 1; i++){
            float y = size.y / 2 - size.y / numQuads.y * i;
            for(int j = 0; j < numQuads.x + 1; j++){
                float x = - size.x / 2 + size.x / numQuads.x * j;
                verts[i * (numQuads.y + 1) + j] = new Vector3(x, UnityEngine.Random.Range(0, 2), y);
            }
        }
        mesh.vertices = verts;

        int[] triangles = new int[numQuads.x * numQuads.y * 2 * 3];
        for(int i = 0; i < numQuads.y; i++){
            for(int j = 0; j < numQuads.x; j++){
                int firstIdx = i * numQuads.x + j;
                triangles[6 * firstIdx    ] = i       * (numQuads.x + 1) + j;
                triangles[6 * firstIdx + 1] = i       * (numQuads.x + 1) + j + 1;
                triangles[6 * firstIdx + 2] = (i + 1) * (numQuads.x + 1) + j + 1;

                triangles[6 * firstIdx + 3] = i       * (numQuads.x + 1) + j;
                triangles[6 * firstIdx + 4] = (i + 1) * (numQuads.x + 1) + j + 1;
                triangles[6 * firstIdx + 5] = (i + 1) * (numQuads.x + 1) + j;
            }
        }
        mesh.triangles = triangles;

        return mesh;
    }
}

public static class PrintExtension{
    public static void p(this object obj, object msg){
        Debug.Log(msg);
    }
    public static void pArr<T>(this object obj, IEnumerable<T> collection){
        obj.p(string.Join(",", collection));
    }
}
