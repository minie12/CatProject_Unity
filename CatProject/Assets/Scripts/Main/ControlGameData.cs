﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGameData : MonoBehaviour {

    int[] playnum = new int[3]; // 각 미니게임의 플레이 횟수 계산
    int[] buycat = new int[8];//총 8개의 고양이. 각 가구의 구매여부 및 배치 여부 판가름 --> -1/0/1
    int[] furniture = new int[8]; // 각 가구의 구매여부 및 디벨롭 여부 판가름 --> -1/012/345
    int[] puzzle = new int[6]; //퍼즐 및 퍼즐의 모은 퍼즐조각 여부 저장하는 배열(012, 8가지에 대해서 이진수 저장)
    int money;
    int[] volumn = new int[4];// a/10 --> 브금, a%10 --> 효과음, 메인/1/2/3, 0 56789 !!!!

    int i;
    string str;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    //파일에서 받아와서 수행한다.
    void Start () {
       PlayerPrefs.DeleteAll();
        //Debug.Log("controlgamedata !");
        Load();
    }

    //어플리케이션을 끌 때
    void saveforFinish()
    {
        PlayerPrefs.SetString("Playnum", makeString(playnum));
        PlayerPrefs.SetString("Buycat", makeString(buycat));
        PlayerPrefs.SetString("Furniture", makeString(furniture));
        PlayerPrefs.SetString("Puzzle", makeString(puzzle));
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetString("Volumn", makeString(volumn));
    } 

    public void Save(string savingsection)
    {
        switch (savingsection)
        {
            case "playnum":
                PlayerPrefs.SetString("Playnum", makeString(playnum));
                break;
            case "cat":
                PlayerPrefs.SetString("Buycat", makeString(buycat));
                break;
            case "furniture":
                PlayerPrefs.SetString("Furniture", makeString(furniture));
                break;
            case "puzzle":
                PlayerPrefs.SetString("Puzzle", makeString(puzzle));
                break;
            case "money":
                PlayerPrefs.SetInt("Money", money);
                break;
            case "volumn":
                PlayerPrefs.SetString("Volumn", makeString(volumn));
                break;
        }
    }

    void Load()
    {
        string[] f_playnum = PlayerPrefs.GetString("Playnum", "0/0/0").Split('/');
        string[] f_buycat = PlayerPrefs.GetString("Buycat", "-1/-1/-1/-1/-1/-1/-1/-1").Split('/');
        string[] f_furniture = PlayerPrefs.GetString("Furniture", "-1/-1/-1/-1/-1/-1/-1/-1").Split('/');
        string[] f_puzzle = PlayerPrefs.GetString("Puzzle", "0/0/0/0/0/0").Split('/');
        int f_money = PlayerPrefs.GetInt("Money", 0);
        string[] f_volumn = PlayerPrefs.GetString("Volumn", "99/99/99/99").Split('/');

        for (i = 0; i < playnum.Length; i++)
        {
            playnum[i] = int.Parse(f_playnum[i]);
            
            //Debug.Log(i+"th playnum is " + playnum[i] + " and puzzle is " + puzzle[i]);
        }

        for (i = 0; i < puzzle.Length; i++)
        {
            //Debug.Log(puzzle.Length);
            puzzle[i] = int.Parse(f_puzzle[i]);
        }

        
        for (i = 0; i < furniture.Length; i++)
        {
            buycat[i] = int.Parse(f_buycat[i]);
            furniture[i] = int.Parse(f_furniture[i]);
            //Debug.Log("ith furniture is " + furniture[i]);
        }
        for (i = 0; i < volumn.Length; i++)
        {
            volumn[i] = int.Parse(f_volumn[i]);
            Debug.Log("ith volumn is " + volumn[i]);
        }

        money = f_money;
    }

    string makeString(int[] arr)
    {
        str = "";

        for (i = 0; i < arr.Length - 1; i++)
        {
            str += arr[i].ToString();
            str += "/";
        }

        str = str + arr[i];

        //print("returned str is " + str);
        return str;
    }

    public void setPlaynum(int[] arr)
    {
        playnum = arr;
    }

    public void setBuycat(int[] arr)
    {
        buycat = arr;
    }

    public void setFurniture(int[] arr)
    {
        furniture = arr;
    }

    public void setPuzzle(int[] arr)
    {
        puzzle = arr;
    }

    public void setMoney(int num)
    {
        money = num;
    }

    public void setVolumn(int[] arr)
    {
        volumn = arr;
    }

    public int[] getPlaynum()
    {
        return playnum;
    }

    public int[] getBuycat()
    {
        return buycat;
    }

    public int[] getFurniture()
    {
        return furniture;
    }

    public int[] getPuzzle()
    {
        return puzzle;
    }

    public int getMoney()
    {
        return money;
    }

    public int[] getVolumn()
    {
        return volumn;
    }
}
