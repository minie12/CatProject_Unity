using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini3_BackToMain : MonoBehaviour {

    GameObject SceneManager;
    GameObject DataManager;
    GameObject ScoreManager;

    int finalScore;
    int money;

    // Use this for initialization
    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        DataManager = GameObject.Find("DataManager");
        ScoreManager = GameObject.Find("Manager");

        money = DataManager.GetComponent<ControlGameData>().getMoney();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        //스코어 세팅
        finalScore = ScoreManager.GetComponent<UIManager>().totalScore;
        money += finalScore;
        DataManager.GetComponent<ControlGameData>().setMoney(money);
        DataManager.GetComponent<PuzzleManager>().setting_Puzzle(finalScore);


        //퍼즐 체크해서 저장
        if (gameObject.name == "back_box")
        {
            SceneManager.GetComponent<SceneMoving>().BacktoHome();
        }
        else if (gameObject.name == "PlayAgain_box")
            SceneManager.GetComponent<SceneMoving>().PlayAgain("Mini_3");
    }
}
