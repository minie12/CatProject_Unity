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

    GameObject FeverScore;

    Sprite[] playerS;

    int time;
    private int fevertime;

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

        GAMEOVER.SetActive(false);
        FEVER.SetActive(false);
        FeverPlayer.SetActive(false);

        for(int i=0; i<2; i++)
        {
            playerS[i] = Resources.Load<Sprite>("UFO_" + i);
        }


        InvokeRepeating("checkTime", 1, 1);
    }
	
    void checkTime()
    {
        time++;
        if(time == fevertime-1)
            Player.GetComponent<Collider2D>().enabled = false;

        if (time == fevertime) //피버타임 준비
        {   // 행성 바깥으로 이동 & 플레이어 중간으로 이동(터치막기)
            FeverPlayer.SetActive(true);
            
            GameManager.GetComponent<PlanetCreate>().Fever();   
            GameManager.GetComponent<ReadyFever>().ready();
            GameManager.GetComponent<MouseCreate>().MouseOFF();
        }


        if (fevertime+4 > time && time >= fevertime+3) //피버타임 시작
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

        if( fevertime+10.5> time && time >= fevertime + 9) //피버타임 정리
        {   // 피버스코어 정지 & 플레이어 이동가능 & 원래 플레이어 이미지로;
            FeverScore.GetComponent<Collider2D>().enabled = false;
            Player.GetComponent<SpriteRenderer>().sprite = playerS[0];
            Player.GetComponent<Collider2D>().enabled = true;

            FeverPlayer.SetActive(false);

            FeverManager.GetComponent<FeverManager>().Monster_back();

        }
        if( time >= fevertime + 12)
        {
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
    }

    public void GameOver()
    {
        gameObject.GetComponent<TotalScore>().TScore();
        GameManager.GetComponent<PlanetCreate>().PlanetOff();
        MAINGAME.SetActive(false);
        FEVER.SetActive(false);
        GAMEOVER.SetActive(true);
        Player.SetActive(false);
        CancelInvoke();
        
    }
}
