﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame3_Mananger : MonoBehaviour {

    GameObject AudioManager;
    GameObject DataManager;
    GameObject FeverSign;

    public int speacialscore;
    public int normalscore;
    public double finalScore;

    float playTime;
    float feverTime;

    Text NormalScoreText;
    Text SpecialScoreText;
    Text FeverText;
    Text FinalScoreText;
    Text BestScoreText;

    GameObject Game;
    GameObject FeverTime;
    GameObject GameOver;

    GameObject GameOverScore;

    GameObject catmanagerObj;
    GameObject speacialScoreObj;

    GameObject GameManager;

    GameObject[] FeverCat = new GameObject[3];

    double[] appliedEffect;

    double bonusAfterGame;
    int feverPlayTime;
    int gamePlayTime;
    public int bonusWhileGame;
    int bonusAfterFever;
    int jackpot;


    private void Awake()
    {
        //Debug.Log("minigame3manager");
        AudioManager = GameObject.Find("AudioManager");
        AudioManager.GetComponent<Main_AudioManager>().setting();

        DataManager = GameObject.Find("DataManager");
        DataManager.GetComponent<ControlGameData>().Load();
    }

    // Use this for initialization
    void Start () {

        appliedEffect = DataManager.GetComponent<GetCatEffect>().SettingCatEffect();

       //Debug.Log("appliedEffect[0]" + appliedEffect[0]);
        //Debug.Log("appliedEffect[6]" + appliedEffect[6]);

        bonusAfterGame = 1 + appliedEffect[0] + appliedEffect[6];
        feverPlayTime = 0 + (int)appliedEffect[4];
        gamePlayTime = 0 + (int)appliedEffect[1];
        bonusWhileGame = (int)appliedEffect[2];
        bonusAfterFever = (int)appliedEffect[3];
        jackpot = (int)appliedEffect[5];

        Debug.Log("bonusaftergame is " + bonusAfterGame);
        Debug.Log("feverPlaytime is" + feverPlayTime);
        Debug.Log("gameplaytime is " + gamePlayTime);
        Debug.Log("bonuswhilegame is " + bonusWhileGame);
        Debug.Log("bonusafterfever is " + bonusAfterFever);
        Debug.Log("jackpot is " + jackpot);
        //appliedEffect[0] 

        GameManager = GameObject.Find("GameManager");
        if (bonusWhileGame != 0)
        {
            GameManager.GetComponent<TotalManager>().gameBonus = bonusWhileGame;
            StartCoroutine(GameManager.GetComponent<TotalManager>().GameBonus());
        }
            

        FeverSign = GameObject.Find("Fever_Sign");
        FeverSign.SetActive(false);

        speacialscore = 0;
        normalscore = 0;

        playTime = 30 + gamePlayTime;
        feverTime = 5 + feverPlayTime;

        GameOverScore = GameObject.Find("GameOverImg");
        FinalScoreText = GameObject.Find("FinalScore").GetComponent<Text>();
        FinalScoreText.text = "";

        NormalScoreText = GameObject.Find("NormalScore").GetComponent<Text>();
        SpecialScoreText = GameObject.Find("SpecialScore").GetComponent<Text>();
        FeverText = GameObject.Find("FeverText").GetComponent<Text>();
        BestScoreText = GameObject.Find("BestScore").GetComponent<Text>();

        FeverText.text = "";
        BestScoreText.text = "";

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
       
	}

    public void callFeverTime()
    {
        if (catmanagerObj.GetComponent<CatManager>().realWaitTime >= 0)
            catmanagerObj.GetComponent<CatManager>().waitTime = catmanagerObj.GetComponent<CatManager>().realWaitTime;
        catmanagerObj.GetComponent<CatManager>().nowWait = false;

        Game.SetActive(false);
        FeverTime.SetActive(true);
        FeverText.text = "0 HIT!";

        for(int i = 0; i < 3; i++)
        {
            FeverCat[i].GetComponent<Fever_ShakingTail>().fever_tail();
        }
        
    }

    public void backtoGame()
    {
        speacialscore += speacialScoreObj.GetComponent<FeverTimeScript>().touchnum;
        speacialScoreObj.GetComponent<FeverTimeScript>().touchnum = 0;
        normalscore += bonusAfterFever;
        showNormalscore();
        showSpeacialscore();
        FeverText.text = "";
        FeverTime.SetActive(false);

        Game.SetActive(true);
        Debug.Log("invokecoroutine calling");
        Game.GetComponent<InvokeCoroutine>().InvokingCoroutine();
    }

    public void callGameover()
    {
        GameOver.SetActive(true);
        calculFinalScore();
        GameOverScore.GetComponent<AppearScore>().setFinalScore();
        Game.SetActive(false);
        FeverTime.SetActive(false);
        StopCoroutine("Fever");
    }

    public void showNormalscore()
    {
        NormalScoreText.text = normalscore.ToString();
    }

    public void showSpeacialscore()
    {
        SpecialScoreText.text = speacialscore.ToString();
    }

    public void calculFinalScore()
    {
        Debug.Log("final score is " + (normalscore + speacialscore * 7) + "and bonusAfterGame is "+bonusAfterGame+", jackpot is ");

        finalScore = (normalscore + speacialscore * 7) * bonusAfterGame;
        int i = Random.Range(0, 100);
        Debug.Log("jackpot possible?" + jackpot);
        if (i < 5)
            finalScore *= jackpot;
    }

    IEnumerator Fever()
    {
        yield return new WaitForSeconds(playTime);
        FeverSign.SetActive(true);
        yield return new WaitForSeconds(1f);
        FeverSign.SetActive(false);
        callFeverTime();
        yield return new WaitForSeconds(feverTime);
        backtoGame();

        StartCoroutine("Fever");
    }
}
