using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Mesh_unique : MonoBehaviour
{
    // Start는 에디터에서 한 번만 호출되므로, 컴포넌트를 껐다 켤 때도 다시 만들도록 OnEnable 사용
    void OnEnable()
      {
            // TODO 1: 원하는 다각형의 정점 좌표를 채우세요 (최소 4개)
          Vector3[] vertices = new Vector3[]
          {     //사각형용 벡터
                new Vector3(0f, 0f, 0f), // 0
                new Vector3(1f, 0f, 0f), // 1
                new Vector3(1f, 1f, 0f), // 2
                new Vector3(0f, 1f, 0f), // 3
                new Vector3(0f, 0f, 1f), // 4
                new Vector3(1f, 0f, 1f), // 5
                new Vector3(1f, 1f, 1f), // 6
                new Vector3(0f, 1f, 1f), // 7
                new Vector3(0.5f, 1.5f, 0.5f), // 8 (지붕 꼭짓점)

                





          };


          int[] squares = new int[]
          {
                        
            0,3,2,  0,2,1,   // 앞
            1,2,6,  5,1,6,   // 오른쪽 
            4,5,6,  4,6,7,   // 뒤 
            7,3,4,  3,0,4,   // 왼쪽 
            0,1,5,  0,5,4,   // 아래

            // 위 뚜껑 대신 점 8로 모이는 삼각형 4개 (지붕)
            3,8,2,           // 앞 지붕
            2,8,6,           // 오른쪽 지붕
            6,8,7,           // 뒤 지붕
            7,8,3            // 왼쪽 지붕

          };

          Mesh mesh = new Mesh();
          mesh.vertices = vertices;
          mesh.triangles = squares;
          mesh.RecalculateNormals();

          // MeshFilter / MeshRenderer가 없으면 자동으로 추가
          MeshFilter mf = GetComponent<MeshFilter>();
          if (mf == null) mf = gameObject.AddComponent<MeshFilter>();
          mf.sharedMesh = mesh;

          // material이 없으면 기본 Lit, 있으면 그대로 사용
          MeshRenderer mr = GetComponent<MeshRenderer>();
          if (mr == null) mr = gameObject.AddComponent<MeshRenderer>();
          if (mr.sharedMaterial == null)
                mr.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      
}

}
