using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : NetworkBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    Rigidbody2D rb;

    [SyncVar]
    public Vector3 destination;
    [SyncVar]
    public Vector3 direction;

    private static event Action<Vector2, Vector2, uint> OnDestinationChanged;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();

        OnDestinationChanged += HandleNewDestination;
    }

    public void HandleNewDestination(Vector2 position, Vector2 direction, uint netID)
    {
        destination = position;
        this.direction = direction;
    }

    // Update is called once per frame
    [ClientCallback]
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
        }
    }

    [Client]
    public void Move(Vector2 position,Vector2 direction, uint netID)
    {
        CmdMove(position, direction, netID);
    }
    [Command(ignoreAuthority = true)]
    public void CmdMove(Vector2 position, Vector2 direction, uint netID)
    {
        RpcMove(position, direction, netID);
    }
    [ClientRpc]
    public void RpcMove(Vector2 position, Vector2 direction, uint netID)
    {
        OnDestinationChanged?.Invoke(position, direction, netID);
    }
}
