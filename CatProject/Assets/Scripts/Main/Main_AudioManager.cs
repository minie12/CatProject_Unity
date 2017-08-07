﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//BGM 플레이해주는것.
public class Main_AudioManager : MonoBehaviour
{

    GameObject DataManager;

    int[][] volumn = new int[4][];

    int nowScene;

    float bgmVol;
    public float effectVol;
    public Vector3 effectVector;

    AudioSource audioPlay;

    //Main
    AudioClip MainBGM;

    //mini1
    AudioClip Mini1BGM;
    public AudioClip cat_hit;
    public AudioClip fever_laser;
    public AudioClip mouse_get;

    //Mini2
    AudioClip Mini2BGM;
    public AudioClip furdisappear;
    public AudioClip cat_feelingGood;
    public AudioClip cat_feelingBad;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        audioPlay = gameObject.GetComponent<AudioSource>();
        DataManager = GameObject.Find("DataManager");
        Debug.Log("Awake called");

        //bgm
        MainBGM = Resources.Load<AudioClip>("Sounds/Main/Main_BGM");
        Mini1BGM = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_BGM");
        Mini2BGM = Resources.Load<AudioClip>("Sounds/Mini2/Mini2_BGM");

        //mini1 effect
        cat_hit = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_hit");
        fever_laser = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_laser");
        mouse_get = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_rat");

        //mini2 effect
        furdisappear = Resources.Load<AudioClip>("Sounds/Mini2/fur_disappear");
        cat_feelingBad = Resources.Load<AudioClip>("Sounds/Mini2/Cat_feelingBad");
        cat_feelingGood = Resources.Load<AudioClip>("Sounds/Mini2/Cat_feelingGood");



        //audioPlay.clip = MainBGM;
        Debug.Log(SceneManager.GetActiveScene().name);

        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                audioPlay.clip = MainBGM;
                nowScene = 0;
                break;
            case "Mini_1":
                audioPlay.clip = Mini1BGM;
                nowScene = 1;
                break;
            case "Mini_2":
                audioPlay.clip = Mini2BGM;
                nowScene = 2;
                break;
            case "Mini_3":
                //audioPlay.clip = Mini3BGM;
                nowScene = 3;
                break;
        }

        audioPlay.Play();
        // setting();
        for (int i = 0; i < 4; i++)
            volumn[i] = new int[2];
        //setting();
    }

    public void setting()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                audioPlay.clip = MainBGM;
                nowScene = 0;
                break;
            case "Mini_1":
                audioPlay.clip = Mini1BGM;
                nowScene = 1;
                break;
            case "Mini_2":
                audioPlay.clip = Mini2BGM;
                nowScene = 2;
                break;
            case "Mini_3":
                //audioPlay.clip = Mini3BGM;
                nowScene = 3;
                break;
        }

        changeVolumn();
        audioPlay.Play();
        /*
        Debug.Log("audiomanager");
        changeVolumn();
        audioPlay.Play();
        */
    }

    public void changeVolumn()
    {
        int[] tempvolumn;

        tempvolumn = DataManager.GetComponent<ControlGameData>().getVolumn();

        for (int i = 0; i < 4; i++)
        {
            volumn[i][0] = tempvolumn[i] / 10; // 배경음악 볼륨
            volumn[i][1] = tempvolumn[i] % 10; // 효과음 볼륨
            Debug.Log(tempvolumn[i] + " " + volumn[i][0]);
        }

        bgmVol = volumn[nowScene][0]; // --> 5,6,7,8,9 vol%5+1
        effectVol = volumn[nowScene][1];

        if (bgmVol != 0)
            bgmVol = ((bgmVol % 5) + 1) / 5;

        if (effectVol != 0)
            effectVol = ((effectVol % 5) + 1) / 5;

        audioPlay.volume = bgmVol;

        effectVector = new Vector3(0, 0, -(0 + effectVol * 10));

        Debug.Log(bgmVol + " is bgmVol and effectVol is " + effectVol);
    }
}