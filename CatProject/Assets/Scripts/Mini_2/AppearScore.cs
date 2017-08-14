using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearScore : MonoBehaviour {
    Text ScoreText;
    Text BestScoreText;

    GameObject GameManager;
    GameObject DataManager;

    public int finalScore;
    int[] bestScorearr;
    int bestScore;

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("MainCamera");
        DataManager = GameObject.Find("DataManager");

        BestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        ScoreText = GameObject.Find("FinalScore").GetComponent<Text>();
    }
	
	public void setFinalScore()
    {
        Start();

        bestScorearr = DataManager.GetComponent<ControlGameData>().getBestScore();
        bestScore = bestScorearr[1];
        //Debug.Log(bestScore);

        finalScore =  GameManager.GetComponent<Minigame3_Mananger>().normalscore + GameManager.GetComponent<Minigame3_Mananger>().speacialscore * 7;

        //best score의 경우는 여기서 save 진행.
        if (bestScore < finalScore)
        {
            //Debug.Log("bestScore");
            bestScore = finalScore;
            bestScorearr[1] = bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }

        BestScoreText.text = "Best Score : " + bestScore.ToString();
        ScoreText.text = "Score :" + finalScore.ToString();
    }
}
