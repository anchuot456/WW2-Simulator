using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildArmy : MonoBehaviour
{
    public InputField field;
    public InputField point;

    // Start is called before the first frame update
    void Start()
    {
        field.text = "0";
        //field.readOnly = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseSoldierField()
    {
        if (int.Parse(point.text) > 0)
        {
            field.text = (int.Parse(field.text) + 1).ToString();
            point.text = (int.Parse(point.text) - 1).ToString();
        }

    }
    public void IncreaseTankField()
    {
        if (int.Parse(point.text) > 1)
        {
            field.text = (int.Parse(field.text) + 1).ToString();
            point.text = (int.Parse(point.text) - 2).ToString();
        }
    }
    public void DecreaseSoldierField()
    {
        if (int.Parse(field.text) > 0)
        {
            field.text = (int.Parse(field.text) - 1).ToString();
            point.text = (int.Parse(point.text) + 1).ToString();
        }
    }
    public void DecreaseTankField()
    {
        if (int.Parse(field.text) > 0)
        {
            field.text = (int.Parse(field.text) - 1).ToString();
            point.text = (int.Parse(point.text) + 2).ToString();
        }
    }
}
