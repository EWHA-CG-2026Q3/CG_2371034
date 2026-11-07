
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Quad_Mesh : MonoBehaviour
{
    void OnEnable()
    {

        Vector3[] points =
        {
            new Vector3(-1f,   0f,  0f),     
            new Vector3(-0.7f, 0f, -0.5f),   
            new Vector3( 0f,   0f, -0.7f),  
            new Vector3( 0.7f, 0f, -0.5f),   
            new Vector3( 1f,   0f,  0f),     
            new Vector3( 0.5f, 0f, -0.25f), 
            new Vector3( 0f,   0f, -0.4f),   
            new Vector3(-0.5f, 0f, -0.25f),  
        };

        int[] triangles =
        {
            7, 1, 0,            
            6, 2, 1,   7, 6, 1,   
            5, 3, 2,   6, 5, 2,  
            5, 4, 3,              
        };


        Mesh mesh = new Mesh();
        mesh.vertices = points;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer.sharedMaterial == null)
            meshRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
    }
}
