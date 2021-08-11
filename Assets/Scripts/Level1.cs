using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Level1 : MonoBehaviour
{

    public Material houseMaterial;
    // Start is called before the first frame update
    void Start()
    {
        TwoSidedMesh();
    }

    // Update is called once per frame
    void TestVertex()
    {
        ProBuilderMesh house = ProBuilderMesh.Create(
            new Vector3[] {
            new Vector3(0f,0f,0f),
            new Vector3 (100f, 0f, 0f),
            new Vector3 (0f, 0f, 100f),
            new Vector3 (100f, 0f, 100f),

            new Vector3(0f,0f,0f),
            new Vector3 (100f, 0f, 0f),
            new Vector3 (0f, 0f, 100f),
            new Vector3 (100f, 0f, 100f)
            },
            new Face[]
            {
                new Face(new int[] {0,1,2, 1,3,2})
            });
        house.SetMaterial(house.faces, houseMaterial);
        house.Refresh();
        house.ToMesh();
    }

    void TwoSidedMesh()
    {
        ProBuilderMesh house = ProBuilderMesh.Create(
            new Vector3[] {
            new Vector3(0f,0f,0f),
            new Vector3 (150f, 0f, 0f),
            new Vector3 (0f, 0f, 150f),
            new Vector3 (150f, 0f, 150f),

            new Vector3(0f,0f,0f),
            new Vector3 (150f, 0f, 0f),
            new Vector3 (0f, 0f, 150f),
            new Vector3 (150f, 0f, 150f)
            },
            new Face[]
            {
                new Face(new int[] {0,2,1}),
                new Face(new int[] {2,3,1}),
                new Face(new int[] {2,0,1}),
                new Face(new int[] {2,1,3}),
            });
        house.SetMaterial(house.faces, houseMaterial);
        //should be used if generating Meshes during update
        //house.Refresh(); 
        //house.ToMesh();
    }
}
