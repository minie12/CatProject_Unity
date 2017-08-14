using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnTouch_Camera : MonoBehaviour {
    GameObject MainManager;
    // Use this for initialization
    void Start () {
        MainManager = GameObject.Find("MainManager");
        Debug.Log("gameObject name is " + gameObject.name);
    }

    void OnMouseDown()
    {
        Debug.Log("onmousedown");
        MainManager.GetComponent<ChangeCameraPos>().ChangePos();
    }
}
