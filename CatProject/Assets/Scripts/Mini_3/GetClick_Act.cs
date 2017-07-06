using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClick_Act : MonoBehaviour {

    GameObject ActParent;
	// Use this for initialization
	void Start () {
        ActParent = gameObject.transform.parent.gameObject;
        Debug.Log(ActParent.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        ActParent.GetComponent<ActManager>().JudgeAct(gameObject);
    }
}
