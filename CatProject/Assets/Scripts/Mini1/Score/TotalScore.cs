using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

    GameObject FeverScore;
    GameObject GameManager;
    GameObject Player;

    public int totalS;
    public Text total_score;

	void Awake () {
        
        FeverScore = GameObject.Find("FeverScore");
        GameManager = GameObject.Find("GameManager");
        Player = GameObject.Find("Player");

        Debug.Log("feverscore"+FeverScore.name);
        total_score.text = "score : 0";
	}
	
    public void FScore()
    {
        Debug.Log("FScore is loaded");
        int tempF;
        Debug.Log(FeverScore.name);
        tempF = FeverScore.GetComponent<FeverScore>().Fscore;
        GameManager.GetComponent<TimeScore>().time += tempF*7;
    }

	public void TScore()
    {
        int mainS, mouseS;
        mainS = GameManager.GetComponent<TimeScore>().time;
        mouseS = Player.GetComponent<ColliderCheck>().count;

        totalS = mainS + mouseS*7;
        total_score.text = "score : " + totalS.ToString();
    }
}
