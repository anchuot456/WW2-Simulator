using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }else if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
}
