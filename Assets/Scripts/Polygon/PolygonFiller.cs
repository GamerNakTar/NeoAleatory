using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonFiller : MonoBehaviour
{
    private void Start()
    {
        // PolygonCollider2D 가져오기
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();

        // Mesh 생성
        Mesh mesh = new Mesh();

        // Collider의 점들 가져오기
        Vector2[] points = polygonCollider.points;

        // Vector2를 Vector3로 변환 (Mesh는 Vector3 필요)
        Vector3[] vertices = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            vertices[i] = points[i];
        }

        // 삼각형 인덱스 생성
        Triangulator triangulator = new Triangulator(points);
        int[] triangles = triangulator.Triangulate();

        // Mesh에 데이터 설정
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // MeshRenderer 및 MeshFilter 추가
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Default";
        meshRenderer.sortingOrder = -10;

        // MeshFilter에 Mesh 할당
        meshFilter.mesh = mesh;

        // 단색 Material 생성
        Material material = new Material(Shader.Find("Unlit/Color"));
        material.color = Color.green; // 원하는 색상
        meshRenderer.material = material;
        meshRenderer.material.renderQueue = 1000;
    }

    void Update()
    {
        if (PauseMenu.IsPaused)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
