
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Swirl : MonoBehaviour
{
    void OnEnable()
    {
       
        float[] ys =    { 0f, 0f, 0.5f, 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4f };
        float[] sizes = { 0f, 1f, 1f,   1f, 1f,   1f, 1f,   1f, 1f,   1f, 0f };

        int sliceCount = ys.Length;   // 단면 수
        int ringCount = 12;           // 단면 하나의 점 수
        float twist = 20f;            // 한 층마다 더 돌리는 각도

        // 꼭짓점 찍기: 단면마다 점 12개를 원을 따라 30도 간격으로 찍음
        Vector3[] points = new Vector3[sliceCount * ringCount];
        for (int s = 0; s < sliceCount; s++)
        {
            bool isOut = true;        
            for (int j = 0; j < ringCount; j++)
            {
                float radius = 0.75f;
                if (isOut) radius = 1f;
                isOut = !isOut;

                float rad = (j * 30f + s * twist) * Mathf.Deg2Rad;   
                float c = Mathf.Cos(rad);
                float sn = Mathf.Sin(rad);

                points[s * ringCount + j] = new Vector3(
                    c * radius * sizes[s],
                    ys[s],
                    sn * radius * sizes[s]);
            }
        }

        // 삼각형 잇기
        int[] triangles = new int[(sliceCount - 1) * ringCount * 6];
        int index = 0;
        for (int s = 0; s < sliceCount - 1; s++)
        {
            for (int j = 0; j < ringCount; j++)
            {
                int next = j + 1;
                if (next == ringCount) next = 0;        
                int a = s * ringCount + j;              
                int b = s * ringCount + next;          
                int c = (s + 1) * ringCount + j;       
                int d = (s + 1) * ringCount + next;     

                
                triangles[index++] = a;
                triangles[index++] = c;
                triangles[index++] = d;
                triangles[index++] = a;
                triangles[index++] = d;
                triangles[index++] = b;
            }
        }

        // 메시에 넣기
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
