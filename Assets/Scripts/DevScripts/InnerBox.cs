using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class InnerBox : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
        foreach (Vector3 normal in GetComponent<MeshFilter>().mesh.normals)
        {
            Debug.Log("Yellow Cube: " + normal);
        }
    }

    // Update is called once per frame
    void CreateShape()
    {
        vertices = new Vector3[]
        {
            //base
            new Vector3 (0,0,0),
            new Vector3 (0,0,150),
            new Vector3 (150,0,0),
            new Vector3 (150, 0, 150),

            //top
            new Vector3 (0,150, 0),
            new Vector3 (150, 150, 0),
            new Vector3 (0, 150, 150),
            new Vector3 (150, 150, 150)
        };

        triangles = new int[]
        {
            0,1,2,
            2,1,3,

            0,2,5,
            5,4,0,

            6, 1, 0,
            6, 0,4,

            2,3,7,
            7,5,2,

            7,3,1,
            7,1,6,

            4,5,6,
            5,7,6




        };
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles; 
    }
}
