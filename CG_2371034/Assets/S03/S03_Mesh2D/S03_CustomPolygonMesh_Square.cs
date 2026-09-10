  using UnityEngine;

  [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class S03_CustomPolygonMesh_Square : MonoBehaviour
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


                //오각형 전용 벡터 
                new Vector3(0.25f, 0.5f, 1f), // 8
                new Vector3(0.75f, 0.5f, 1f), // 9
                new Vector3(0.5f, 1.25f, 1f), // 10



          };

          // TODO 2: 정점 3개씩 묶어 삼각형들을 구성하세요
          int[] triangles = new int[]
          {
                        
            8,9,6,
            6,7,8,
            7,6,10

          };

          Mesh mesh = new Mesh();
          mesh.vertices = vertices;
          mesh.triangles = triangles;
          mesh.RecalculateNormals();

          GetComponent<MeshFilter>().mesh = mesh;
          GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      }
  }