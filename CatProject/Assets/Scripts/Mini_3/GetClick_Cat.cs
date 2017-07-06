using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClick_Cat : MonoBehaviour {

    GameObject cat;
	// Use this for initialization
	void Start () {
        cat = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        cat.GetComponent<DemandManager>().isRightAct();
    }
}
