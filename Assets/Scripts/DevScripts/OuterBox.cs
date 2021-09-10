using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class OuterBox : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector3[] normals;
    // Note: create a new set of vertices for each face in order to have proper normals(lighting)
    void Start()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        mesh = new Mesh();
        CreateShape();
        UpdateMesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshFilter>().mesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh.Optimize();
        gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        normals = GetComponent<MeshFilter>().sharedMesh.normals;
        foreach(Vector3 normal in normals)
            Debug.Log("Blue Cube: "+normal);
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
            2,1,0,
            3,1,2,

            5,2,0,
            0,4,5,

            0, 1, 6,
            4, 0,6,

            7,3,2,
            2,5,7,

            1,3,7,
            6,1,7,

            6,5,4,
            6,7,5
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < gameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length; i++)
        {
            Gizmos.color = Color.blue;
            //Debug.Log(gameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length);
            Gizmos.DrawLine(vertices[i], vertices[i]*200);
        }
    }
}
