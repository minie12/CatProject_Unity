using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClick_fur : MonoBehaviour {
    GameObject parentObj;
    // Use this for initialization
    void Start () {
        parentObj = gameObject.GetComponent<Transform>().parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        parentObj.GetComponent<FurManager>().GetObj(gameObject);
    }
}
