using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{


    public GameObject unit;
    private Vector3 unitSize;

    public UnitController list;
    private List<GameObject> selectedUnits;

    public GameObject playerZone;

    //SelectionBox
    public RectTransform selectionBox;
    private bool isDragged;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        selectedUnits = new List<GameObject>();
        unitSize = unit.GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //Nhấp chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            //Bỏ chọn unit cũ
            selectedUnits.Clear();
            
            //Selection Box
            startPos = Input.mousePosition;
        }
        //Kéo chuột trái
        else if (Input.GetMouseButton(0))
        {
            if((Input.mousePosition - startPos).magnitude > 40)
            {
                isDragged = true;
                if (!selectionBox.gameObject.activeInHierarchy)
                    selectionBox.gameObject.SetActive(true);
                float width = Input.mousePosition.x - startPos.x;
                float height = Input.mousePosition.y - startPos.y;

                selectionBox.sizeDelta = new Vector3(Mathf.Abs(width), Mathf.Abs(height));
                selectionBox.anchoredPosition = startPos + new Vector3(width / 2, height / 2);
            }
            
        }
        //Thả chuột trái
        else if (Input.GetMouseButtonUp(0))
        {
            
            if (isDragged == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.tag == "Unit" && hit.collider.gameObject.layer == LayerMask.NameToLayer(UnitController.myLayer))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        selectedUnits.Add(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                selectionBox.gameObject.SetActive(false);

                Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
                Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

                foreach (GameObject unit in list.units)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
                    if (unit.layer == LayerMask.NameToLayer(UnitController.myLayer))
                    {
                        if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                        {
                            selectedUnits.Add(unit);
                        }
                    }
                }

                //Tắt isDragged
                isDragged = false;
            }
        }
        //Nhấp chuột phải
        else if (Input.GetMouseButtonDown(1))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Background")
                {
                    Vector3 clickPos = new Vector3(hit.point.x, hit.point.y, 0);
                    foreach (var unit in selectedUnits)
                    {
                        // Nếu còn thời gian set quân đầu trận
                        if (playerZone != null) 
                        {
                            Debug.Log(clickPos);
                            Vector2 min = playerZone.transform.position - (playerZone.GetComponent<Renderer>().bounds.size / 2);
                            Vector2 max = playerZone.transform.position + (playerZone.GetComponent<Renderer>().bounds.size / 2);
                            //Vector3 screenPos = Camera.main.WorldToScreenPoint(clickPos);
                            if (clickPos.x > min.x && clickPos.x < max.x && clickPos.y > min.y && clickPos.y < max.y)
                            {
                                //tính góc di chuyển
                                var direction = Vector3.right;
                                var angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.right);
                                angle = Mathf.Deg2Rad * (angle - 90);

                                //set tham số
                                var index = selectedUnits.IndexOf(unit);
                                unit.transform.position = clickPos + (index) * unitSize.x * 1.1f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                                unit.GetComponent<Movement>().Move(clickPos + (index) * unitSize.x * 1.1f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0), direction);
                                var _lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
                                unit.transform.rotation = _lookRotation;
                            }
                            
                        }
                        else //Hết thời gian set quân
                        {
                            //tính góc di chuyển
                            var direction = clickPos - selectedUnits[0].transform.position;
                            var angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.right);
                            angle = Mathf.Deg2Rad * (angle - 90);

                            //set tham số
                            var index = selectedUnits.IndexOf(unit);
                            unit.GetComponent<Movement>().Move(clickPos + (index) * unitSize.x * 1.1f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0), direction);
                        }
                    }
                }
            }
        }
    }
}
