using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    private Mesh mesh;
    private float fov;
    private Vector3 origin;

    float viewDistance;
    private List<GameObject> detected;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 360f;

        viewDistance = 2f;
        detected = new List<GameObject>();
    }
    private void Update()
    {
        //Ẩn enemy ngoài vùng phát hiện
        foreach (var unit in detected)
        {
            try
            {
                unit.GetComponent<Renderer>().enabled = false;
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        detected.Clear();

        //Tạo vùng raycast
        int rayCount = 360;
        float angle = 0;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        
        for (int i = 0; i <= rayCount; i++)
        {
            
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, UtilsClass.GetVectorFromAngle(angle), viewDistance,enemyLayer);
            if (raycastHit2D.collider != null)
            {
                
                raycastHit2D.collider.GetComponent<Renderer>().enabled = true;
                if (detected.IndexOf(raycastHit2D.collider.gameObject) < 0) {
                    detected.Add(raycastHit2D.collider.gameObject);
                }
                
                
            }

            Vector3 vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;


                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        
    }
    
    public void removeDetected(GameObject gameObject)
    {
        if (detected.IndexOf(gameObject) >= 0)
        {
            Debug.Log("2");
            detected.Remove(gameObject);
        }
        
    }
}
