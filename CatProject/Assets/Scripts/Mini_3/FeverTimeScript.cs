using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTimeScript : MonoBehaviour
{
    public int touchnum;
    GameObject toy;

    void Start()
    {
        toy = GameObject.Find("FeverTime").transform.Find("Item").gameObject;
    }

    void OnMouseDown()
    {
        toy.transform.eulerAngles += new Vector3(0, 180, 0);
        touchnum++;
    }
}
