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
    [SerializeField] int numVertsInSegment = 20;

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
            Vector3[] segment = GenerateSegment(
                Curve.CalcPos(t),
                Curve.CalcVel(t),
                Curve.CalcSecondDerivative(t),
                radius,
                numVertsInSegment
            );
            Vector3[] segment_notwist = new Vector3[numVertsInSegment];

            if(i==0){
                segment_notwist = segment;
            }else{
                float min_dist = float.MaxValue;
                int min_idx = -1;
                for(int k = 0; k < numVertsInSegment; k++){
                    float newDist = Vector3.Distance(verts[verts.Count - numVertsInSegment], segment[k]);
                    if(min_dist > newDist){
                        min_dist = newDist;
                        min_idx = k;
                        segment_notwist[0] = segment[k];
                    }
                }
                bool dir = 
                      Vector3.Distance(verts[verts.Count - numVertsInSegment + 1], segment[(min_idx + 1) % numVertsInSegment])
                    < Vector3.Distance(verts[verts.Count - numVertsInSegment + 1], segment[(numVertsInSegment + min_idx - 1) % numVertsInSegment]);
                if(dir){
                    for(int k = 1; k < numVertsInSegment; k++){
                        segment_notwist[k] = segment[(min_idx + k) % numVertsInSegment];
                    }
                }else{
                    for(int k = 1; k < numVertsInSegment; k++){
                        segment_notwist[k] = segment[(numVertsInSegment + min_idx - k) % numVertsInSegment];
                    }
                }
            }

            verts.AddRange(segment_notwist);
            uvs.AddRange(new Vector2[numVertsInSegment]);

            if(i == 0) continue;

            int lastHexFirstIdx = verts.Count - numVertsInSegment * 2;
            int currentHexFirstIdx = verts.Count - numVertsInSegment;
            
            for(int j = 0; j < numVertsInSegment; j++){
                triangles.AddRange(new int[]{
                    lastHexFirstIdx + j % numVertsInSegment, lastHexFirstIdx + (j + 1) % numVertsInSegment, currentHexFirstIdx + (j + 1) % numVertsInSegment,
                    lastHexFirstIdx + j % numVertsInSegment, currentHexFirstIdx + (j + 1) % numVertsInSegment, currentHexFirstIdx + j % numVertsInSegment
                });
            }
            
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        return mesh;
    }

    public Vector3[] GenerateSegment(Vector3 center, Vector3 direction, Vector3 normal, float radius, int numVerts){
        Vector3[] verts = new Vector3[numVerts];

        //この辺正しいのか……？
        Vector3 dir_normal = direction, normal_normal = normal, binormal_normal = Vector3.Cross(direction, normal);
        Vector3.OrthoNormalize(ref dir_normal, ref normal_normal, ref binormal_normal);
        for(int i = 0; i < numVerts; i++){
            verts[i] = center + Vector3.SlerpUnclamped(normal_normal, binormal_normal, i * 4f / numVerts) * radius;
        }

        return verts;
    }
}
