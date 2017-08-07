﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame3_Mananger : MonoBehaviour {

    GameObject AudioManager;

    int speacialscore;
    public int normalscore;

    float playTime;
    float feverTime;

    Text NormalScoreText;
    Text SpecialScoreText;

    GameObject Game;
    GameObject FeverTime;
    GameObject GameOver;

    GameObject catmanagerObj;
    GameObject speacialScoreObj;

    GameObject[] FeverCat = new GameObject[3];

	// Use this for initialization
	void Start () {

        speacialscore = 0;
        normalscore = 0;

        playTime = 30;
        feverTime = 5;

        AudioManager = GameObject.Find("AudioManager");

        NormalScoreText = GameObject.Find("NormalScore").GetComponent<Text>();
        SpecialScoreText = GameObject.Find("SpecialScore").GetComponent<Text>();

        Game = GameObject.Find("Game");
        FeverTime = GameObject.Find("FeverTime");
        GameOver = GameObject.Find("GameOver");

        FeverCat[0] = FeverTime.transform.Find("Cat").transform.Find("fevercat1").gameObject;
        FeverCat[1] = FeverTime.transform.Find("Cat").transform.Find("fevercat2").gameObject;
        FeverCat[2] = FeverTime.transform.Find("Cat").transform.Find("fevercat3").gameObject;

        catmanagerObj = Game.transform.Find("CatManager").gameObject;
        speacialScoreObj = FeverTime.transform.Find("Background").gameObject;

        Game.SetActive(true);
        FeverTime.SetActive(false);
        GameOver.SetActive(false);

        StartCoroutine("Fever");
        AudioManager.GetComponent<Main_AudioManager>().setting();
	}

    public void callFeverTime()
    {
        if (catmanagerObj.GetComponent<CatManager>().realWaitTime >= 0)
            catmanagerObj.GetComponent<CatManager>().waitTime = catmanagerObj.GetComponent<CatManager>().realWaitTime;
        catmanagerObj.GetComponent<CatManager>().nowWait = false;

        Game.SetActive(false);
        FeverTime.SetActive(true);
        for(int i = 0; i < 3; i++)
        {
            FeverCat[i].GetComponent<Fever_ShakingTail>().fever_tail();
        }
        
    }

    public void backtoGame()
    {
        speacialscore += speacialScoreObj.GetComponent<FeverTimeScript>().touchnum;
        speacialScoreObj.GetComponent<FeverTimeScript>().touchnum = 0;
        showSpeacialscore();
        FeverTime.SetActive(false);

        Game.SetActive(true);
        Debug.Log("invokecoroutine calling");
        Game.GetComponent<InvokeCoroutine>().InvokingCoroutine();
    }

    public void callGameover()
    {
        GameOver.SetActive(true);
        Game.SetActive(false);
    }

    public void showNormalscore()
    {
        NormalScoreText.text = normalscore.ToString();
    }

    public void showSpeacialscore()
    {
        SpecialScoreText.text = speacialscore.ToString();
    }

    IEnumerator Fever()
    {
        yield return new WaitForSeconds(playTime);
        callFeverTime();
        yield return new WaitForSeconds(feverTime);
        backtoGame();

        StartCoroutine("Fever");
    }
}
