using System.Collections;
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
    float effectVol;

    AudioSource audioPlay;

    //Main
    AudioClip MainBGM;

    //Mini2
    AudioClip Mini2BGM;
    AudioClip furdisappear;


    public void Awake()
    {
        

        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        audioPlay = gameObject.GetComponent<AudioSource>();
        DataManager = GameObject.Find("DataManager");
        Debug.Log("Awake called");

        MainBGM = Resources.Load<AudioClip>("Sounds/Main/Main_BGM");
        Mini2BGM = Resources.Load<AudioClip>("Sounds/Mini2/Mini2_BGM");
        furdisappear = Resources.Load<AudioClip>("Sounds/Mini2/fur_disappear");

        //audioPlay.clip = MainBGM;
        Debug.Log(SceneManager.GetActiveScene().name);

        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                audioPlay.clip = MainBGM;
                nowScene = 0;
                break;
            case "Mini_1":
                //audioPlay.clip = Mini1BGM;
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





    }

    // Use this for initialization
    void Start()
    {
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
                //audioPlay.clip = Mini1BGM;
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

        bgmVol = volumn[nowScene][0];
        effectVol = volumn[nowScene][1];
        Debug.Log(bgmVol);
        Debug.Log(bgmVol / 8);
        audioPlay.volume = (bgmVol / 8);
    }
}
