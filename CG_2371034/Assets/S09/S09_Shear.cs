using UnityEngine;


[ExecuteAlways]
[RequireComponent(typeof(DiamondMesh))]
public class S09_Shear : MonoBehaviour
{
    [SerializeField] float k = (4f + 1f) / 5f;   // 기울이기 정도

    DiamondMesh diamondMesh;

    


    void OnEnable()
    {
        diamondMesh = GetComponent<DiamondMesh>();
    }

    void Update()
    {
        if (diamondMesh == null || diamondMesh.BaseVertices == null) return;

        float[,] S = ShearMatrixRaw(k);
        Vector3[] baseVertices = diamondMesh.BaseVertices;
        Vector3[] verts = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++){
            verts[i] = FromHomogeneous(MultiplyMatrixVectorRaw(S, ToHomogeneous(baseVertices[i])));
            if(i == 5)
            {
                Debug.Log("꼭대기 정점의 좌표는 " + verts[i]);
            }
        }
        diamondMesh.SetVertices(verts);
    }

    // 기울이기 행렬 (e₁, e₃, 원점은 그대로)
    // 변환 후 e₂ = (0, 1, 0) → (k, 1, 0) 이므로 이를 2열에 넣음
    float[,] ShearMatrixRaw(float k)
    {
        return new float[,] {
            { 1f, k,  0f, 0f },
            { 0f, 1f, 0f, 0f },
            { 0f, 0f, 1f, 0f },
            { 0f, 0f, 0f, 1f }
        };
    }

    Vector4 ToHomogeneous(Vector3 v)
    {
        return new Vector4(v.x, v.y, v.z, 1f);
    }

    Vector3 FromHomogeneous(Vector4 h)
    {
        return new Vector3(h.x, h.y, h.z);
    }

    Vector4 MultiplyMatrixVectorRaw(float[,] M, Vector4 v)
    {
        float[] input = { v.x, v.y, v.z, v.w };
        float[] result = new float[4];
        for (int row = 0; row < 4; row++)
            for (int col = 0; col < 4; col++)
                result[row] += M[row, col] * input[col];
        return new Vector4(result[0], result[1], result[2], result[3]);
    }
}
