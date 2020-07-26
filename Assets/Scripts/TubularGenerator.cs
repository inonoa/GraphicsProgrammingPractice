using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MeshGenerator/Tubular", fileName = "TubularGenerator", order = 0)]
public class TubularGenerator : ScriptableObject, IGenerateMesh
{

    [SerializeField] Vector3[] curvePositions;
    [SerializeField] float radius = 0.3f;
    [SerializeField] int numCylinders = 100;

    CatmullRomCurveVector3 _Curve;
    CatmullRomCurveVector3 Curve{
        get{
            if(_Curve == null) _Curve = new CatmullRomCurveVector3(curvePositions);
            return _Curve;
        }
    }

    public Mesh Generate(){
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for(int i = 0; i < numCylinders + 1; i ++){
            float t = i / (float)numCylinders;
            Vector3[] hexagon = GenerateHexagon(
                Curve.CalcPos(t),
                Curve.CalcVel(t),
                Curve.CalcSecondDerivative(t),
                radius
            );
            Vector3[] hexagon_notwist = new Vector3[6];

            if(i==0){
                hexagon_notwist = hexagon;
            }else{
                for(int j = 0; j < 6; j++){
                    float min_dist = float.MaxValue;
                    for(int k = 0; k < 6; k++){
                        float newDist = Vector3.Distance(verts[verts.Count - 6 + j], hexagon[k]);
                        if(min_dist > newDist){
                            min_dist = newDist;
                            hexagon_notwist[j] = hexagon[k];
                        }
                    }
                }
            }

            verts.AddRange(hexagon_notwist);
            uvs.AddRange(new Vector2[]{
                new Vector2(0   , 0),
                new Vector2(1/6f, 1/3f),
                new Vector2(2/6f, 2/3f),
                new Vector2(3/6f, 1),
                new Vector2(4/6f, 2/3f),
                new Vector2(5/6f, 1/3f),
            });

            if(i == 0) continue;

            int lastHexFirstIdx = verts.Count - 12;
            int currentHexFirstIdx = verts.Count - 6;
            
            for(int j = 0; j < 6; j++){
                triangles.AddRange(new int[]{
                    lastHexFirstIdx + j % 6, lastHexFirstIdx + (j + 1) % 6, currentHexFirstIdx + (j + 1) % 6,
                    lastHexFirstIdx + j % 6, currentHexFirstIdx + (j + 1) % 6, currentHexFirstIdx + j % 6
                });
            }
            
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        return mesh;
    }

    public Vector3[] GenerateHexagon(Vector3 center, Vector3 direction, Vector3 normal, float radius){
        Vector3[] verts = new Vector3[6];

        //この辺正しいのか……？
        Vector3 dir_normal = direction, normal_normal = normal, binormal_normal = Vector3.Cross(direction, normal);
        Vector3.OrthoNormalize(ref dir_normal, ref normal_normal, ref binormal_normal);
        for(int i = 0; i < 6; i++){
            verts[i] = center + Vector3.SlerpUnclamped(normal_normal, binormal_normal, i * 2f / 3f) * radius;
        }

        return verts;
    }
}
