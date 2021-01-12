using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private GameObject[] armies;
    [SerializeField] private GameObject soldierA;
    [SerializeField] private GameObject soldierB;
    [SerializeField] private Transform startPosA;
    [SerializeField] private Transform startPosB;
    [SerializeField] private UnitController list;
    public ArmyDetail playerADetail;
    public ArmyDetail playerBDetail;
    private void Awake()
    {
        var unitSize = soldierA.GetComponent<Renderer>().bounds.size;
        var list = GameObject.Find("UnitController").GetComponent<UnitController>();
        armies = GameObject.FindGameObjectsWithTag("Army");
        foreach(var unit in armies)
        {
            if (unit.GetComponent<ArmyDetail>().GetStatus()==ArmyDetail.Status.InBattle)
            {
                if (unit.name.Contains("PlayerA")) {
                    playerADetail = unit.GetComponent<ArmyDetail>();
                }
                else
                {
                    playerBDetail = unit.GetComponent<ArmyDetail>();
                }
                
            }
        }
        //PlayerA
        for (int i = 0; i < playerADetail.soldiers; i++)
        {
            var pos = startPosA.position + (i- playerADetail.soldiers) * unitSize.x * 1.1f * new Vector3(0, 1, 0);
            var unit = Instantiate(soldierA, pos, Quaternion.LookRotation(Vector3.forward, Vector3.right));
            unit.GetComponent<Attack>().setList(list);
        }
        for (int i = 0; i < playerADetail.tanks; i++)
        {
            var pos = startPosA.position + (i - playerADetail.tanks) * unitSize.x * 1.1f * new Vector3(-1, 1, 0);
            var unit = Instantiate(soldierA, pos, Quaternion.LookRotation(Vector3.forward, Vector3.right));
            unit.GetComponent<Attack>().setList(list);
        }
        //PlayerB
        for (int i = 0; i < playerBDetail.soldiers; i++)
        {
            var pos = startPosB.position + (i - playerBDetail.soldiers) * unitSize.x * 1.1f * new Vector3(0, 1, 0);
            var unit = Instantiate(soldierB, pos, Quaternion.LookRotation(Vector3.forward, Vector3.right));
            unit.GetComponent<Attack>().setList(list);
        }
        for (int i = 0; i < playerBDetail.tanks; i++)
        {
            var pos = startPosB.position + (i - playerBDetail.tanks) * unitSize.x * 1.1f * new Vector3(-1, 1, 0);
            var unit = Instantiate(soldierB, pos, Quaternion.LookRotation(Vector3.forward, Vector3.right));
            unit.GetComponent<Attack>().setList(list);
        }
    }
    private void Update()
    {
        if (list.PlayerAUnits.Count==0)
        {
            Debug.Log(gameObject.name + "Point" +PlayerPrefs.GetFloat("Point"));
            //DontDestroyOnLoad(gameObject);
            foreach (var unit in armies)
            {
                if (unit.GetComponent<ArmyDetail>().GetStatus() == ArmyDetail.Status.InBattle)
                {
                    playerADetail.soldiers = 0;
                    playerADetail.tanks = 0;
                    playerBDetail.soldiers = GameObject.FindGameObjectsWithTag("Unit").Length;
                    playerBDetail.status = ArmyDetail.Status.Idle;
                }
            }
            SceneManager.LoadScene(1);
        }else if (list.PlayerBUnits.Count == 0)
        {
            Debug.Log(gameObject.name+"Point" + PlayerPrefs.GetFloat("Point"));
            //DontDestroyOnLoad(gameObject);
            foreach (var unit in armies)
            {
                if (unit.GetComponent<ArmyDetail>().GetStatus() == ArmyDetail.Status.InBattle)
                {
                    playerBDetail.soldiers = 0;
                    playerBDetail.tanks = 0;
                    playerADetail.soldiers = GameObject.FindGameObjectsWithTag("Unit").Length;
                    playerADetail.status = ArmyDetail.Status.Idle;
                }
            }
            SceneManager.LoadScene(1);
        }
    }
}
