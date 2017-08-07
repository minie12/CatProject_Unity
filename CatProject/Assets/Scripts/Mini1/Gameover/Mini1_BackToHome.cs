using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini1_BackToHome : MonoBehaviour
{
    GameObject SceneManager;
    GameObject DataManager;
    GameObject TotalManager;
    int money;

    // Use this for initialization
    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        DataManager = GameObject.Find("DataManager");
        TotalManager = GameObject.Find("TotalManager");

        money = DataManager.GetComponent<ControlGameData>().getMoney();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        //스코어 세팅
        money += TotalManager.GetComponent<TotalScore>().totalS;
        DataManager.GetComponent<ControlGameData>().setMoney(money);
        //퍼즐 체크해서 저장
        if (gameObject.name == "Button_home")
        {
            SceneManager.GetComponent<SceneMoving>().BacktoHome();
        }
        else if (gameObject.name == "Button_retry")
            SceneManager.GetComponent<SceneMoving>().PlayAgain("Mini_1");
    }


}
