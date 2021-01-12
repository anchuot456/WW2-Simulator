using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyDetail : MonoBehaviour
{
    public int soldiers;
    public int tanks;
    public Status status;

    public enum Status { Idle,InBattle};

    // Start is called before the first frame update
    void Start()
    {
        status = Status.Idle;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetStatus(Status status)
    {
        this.status = status;
    }
    public Status GetStatus()
    {
        return status;
    }
    public void SetDetail(int soldier,int tank)
    {
        this.soldiers = soldier;
        this.tanks = tank;
    }
}
