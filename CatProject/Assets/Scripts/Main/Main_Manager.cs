using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main_Manager : MonoBehaviour
{

    GameObject AudioManager;
    GameObject DataManager;

    GameObject[] fursprObj = new GameObject[4];
    GameObject[] realFurObj = new GameObject[4];

    int i;
    int nowactiveIndex;

    // Use this for initialization
    void Start()
    {

        AudioManager = GameObject.Find("AudioManager");
        //Debug.Log("start at mainMainManager called");
        //오브젝트 찾아서 넣어주기
        fursprObj[0] = GameObject.Find("Shop_Sprite");
        fursprObj[1] = GameObject.Find("Collection_Sprite");
        fursprObj[2] = GameObject.Find("MiniGame_Sprite");
        fursprObj[3] = GameObject.Find("Setting_Sprite");

        realFurObj[0] = GameObject.Find("Real_Shop");
        realFurObj[1] = GameObject.Find("Real_Collection");
        realFurObj[2] = GameObject.Find("Real_MiniGame");
        realFurObj[3] = GameObject.Find("Real_Setting");

        //objsetactiveFalse(realFurObj);

        AudioManager.GetComponent<Main_AudioManager>().setting();

        nowactiveIndex = -1;
    }

    //콜라이더가 붙어있는 기존 오브젝트들의 콜라이더를 꺼 주고
    //해당 오브젝트를 켜 준다.
    //initial 작업 수행
    public void doyourJob(int objindex)
    {
        turnOffCollider();
        nowactiveIndex = objindex;
        //Debug.Log("nowactiveindex is "+nowactiveIndex);
        realFurObj[objindex].SetActive(true);
        realFurObj[objindex].GetComponent<CommonJob>().initial();
        
    }

    //켜져있던 오브젝트를 끄고, 콜라이더를 다 켜준다.
    public void backtoMain()
    {
        turnOnCollider();
        Debug.Log("nowactiveindex is " + nowactiveIndex);
        realFurObj[nowactiveIndex].SetActive(false);
        nowactiveIndex = -1;
    }

    //각각의 오브젝트의 콜라이더를 켜 준다
    void turnOnCollider()
    {
        for (i = 0; i < 4; i++)
        {
            fursprObj[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    //각각의 오브젝트의 콜라이더를 꺼 준다
    void turnOffCollider()
    {
        for (i = 0; i < 4; i++)
        {
            fursprObj[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void objsetactiveFalse(GameObject[] obj)
    {
        for (i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
        }
    }
}
