using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmySelectionController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject background;
    public static Vector3 backgroundSize;
    public static Vector3 anchorPoint;

    [SerializeField] private List<GameObject> selectedUnits;
    // Start is called before the first frame update
    void Start()
    {
        backgroundSize = background.GetComponent<Renderer>().bounds.size;
        anchorPoint = background.gameObject.transform.position - backgroundSize / 2;

        selectedUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        if (Input.GetMouseButtonDown(0))
        {
            selectedUnits.Clear();
        }
        else if (Input.GetMouseButtonUp(0))//nhấp chuột trái
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "Army" && hit.collider.gameObject.layer == LayerMask.NameToLayer(ArmyController.myLayer))
                {
                    selectedUnits.Add(hit.collider.gameObject);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))//nhấp chuột phải
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Vector3 clickPos = new Vector3(hit.point.x, hit.point.y, 0);
                Vector3 point = getPoint(clickPos);

                Vector3 position = getPosition(point);
                foreach (var unit in selectedUnits)
                {
                    //set tham số
                    var index = selectedUnits.IndexOf(unit);
                    unit.GetComponent<ArmyMovement>().Move(position,point);
                }
            }
        }
    }
    public static Vector2 getPoint(Vector2 pos)
    {
        Vector2 point = new Vector2();
        point.x = Mathf.Floor((pos.x - anchorPoint.x) / (backgroundSize.x / 4));
        point.y = Mathf.Floor((pos.y - anchorPoint.y) / (backgroundSize.y / 4));
        return point;
    }
    public static Vector2 getPosition(Vector2 point)
    {
        Vector2 position = new Vector2();
        position.x = point.x * backgroundSize.x / 4 + backgroundSize.x / 8 + anchorPoint.x;
        position.y = point.y * backgroundSize.y / 4 + backgroundSize.y / 8 + anchorPoint.y;
        return position;
    }
}
