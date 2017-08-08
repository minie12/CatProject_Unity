using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearScore : MonoBehaviour {
    Text ScoreText;

    GameObject GameManager;

    public int finalScore;

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("MainCamera");
        ScoreText = GameObject.Find("FinalScore").GetComponent<Text>();
    }
	
	public void setFinalScore()
    {
        Debug.Log(GameManager.name);
        finalScore =  GameManager.GetComponent<Minigame3_Mananger>().normalscore + GameManager.GetComponent<Minigame3_Mananger>().speacialscore * 7;
        ScoreText.text = "Score :" + finalScore.ToString();
    }
}
