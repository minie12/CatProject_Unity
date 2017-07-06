using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalManager : MonoBehaviour {

    public int TotalFurNum;
    float gameOverTime;
    float setgameoverTimer;
    Text FurText;
    Text GameOverText;

    public bool startcor;

	// Use this for initialization
	void Start () {

        TotalFurNum = 0;
        setgameoverTimer = 5;

        gameOverTime = setgameoverTimer;
        startcor = false;

        FurText = GameObject.Find("FurText").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOverTimer").GetComponent<Text>();

        FurText.text = "";
        GameOverText.text = "";

        appearFurText();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(startcor == true)
        {
            if (TotalFurNum >= 12)
            {
                gameOverTime -= Time.deltaTime;
                
                GameOverText.text = gameOverTime.ToString();

                if (gameOverTime < 0)
                {
                    GameOverText.text = "gameover!";
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

}
