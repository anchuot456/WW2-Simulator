using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    Rigidbody2D rb;

    Vector3 destination;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            //xoay góc
            var angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            
            if (angle != 0.0f)
            {
                var _lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
            }
            
            //di chuyển
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            //rb.velocity = _direction * speed * Time.deltaTime;
            //transform.Translate((_direction) * speed * Time.deltaTime);
        }
    }

    public void Move(Vector2 position,Vector2 direction)
    {
        destination = position;
        this.direction = direction;
    }
}
