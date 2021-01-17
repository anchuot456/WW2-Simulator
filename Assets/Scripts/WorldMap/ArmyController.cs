using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyController : MonoBehaviour
{
    public static ArmyController instance;
    public List<GameObject> armies;
    public List<GameObject> PlayerAArmies;
    public List<GameObject> PlayerBArmies;
    // Start is called before the first frame update
    void Start()
    {


        if (instance == null)
        {
            armies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Army"));
            foreach (var unit in armies)
            {
                if (unit.layer == LayerMask.NameToLayer("PlayerA"))
                {
                    PlayerAArmies.Add(unit);
                }
                else
                {
                    PlayerBArmies.Add(unit);
                }
                var point = unit.GetComponent<ArmyMovement>();
                PlayerPrefs.SetFloat(unit.name + " Point", ArmySelectionController.getPoint(transform.position).y * 4 + ArmySelectionController.getPoint(transform.position).x);
                DontDestroyOnLoad(unit);
            }
            DontDestroyOnLoad(gameObject);
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cập nhật mảng unit sau mỗi frame
        foreach(var unit in armies)
        {
            //PlayerPrefs.SetFloat(unit.name + " Point", ArmySelectionController.getPoint(transform.position).y * 4 + ArmySelectionController.getPoint(transform.position).x);
            var tanks = unit.GetComponent<ArmyDetail>().tanks;
            var soldier= unit.GetComponent<ArmyDetail>().soldiers;
            if (tanks + soldier == 0)
            {
                if (unit.layer == LayerMask.NameToLayer("PlayerA"))
                {
                    PlayerAArmies.Remove(unit);
                }
                else
                {
                    PlayerBArmies.Remove(unit);
                }
                armies.Remove(unit);
                Destroy(unit);
            }
        }
        
    }
}
