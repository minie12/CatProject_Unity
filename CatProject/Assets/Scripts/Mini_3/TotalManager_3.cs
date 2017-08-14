using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalManager_3 : MonoBehaviour
{
    GameObject AudioManager;
    GameObject mainGame;
    GameObject feverTime;
    GameObject feverPresent;
    GameObject gameOver;
    GameObject gameOverScene;
    GameObject UIManager;

    GameObject feverTimeFrame;

    Sprite[] gameoverSpr = new Sprite[2];

    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager");
        AudioManager.GetComponent<Main_AudioManager>().setting();
    }

    // Use this for initialization
    void Start()
    {
        mainGame = GameObject.Find("MainGame");
        feverTime = GameObject.Find("FeverTime");
        feverPresent = GameObject.Find("feverPresent");
        gameOver = GameObject.Find("GameOver");
        UIManager = GameObject.Find("Manager");
        for (int i = 0; i < 2; i++)
        {
            gameoverSpr[i] = Resources.Load<Sprite>("Sprites/GameOver_box_" + (i+1));
        }
        gameOverScene = GameObject.Find("GameOverScene");

        feverTimeFrame = GameObject.Find("FeverTimeFrame");

        mainGame.SetActive(true);
        feverTime.SetActive(false);
        gameOver.SetActive(false);
        feverTimeFrame.SetActive(false);
    }

    public void IsFever()
    {
        feverPresent.GetComponent<Transform>().position = new Vector3(-13.67f, 0.38f, 1);
        mainGame.SetActive(false);
        feverTime.SetActive(true);
        feverPresent.SetActive(true);
    }

    public void MainGameOn()
    {
        StartCoroutine(GameObject.Find("Manager").GetComponent<UIManager>().SetScore());
        feverTime.SetActive(false);
        mainGame.SetActive(true);
        GameObject.Find("Warehouse").GetComponent<SpawnBox>().CallBox();
    }

    public void CatEnd()
    {
        mainGame.SetActive(false);
        gameOver.SetActive(true);
        feverTime.SetActive(false);
        gameOverScene.GetComponent<SpriteRenderer>().sprite = gameoverSpr[0];
        GameObject.Find("Manager").GetComponent<UIManager>().CalcTotal();
        UIManager.SetActive(false);
    }

    public void ToyEnd()
    {
        mainGame.SetActive(false);
        gameOver.SetActive(true);
        feverTime.SetActive(false);
        gameOverScene.GetComponent<SpriteRenderer>().sprite = gameoverSpr[1];
        GameObject.Find("Manager").GetComponent<UIManager>().CalcTotal();
        UIManager.SetActive(false);
    }
}
