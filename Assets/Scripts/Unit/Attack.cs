using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float hp;

    private GameObject target;
    [SerializeField] private UnitController list;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            target.GetComponent<Attack>().decreaseHP(damage);
        }

        if (hp <= 0)
        {
            foreach(var unit in list.units)
            {
                unit.GetComponentInChildren<FieldOfView>().removeDetected(gameObject);
                Debug.Log("1");
            }
            list.units.Remove(gameObject);
            if(gameObject.layer == LayerMask.NameToLayer("PlayerA"))
            {
                list.PlayerAUnits.Remove(gameObject);
            }
            else
            {
                list.PlayerBUnits.Remove(gameObject);
            }
            Destroy(gameObject);
        }
    }
    public void decreaseHP(float damage)
    {
        hp -= damage * Time.deltaTime;
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }
    public void setList(UnitController list)
    {
        this.list = list;
    }
}
