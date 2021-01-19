using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : NetworkBehaviour
{
    public float speed = 5f;

    
    [SerializeField] private GameObject background;
    private Vector3 backgroundSize;
    private Vector3 anchorPoint;
    private Vector2 point;

    [SyncVar]
    private Vector3 destination;
    [SyncVar]
    private Vector2 position;

    private static event Action<Vector3,Vector2,uint> OnDestinationChanged;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;

        backgroundSize = background.GetComponent<Renderer>().bounds.size;
        anchorPoint = background.gameObject.transform.position - backgroundSize / 2;
        point = ArmySelectionController.getPoint(transform.position);

        OnDestinationChanged += HandleNewDestination;
    }

    public void HandleNewDestination(Vector3 point,Vector2 position,uint netID)
    {
        if (netId == netID)
        {
            Debug.Log(netId);
            Debug.Log("set position OK");
            destination = position;
            position.x = point.x;
            position.y = point.y;
            this.point = point;
            Debug.Log(destination);
        }
        
    }
    [ClientCallback]
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

    [Client]
    public void Move(Vector2 position, Vector3 point,uint netID)
    {
        Debug.Log("client OK");
        Debug.Log(netId);
        CmdMove(position, point, netID);
    }
    [Command]
    public void CmdMove(Vector2 position,Vector3 point, uint netID)
    {
        Debug.Log("CMD OK");
        RpcMove(position, point, netID);
    }
    [ClientRpc]
    public void RpcMove(Vector2 position,Vector3 point, uint netID)
    {
        Debug.Log("RPC OK");
        OnDestinationChanged?.Invoke(point,position,netID);
        
    }

}
