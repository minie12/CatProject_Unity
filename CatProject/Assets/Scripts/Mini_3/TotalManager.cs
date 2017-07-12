﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalManager : MonoBehaviour
{

    public int TotalFurNum;
    public int secondsforScore;

    float gameOverTime;
    float setgameoverTimer;

    Text FurText;
    Text GameOverText;

    public bool startcor;

    GameObject MiniGame3_Manager;

    // Use this for initialization
    void Start()
    {

        TotalFurNum = 0;
        secondsforScore = 0;

        setgameoverTimer = 5;
        gameOverTime = setgameoverTimer;
        startcor = false;

        FurText = GameObject.Find("FurText").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOverTimer").GetComponent<Text>();

        MiniGame3_Manager = GameObject.Find("MainCamera");

        FurText.text = "";
        GameOverText.text = "";

        appearFurText();
        StartCoroutine("countSeconds");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startcor == true)
        {
            if (TotalFurNum >= 12)
            {
                gameOverTime -= Time.deltaTime;

                GameOverText.text = gameOverTime.ToString();

                if (gameOverTime < 0)
                {
                    GameOverText.text = "gameover!";
                    MiniGame3_Manager.GetComponent<Minigame3_Mananger>().callGameover();
                }

            }

            if (TotalFurNum < 12)
            {
                GameOverText.text = "";
                setgameoverTimer -= 0.5f;
                gameOverTime = setgameoverTimer;
                startcor = false;
            }
        }
    }

    public void appearFurText()
    {
        FurText.text = TotalFurNum.ToString();
    }

    public IEnumerator countSeconds()
    {
        //Debug.Log("countseconds called");
        yield return new WaitForSeconds(1);
        secondsforScore++;

        StartCoroutine("countSeconds");
    }

}
