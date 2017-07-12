using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeCoroutine : MonoBehaviour {

    GameObject GameManager;
    GameObject CatManager;

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager");
        CatManager = GameObject.Find("CatManager");
	}

    public void InvokingCoroutine()
    {
        StartCoroutine(GameManager.GetComponent<TotalManager>().countSeconds());
        StartCoroutine(CatManager.GetComponent<CatManager>().appearCat());
        for(int i=0;i< CatManager.GetComponent<CatManager>().nowCat; i++)
        {
            GameObject tempcat = CatManager.GetComponent<CatManager>().CatList[i];
            StartCoroutine(tempcat.GetComponent<FurManager>().appearFur());
            StartCoroutine(tempcat.GetComponent<DemandManager>().appearDemand());
        }
    }
}
