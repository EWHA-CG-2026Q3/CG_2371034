
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Wave_Blue : MonoBehaviour
{
    void OnEnable()
    {
        // 옆에서 본 모양 (x칸 = z좌표, y칸 = 높이)
        Vector2[] line =
        {
            // 0~3번: 바깥선 (뒤쪽 바닥 → 꼭대기)
            new Vector2(-2.0f, 0.0f), new Vector2(-1.6f, 1.4f), new Vector2(-0.5f, 2.4f), new Vector2(0.9f, 2.5f),

            // 4번: 말린 끝
            new Vector2(1.65f, 1.6f),

            // 5~8번: 안쪽선 (안쪽 벽 → 앞쪽 바닥)
            new Vector2(0.6f, 2.15f), new Vector2(-0.05f, 1.6f), new Vector2(0.35f, 0.6f), new Vector2(2.4f, 0.0f),
        };

        // 단면 6장의 위치와 크기를 직접 적음 (양 끝은 크기 0 = 한 점)
        float[] xs =    { -4f, -3f,   -1f,  1f,    3f,   4f };
        float[] sizes = {  0f, 0.55f, 0.9f, 0.95f, 0.6f, 0f };

        int sliceCount = xs.Length;      // 단면 수
        int lineCount = line.Length;     // 단면 하나의 점 수 (9개)

        // 꼭짓점 찍기
        Vector3[] points = new Vector3[sliceCount * lineCount];
        for (int s = 0; s < sliceCount; s++)
        {
            for (int i = 0; i < lineCount; i++)
            {
                points[s * lineCount + i] = new Vector3(xs[s], line[i].y * sizes[s], line[i].x * sizes[s]);
            }
        }

        // 2. 삼각형 잇기
        int[] triangles = new int[(sliceCount - 1) * (lineCount - 1) * 6];
        int index = 0;
        for (int s = 0; s < sliceCount - 1; s++)
        {
            for (int i = 0; i < lineCount - 1; i++)
            {
                int a = s * lineCount + i;        // 이번 단면의 점
                int b = a + 1;                    // 이번 단면의 다음 점
                int c = (s + 1) * lineCount + i;  // 다음 단면의 점
                int d = c + 1;                    // 다음 단면의 다음 점

                triangles[index++] = a;
                triangles[index++] = b;
                triangles[index++] = d;
                triangles[index++] = a;
                triangles[index++] = d;
                triangles[index++] = c;
            }
        }

        //메시에 넣기
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
