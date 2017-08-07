using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_BackToMain : MonoBehaviour {
    GameObject SceneManager;

	// Use this for initialization
	void Start () {
        SceneManager = GameObject.Find("SceneManager");
	}
	
	// Update is called once per frame
	void OnMouseDown () {
        //스코어 저장
        //퍼즐 체크해서 저장
        SceneManager.GetComponent<SceneMoving>().BacktoHome();
	}
}
