using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_BackToMain : MonoBehaviour {
    GameObject SceneManager;
    GameObject DataManager;
    GameObject ScoreManager;

    int money;

    // Use this for initialization
    void Start () {
        SceneManager = GameObject.Find("SceneManager");
        DataManager = GameObject.Find("DataManager");
        ScoreManager = GameObject.Find("GameOverImg");

        money = DataManager.GetComponent<ControlGameData>().getMoney();
    }
	
	// Update is called once per frame
	void OnMouseDown () {
        //스코어 세팅
        money += ScoreManager.GetComponent<AppearScore>().finalScore;
        DataManager.GetComponent<ControlGameData>().setMoney(money);
        //퍼즐 체크해서 저장
        if (gameObject.name == "BackToMain")
        {
            SceneManager.GetComponent<SceneMoving>().BacktoHome();
        }
        else if (gameObject.name == "TryAgain")
            SceneManager.GetComponent<SceneMoving>().PlayAgain("Mini_2");
    }
}
