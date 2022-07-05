using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class WaterManager : MonoBehaviour
{
    private MeshFilter meshfiler;

    private void Awake()
    {
        meshfiler = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertex = meshfiler.mesh.vertices;
        for(int i = 0; i < vertex.Length;i++)
        {
            vertex[i].y = WaveManeger.instace.GetHeight(transform.position.x + vertex[i].x);
        }
        meshfiler.mesh.vertices = vertex;
        meshfiler.mesh.RecalculateNormals();
    }
}
