using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 position;
    [SerializeField] private GameObject background;
    private Vector3 backgroundSize;
    private Vector3 anchorPoint;
    private Vector2 point;

    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;

        backgroundSize = background.GetComponent<Renderer>().bounds.size;
        anchorPoint = background.gameObject.transform.position - backgroundSize / 2;
        point = ArmySelectionController.getPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            //di chuyển
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
        var _lookRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.rotation = _lookRotation;
    }

    public void Move(Vector2 position,Vector3 point)
    {
        destination = position;
        position.x = point.x;
        position.y = point.y;
        this.point = point;
    }

}
