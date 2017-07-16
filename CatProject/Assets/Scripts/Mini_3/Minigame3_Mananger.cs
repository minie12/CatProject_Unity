using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame3_Mananger : MonoBehaviour {
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

	// Use this for initialization
	void Start () {

        speacialscore = 0;
        normalscore = 0;

        playTime = 60;
        feverTime = 5;

        NormalScoreText = GameObject.Find("NormalScore").GetComponent<Text>();
        SpecialScoreText = GameObject.Find("SpecialScore").GetComponent<Text>();

        Game = GameObject.Find("Game");
        FeverTime = GameObject.Find("FeverTime");
        GameOver = GameObject.Find("GameOver");

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
        NormalScoreText.text = "Score : "+normalscore.ToString();
    }

    public void showSpeacialscore()
    {
        SpecialScoreText.text = "Special : " + speacialscore.ToString();
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
