using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // for score
    public float timer = 0;
    public Text scoreText;
    public int seconds;
    // for present
    public Text presentText;
    public int presentNum = 0;
    // for speed
    public float speed;
    //fever
    int playTime;
    int feverTime;
    //TotalScore
    GameObject totalText;
    Text BestScoreText;

    //for calculation best score
    GameObject DataManager;
    int[] bestScorearr;
    int bestScore;

    public int totalScore;

    // Use this for initialization
    void Start()
    {


        DataManager = GameObject.Find("DataManager");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        presentText = GameObject.Find("SpecialScore").GetComponent<Text>();
        totalText = GameObject.Find("TotalScore");
        BestScoreText = GameObject.Find("BestScore").GetComponent<Text>();

        playTime = 10;
        feverTime = 7;

        totalText.SetActive(false);
        speed = 0.055f;
        StartCoroutine("SpeedSetting");
        StartCoroutine("FeverSetting");
        StartCoroutine("SetScore");

        presentText.text = "0";
        BestScoreText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //for setting time = score
        timer += Time.deltaTime;    
    }

    //increasing speed every 30 seconds
    IEnumerator SpeedSetting()
    {
        if (speed < 0.1f)
        {
            yield return new WaitForSeconds(30);
            speed += 0.007f;
        }

        StartCoroutine(SpeedSetting());
    }

    IEnumerator FeverSetting()
    {
        yield return new WaitForSeconds(playTime);
        Debug.Log("Fever");
        StopCoroutine("SetScore");        
        GameObject.Find("Warehouse").GetComponent<SpawnBox>().StopBox();
        yield return new WaitForSeconds(feverTime);

        StartCoroutine(FeverSetting());
    }

    public void PresentPlus()
    {
        presentNum += 1;
        //for setting speical score (7 times at the end)
        presentText.text = presentNum.ToString();
    }

    public void CalcTotal()
    {
        totalScore = presentNum * 7 + seconds;
        totalText.GetComponent<Text>().text = "Total Score: " + totalScore.ToString();
        totalText.SetActive(true);

        bestScorearr = DataManager.GetComponent<ControlGameData>().getBestScore();
        bestScore = bestScorearr[2];

        if (bestScore < totalScore)
        {
            //Debug.Log("bestScore");
            bestScore = totalScore;
            bestScorearr[2] = bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }

        BestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    public IEnumerator SetScore()
    {
        Debug.Log("hi");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("over");
        seconds++;
        scoreText.text = seconds.ToString();
        StartCoroutine("SetScore");
    }
}
