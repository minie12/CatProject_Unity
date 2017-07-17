﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{

    public bool[] requestSelected = new bool[3];
    GameObject[] requestObj = new GameObject[3];

    int[] sprindex = new int[3];

    Sprite[][] myActspr = new Sprite[3][];
    string[] filedir = new string[3];

    int i;

    // Use this for initialization
    void Start()
    {
        requestObj[0] = gameObject.transform.Find("Touch").gameObject;
        requestObj[1] = gameObject.transform.Find("Eat").gameObject;
        requestObj[2] = gameObject.transform.Find("Play").gameObject;

        for (i = 0; i < 3; i++)
        {
            myActspr[i] = new Sprite[2];

            switch (i)
            {
                case 0:
                    filedir[i] = "Pic_3/button_brush";
                    break;
                case 1:
                    filedir[i] = "Pic_3/button_can";
                    break;
                case 2:
                    filedir[i] = "Pic_3/button_mouse";
                    break;

            }
            myActspr[i][0] = Resources.Load<Sprite>(filedir[i]);
            myActspr[i][1] = Resources.Load<Sprite>(filedir[i] + "_selected");
        }

        sprindex[0] = 0;
        sprindex[1] = 0;
        sprindex[2] = 0;
    }

    public void JudgeAct(GameObject callobj)
    {
        if (callobj.transform.name == "Touch")
            setting(0);

        else if (callobj.transform.name == "Eat")
            setting(1);

        else if (callobj.transform.name == "Play")
            setting(2);
    }

    void setting(int num)
    {
        if (sprindex[num] == 1) // 선택을 취소하는 거라면 -- 선택한 것을 다시 누름
        {
            requestSelected[num] = false;
            sprindex[num] = 0;
            requestObj[num].GetComponent<SpriteRenderer>().sprite = myActspr[num][0];
        }

        else
        {
            //아니라면
            for (i = 0; i < 3; i++)
            {
                if (i == num)
                {
                    requestSelected[i] = true;
                    sprindex[i] = 1;
                    requestObj[i].GetComponent<SpriteRenderer>().sprite = myActspr[i][1];
                }

                else
                {
                    requestSelected[i] = false;
                    sprindex[i] = 0;
                    requestObj[i].GetComponent<SpriteRenderer>().sprite = myActspr[i][0];
                }
            }
        }

    }
}
