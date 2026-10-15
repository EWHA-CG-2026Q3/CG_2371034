using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Mesh_square : MonoBehaviour
{
     void Start()
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





          };


          int[] squares = new int[]
          {
                        
            0,3,2,  0,2,1,   // 앞
            1,2,6,  5,1,6,   // 오른쪽 
            4,5,6,  4,6,7,   // 뒤 
            7,3,4,  3,0,4,   // 왼쪽 
            3,7,6,  3,6,2,   // 위 
            0,1,5,  0,5,4    // 아래 

          };

          Mesh mesh = new Mesh();
          mesh.vertices = vertices;
          mesh.triangles = squares;
          mesh.RecalculateNormals();

          GetComponent<MeshFilter>().mesh = mesh;
          // material이 없으면 기본 Lit, 있으면 그대로 사용
          MeshRenderer mr = GetComponent<MeshRenderer>();
          if (mr.sharedMaterial == null)
                mr.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      
}


}
