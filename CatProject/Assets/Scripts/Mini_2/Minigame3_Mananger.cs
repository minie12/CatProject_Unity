using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame3_Mananger : MonoBehaviour {

    GameObject AudioManager;
    GameObject DataManager;
    GameObject FeverSign;

    public int speacialscore;
    public int normalscore;

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

    GameObject[] FeverCat = new GameObject[3];

    private void Awake()
    {
        Debug.Log("minigame3manager");
        AudioManager = GameObject.Find("AudioManager");
        AudioManager.GetComponent<Main_AudioManager>().setting();

        DataManager = GameObject.Find("DataManager");
        DataManager.GetComponent<ControlGameData>().Load();
    }

    // Use this for initialization
    void Start () {

        FeverSign = GameObject.Find("Fever_Sign");
        FeverSign.SetActive(false);

        speacialscore = 0;
        normalscore = 0;

        playTime = 30;
        feverTime = 5;

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

    IEnumerator Fever()
    {
        yield return new WaitForSeconds(playTime);
        FeverSign.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FeverSign.SetActive(false);
        callFeverTime();
        yield return new WaitForSeconds(feverTime);
        backtoGame();

        StartCoroutine("Fever");
    }
}
