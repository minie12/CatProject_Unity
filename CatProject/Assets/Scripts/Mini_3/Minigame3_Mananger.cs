using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame3_Mananger : MonoBehaviour {
    int speacialscore;
    int normalscore;

    GameObject Game;
    GameObject FeverTime;
    GameObject GameOver;

	// Use this for initialization
	void Start () {

        speacialscore = 0;
        normalscore = 0;

        Game = GameObject.Find("Game");
        FeverTime = GameObject.Find("FeverTime");
        GameOver = GameObject.Find("GameOver");

        Game.SetActive(true);
        FeverTime.SetActive(false);
        GameOver.SetActive(false);

        StartCoroutine("Fever");
	}

    public void callFeverTime()
    {
        normalscore += Game.transform.Find("GameManager").gameObject.GetComponent<TotalManager>().secondsforScore;
        Game.transform.Find("GameManager").gameObject.GetComponent<TotalManager>().secondsforScore = 0;
        Game.SetActive(false);
        FeverTime.SetActive(true);
        
    }

    public void backtoGame()
    {
        Game.SetActive(true);
        speacialscore += FeverTime.GetComponent<FeverTimeScript>().touchnum;
        FeverTime.GetComponent<FeverTimeScript>().touchnum = 0;
        FeverTime.SetActive(false);
    }

    public void callGameover()
    {
        GameOver.SetActive(true);
        Game.SetActive(false);
    }

    IEnumerator Fever()
    {
        yield return new WaitForSeconds(60);
        callFeverTime();
        yield return new WaitForSeconds(5);
        backtoGame();

        StartCoroutine("Fever");
    }
}
