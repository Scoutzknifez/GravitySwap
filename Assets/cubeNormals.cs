using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeNormals : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3[] meshnormals;
    Vector3[] meshVertices;
    void Start()
    {
        meshnormals = gameObject.GetComponent<MeshFilter>().sharedMesh.normals;
        meshVertices = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        for(int i = 0; i < meshnormals.Length; i++)
        {
            Debug.Log("Cube normal: " + meshnormals[i]);
            //Debug.Log("Cube Vertex: " + meshVertices[i]);
        }
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        for (int i = 0; i < meshVertices.Length; i++)
        {
            //Debug.Log(gameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length);
            //Gizmos.DrawLine(meshVertices[i], meshVertices[i] * 200);
        }
    }
}
