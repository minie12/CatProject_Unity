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

    int money;

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

        finalScore = (int)GameManager.GetComponent<Minigame3_Mananger>().finalScore;

        money = DataManager.GetComponent<ControlGameData>().getMoney();
        money += finalScore;

        //best score의 경우는 여기서 save 진행.
        if (bestScore < finalScore)
        {
            //Debug.Log("bestScore");
            bestScore = finalScore;
            bestScorearr[1] = bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }

        DataManager.GetComponent<ControlGameData>().setMoney(money);
        DataManager.GetComponent<PuzzleManager>().setting_Puzzle(finalScore);
        DataManager.GetComponent<ControlGameData>().Save("money");
        DataManager.GetComponent<ControlGameData>().Save("puzzle");

        BestScoreText.text = "Best Score : " + bestScore.ToString();
        ScoreText.text = "Score :" + finalScore.ToString();
    }
}
