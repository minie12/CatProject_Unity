using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

    GameObject FeverScore;
    GameObject GameManager;
    GameObject DataManager;
    GameObject Player;

    int[] bestScorearr;
    int bestScore;

    public Text best_score;
    public int totalS;

    public Text total_score;

	void Awake () {
        DataManager = GameObject.Find("DataManager");
        FeverScore = GameObject.Find("FeverScore");
        GameManager = GameObject.Find("GameManager");
        Player = GameObject.Find("Player");

        total_score = GameObject.Find("score_Total").GetComponent<Text>();
        best_score = GameObject.Find("score_Best").GetComponent<Text>();

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

        bestScorearr = DataManager.GetComponent<ControlGameData>().getBestScore();
        bestScore = bestScorearr[0];

        totalS = mainS + mouseS * 7;

        if (bestScore < totalS)
        {
            //Debug.Log("bestScore");
            bestScore = totalS;
            bestScorearr[0] = bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }


        best_score.text = "best : " + bestScore.ToString();
        total_score.text = "score : " + totalS.ToString();
    }


}
