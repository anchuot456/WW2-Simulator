using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateArmy : NetworkBehaviour
{
    public InputField soldierField;
    public InputField tankField;

    public GameObject playerAArmy;
    public GameObject playerBArmy;

    public ArmySelectionController player;

    private Vector2 spawnPos;

    private int soldier;
    private int tank;
    // Start is called before the first frame update
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var index in players)
        {
            if (index.GetComponent<ArmySelectionController>().isLocalPlayer)
            {
                player = index.GetComponent<ArmySelectionController>();
                break;
            }
        }

        if (player.myLayer == "PlayerA")
        {
            spawnPos = new Vector2(0, 3);
        }
        else
        {
            spawnPos = new Vector2(3, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        soldier = int.Parse(soldierField.text);
        tank = int.Parse(tankField.text);
    }
    public void BuildNewArmy()
    {

        Vector3 position = ArmySelectionController.getPosition(spawnPos);
        var _lookRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        var rotation = _lookRotation;

        var newArmy = Instantiate(playerAArmy, position, rotation);
        newArmy.GetComponent<ArmyDetail>().SetDetail(soldier, tank);
    }
}
