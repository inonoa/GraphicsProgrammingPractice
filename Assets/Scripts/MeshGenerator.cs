using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MeshGenerator : SerializedMonoBehaviour
{
    [SerializeField] MeshFilter meshFilterPrefab;
    [SerializeField] IGenerateMesh generateMesh;

    void Start()
    {
        MeshFilter meshFilter = Instantiate(meshFilterPrefab);
        meshFilter.mesh = generateMesh.Generate();
    }


    void Update()
    {
        
    }
}
