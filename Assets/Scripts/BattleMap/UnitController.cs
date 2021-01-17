using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static string myLayer;
    public List<GameObject> units;
    public List<GameObject> PlayerAUnits;
    public List<GameObject> PlayerBUnits;
    // Start is called before the first frame update
    private void Awake()
    {
        //Tìm localplayer
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.GetComponent<ArmySelectionController>().isLocalPlayer)
            {
                myLayer = player.GetComponent<ArmySelectionController>().myLayer;
            }

        }
    }
    void Start()
    {
        units = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        foreach (var unit in units)
        {
            if (unit.layer == LayerMask.NameToLayer(myLayer))
            {
                unit.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                unit.GetComponent<Renderer>().enabled = false;
            }
            if(unit.layer == LayerMask.NameToLayer("PlayerA"))
            {
                PlayerAUnits.Add(unit);
            }
            else
            {
                PlayerBUnits.Add(unit);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Cập nhật mảng unit sau mỗi frame
        if (myLayer == "PlayerA")
        {
            foreach (var unit in PlayerAUnits)
            {
                unit.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
