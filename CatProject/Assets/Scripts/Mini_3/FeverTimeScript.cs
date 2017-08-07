using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTimeScript : MonoBehaviour
{
    public int touchnum;
    GameObject toy;
    Text FeverText;

    void Start()
    {
        toy = GameObject.Find("FeverTime").transform.Find("Item").gameObject;
        FeverText = GameObject.Find("FeverText").GetComponent<Text>();
        
    }

    void OnMouseDown()
    {
        toy.transform.eulerAngles += new Vector3(0, 180, 0);
        touchnum++;

        FeverText.text = touchnum.ToString() + " HIT!";
    }
}
