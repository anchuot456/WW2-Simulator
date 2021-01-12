using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateArmy : MonoBehaviour
{
    public InputField soldierField;
    public InputField tankField;

    public GameObject playerAArmy;
    public GameObject playerBArmy;

    private Vector2 spawnPos;

    private int soldier;
    private int tank;
    // Start is called before the first frame update
    void Start()
    {
        if (ArmyController.myLayer == "PlayerA")
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
