
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Wave_White : MonoBehaviour
{
    void OnEnable()
    {
     
        float[] sizes = { 0.55f, 0.67f, 0.78f, 0.87f, 0.94f, 1f, 0.97f, 0.92f, 0.83f, 0.72f, 0.6f };

        int sliceCount = sizes.Length;   // 단면 수
        int lineCount = 4;               // 단면 하나의 점 수

        // 꼭짓점 찍기
        Vector3[] points = new Vector3[sliceCount * lineCount];
        bool isLong = false;             
        for (int s = 0; s < sliceCount; s++)
        {
            float x = -3f + s * 0.6f;    // 왼쪽 끝 -3부터 오른쪽 끝 +3까지
            float size = sizes[s];

            points[s * 4 + 0] = new Vector3(x, 1.75f * size, -1.6f * size);   // 뒤쪽 아래
            points[s * 4 + 1] = new Vector3(x, 3.0f * size, 0.2f * size);     // 꼭대기
            points[s * 4 + 2] = new Vector3(x, 2.45f * size, 1.25f * size);   // 앞쪽

            // 앞쪽 끝: 번갈아 놓아 좌우로 지그재그가 됨
            if (isLong) points[s * 4 + 3] = new Vector3(x, 1.35f * size, 1.85f * size);
            else        points[s * 4 + 3] = new Vector3(x, 2.1f * size, 1.5f * size);

            isLong = !isLong;
        }

        //  삼각형 잇기
        int[] triangles = new int[(sliceCount - 1) * (lineCount - 1) * 6];
        int index = 0;
        for (int s = 0; s < sliceCount - 1; s++)
        {
            for (int i = 0; i < lineCount - 1; i++)
            {
                int a = s * 4 + i;        // 이번 단면의 점
                int b = a + 1;            // 이번 단면의 다음 점
                int c = (s + 1) * 4 + i;  // 다음 단면의 점
                int d = c + 1;            // 다음 단면의 다음 점

  
                triangles[index++] = a;
                triangles[index++] = b;
                triangles[index++] = d;
                triangles[index++] = a;
                triangles[index++] = d;
                triangles[index++] = c;
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
