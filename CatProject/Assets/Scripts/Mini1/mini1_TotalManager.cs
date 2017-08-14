using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini1_TotalManager : MonoBehaviour {

    GameObject AudioManager;

    GameObject GAMEOVER;
    GameObject FEVER;
    GameObject MAINGAME;
    GameObject GameManager;
    GameObject FeverManager;
    GameObject Player;

    GameObject FeverPlayer;
    GameObject TouchPlayer;

    GameObject FeverScore;

    Sprite[] playerS;

    int time;
    public int fevertime;

    int flag;

    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager");
    
        AudioManager.GetComponent<Main_AudioManager>().setting();
    }

    // Use this for initialization
    void Start () {
        time = 0;
        fevertime = 30;

        playerS = new Sprite[2];

        GAMEOVER = GameObject.Find("GAMEOVER");
        FEVER = GameObject.Find("FEVER");
        MAINGAME = GameObject.Find("MAINGAME");
        GameManager = GameObject.Find("GameManager");
        FeverManager = GameObject.Find("FeverManager");
        Player = GameObject.Find("Player");
        FeverScore = GameObject.Find("FeverScore");

        FeverPlayer = Player.transform.Find("FeverPlayer").gameObject;
        TouchPlayer = GameObject.Find("TouchPlayer");

        GAMEOVER.SetActive(false);
        FEVER.SetActive(false);
        FeverPlayer.SetActive(false);

        for(int i=0; i<2; i++)
        {
            playerS[i] = Resources.Load<Sprite>("UFO_" + i);
        }

        StartCoroutine(GameManager.GetComponent<TimeScore>().CountScore());
        StartCoroutine("CheckTime");
    }
	
    IEnumerator CheckTime()
    {
        time++;

        if (time == fevertime) //피버타임 준비
        {   // 행성 바깥으로 이동 & 플레이어 중간으로 이동(터치막기)

            Player.GetComponent<PlayerMove>().check = 1;

            FeverPlayer.SetActive(true);
            GameManager.GetComponent<PlanetCreate>().Fever();
            GameManager.GetComponent<ReadyFever>().ready();
            GameManager.GetComponent<MouseCreate>().MouseOFF();

            //Player.SetActive(false);
        }


        if (fevertime + 4 > time && time >= fevertime + 3) //피버타임 시작
        {   // 피버타임 ON & 플레이어 이미지 변경 & 몬스터 랜덤 켜기

            GameManager.GetComponent<PlanetCreate>().PlanetOff();

            Player.GetComponent<SpriteRenderer>().sprite = playerS[1];
            FEVER.SetActive(true);

            FeverManager.GetComponent<FeverManager>().Boss_ON();
            GameManager.GetComponent<MapMove>().enabled = true;
            GameManager.GetComponent<TimeScore>().enabled = true;
            GameManager.GetComponent<MouseCreate>().enabled = true;
            GameManager.SetActive(false);
            MAINGAME.SetActive(false);
        }

        if (fevertime + 9.5 > time && time >= fevertime + 8) //피버타임 정리
        {   // 피버스코어 정지 & 플레이어 이동가능 & 원래 플레이어 이미지로;
            FeverScore.GetComponent<Collider2D>().enabled = false;
            Player.GetComponent<SpriteRenderer>().sprite = playerS[0];

            FeverPlayer.SetActive(false);

            FeverManager.GetComponent<FeverManager>().Monster_back();

        }
        if (time >= fevertime + 11)
        {
            //TouchPlayer.SetActive(true);
            Player.GetComponent<PlayerMove>().check = 0;

            gameObject.GetComponent<TotalScore>().FScore();

            FeverManager.GetComponent<FeverManager>().Monster_normal();
            FeverScore.GetComponent<Collider2D>().enabled = true;
            FEVER.SetActive(false);

            GameManager.SetActive(true);
            MAINGAME.SetActive(true);

            GameManager.GetComponent<PlanetCreate>().FeverOff();

            gameObject.GetComponent<CoroutineManager>().ReStart();

            time = 0;

        }

        yield return new WaitForSeconds(1);
        StartCoroutine("CheckTime");
    }

    public void GameOver()
    {
        gameObject.GetComponent<TotalScore>().TScore();
        GameManager.GetComponent<PlanetCreate>().PlanetOff();
        MAINGAME.SetActive(false);
        FEVER.SetActive(false);
        GAMEOVER.SetActive(true);
        Player.SetActive(false);
        StopAllCoroutines();
        
    }
}
