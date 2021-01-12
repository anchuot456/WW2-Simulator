using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject map;

    private Vector3 mapSize;
    private Vector3 anchorPoint;
    // Start is called before the first frame update
    void Start()
    {
        mapSize = map.GetComponent<Renderer>().bounds.size;
        anchorPoint = map.gameObject.transform.position - mapSize / 2;
        CreateGrid();
    }

    void CreateGrid()
    {
        
        for(int i = 0; i <= 4; i++)
        {
            //Hàng ngang
            GameObject line = new GameObject();
            line.AddComponent<LineRenderer>();
            LineRenderer row = line.GetComponent<LineRenderer>();
            row.positionCount = 2;
            Vector3 position = Vector3.zero;
            //Điểm A tại bên trái
            position.x = anchorPoint.x;
            position.y = i * mapSize.y / 4 + anchorPoint.y;
            row.SetPosition(0, position);
            //Điểm B tại bên phải
            position.x = mapSize.x + anchorPoint.x;
            position.y = i * mapSize.y / 4 + anchorPoint.y;
            row.SetPosition(1, position);
            row.SetWidth(0.1f, 0.1f);

            //Hàng dọc
            GameObject line1 = new GameObject();
            line1.AddComponent<LineRenderer>();
            LineRenderer col = line1.GetComponent<LineRenderer>();
            col.positionCount = 2;
            Vector3 position1 = Vector3.zero;
            //Điểm A tại bên dưới
            position1.x = i* mapSize.x / 4 + anchorPoint.x;
            position1.y = anchorPoint.y;
            col.SetPosition(0, position1);
            //Điểm B tại bên trên
            position1.x = i * mapSize.x / 4 + anchorPoint.x;
            position1.y = mapSize.y + anchorPoint.y;
            col.SetPosition(1, position1);
            col.SetWidth(0.1f, 0.1f);
        }
    }
}
