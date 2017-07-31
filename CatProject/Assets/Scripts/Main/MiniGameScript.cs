using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameScript : CommonJob {

    GameObject IntroObj;
    GameObject selectObj;

    string[] scene = new string[3];
    int[] playnum = new int[3];

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //Debug.Log("minigamescript start method");

        IntroObj = GameObject.Find("Intro_Story");
        selectObj = GameObject.Find("MiniGame_Select");

        //Debug.Log("start method_mainmanager name is " + MainManager.name);

        scene[0] = "Mini_1";
        scene[1] = "Mini_2";
        scene[2] = "Mini_3";

        selectObj.SetActive(false);
        //IntroObj.SetActive(false);
    
}

    public override void initial()
    {
        //Debug.Log("initial function from minigamescript");
        //각 미니게임별 플레이 횟수를 읽어오기
        Debug.Log(MainManager.name);
        selectObj.SetActive(true);
        playnum = MainManager.GetComponent<ControlGameData>().getPlaynum();

    }

    public void playGame(int clickindex)
    {
        if (playnum[clickindex]==0)
        {
            selectObj.SetActive(false);
            playnum[clickindex]++;
            save();
            IntroObj.SetActive(true);
            IntroObj.GetComponent<ShowingIntro>().callingIntro(gameObject, clickindex);
        }
        else
        {
            selectObj.SetActive(false);
            playnum[clickindex]++;
            //플레이횟수 저장하고 게임으로 가기
            save();
            //게임으로 가기!
            SceneManager.LoadScene(scene[clickindex]);
        }
        
    }

    //x표 클릭한 경우 --> goout
    public void goout()
    {
        Debug.Log(selectObj.name);
        selectObj.SetActive(false);
        save();
        finish();
    }

    public override void finish()
    {
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }

    public override void save()
    {
        //플레이 횟수를 저장
        MainManager.GetComponent<ControlGameData>().setPlaynum(playnum);
        MainManager.GetComponent<ControlGameData>().Save("playnum");
    }
}

