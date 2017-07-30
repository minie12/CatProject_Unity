﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : CommonJob
{ GameObject IntroObj;

    //고양이, 가구 배치를 위해서 메인화면에서의 오브젝트들 읽어와야함(setactive true, false 위해서)

    GameObject BaseObj;
    GameObject ExitButton;
    GameObject[] Category = new GameObject[3];

    GameObject GoodsParent;
    GameObject[] Goods = new GameObject[8];
    GameObject StoryParent;
    GameObject[] Story = new GameObject[3];

    GameObject InfoObj;
    GameObject InfoButton_Exit;
    GameObject InfoButton_Placement;

    Sprite[] catSpr = new Sprite[8];
    Sprite[] catInfoSpr = new Sprite[8];
    Sprite[] furnitureSpr = new Sprite[8];
    Sprite[] furnitureInfoSpr = new Sprite[8];
    Sprite[] placementSpr = new Sprite[2];

    /* 콜렉션에 필요한, 데이터에서 읽어와야 하는 정보들
      고양이의 구매 여부
      가구의 구매 여부 및 디벨롭 수준, 설치 정보
      소유한 금액
     */
    int[] buycat = new int[8]; //정수를 읽어와서 이진수로 나누면서 판단해야 함 --> 128 : 8번째만 구매함
    int[] furniture = new int[8]; //구매여부 및 디벨롭 여부 판가름할것. -1(구매안함)/012(구매&레벨업에 따라 1/2/3), 설치한 것은 레벨따라 345
    int[] playnum = new int[3];

    bool[] activecategory = new bool[3]; // cat-furniture-story

    string sprdir;
    int i;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        sprdir = "Main/ShopAndCollection/";

        IntroObj = GameObject.Find("Intro_Story");

        BaseObj = GameObject.Find("Base_Collection");
        ExitButton = GameObject.Find("C_ExitButton");
        Category[0] = GameObject.Find("CollectionCategory_C");
        Category[1] = GameObject.Find("CollectionCategory_F");
        Category[2] = GameObject.Find("CollectionCategory_S");

        GoodsParent = GameObject.Find("C_Goods");
        for (i = 0; i < 4; i++)
        {
            Goods[i] = GameObject.Find("C_Goods_" + i);
            Goods[i + 4] = GameObject.Find("C_Goods_" + (i + 4));

            catSpr[i] = Resources.Load<Sprite>(sprdir + "Cat_" + i);
            catSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Cat_" + (i + 4));
            furnitureSpr[i] = Resources.Load<Sprite>(sprdir + "Furniture_" + i);
            furnitureSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Furniture_" + (i + 4));
            catInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_C_" + i);
            catInfoSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Info_C_" + (i + 4));
            furnitureInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_F_" + i);
            furnitureInfoSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Info_F_" + (i + 4));
        }

        StoryParent = GameObject.Find("C_Story");
        for (i = 0; i < Story.Length; i++)
        {
            Story[i] = GameObject.Find("C_Story_" + i);
        }

        InfoObj = GameObject.Find("Collection_Info");
        InfoButton_Exit = GameObject.Find("Exit_Button");
        InfoButton_Placement = GameObject.Find("Placement_Button");

        placementSpr[0] = Resources.Load<Sprite>("Main/Collection/Collection_info_placement");
        placementSpr[0] = Resources.Load<Sprite>("Main/Collection/Collection_info_disposition");

        for (i = 0; i < activecategory.Length; i++)
            activecategory[i] = false;

        InfoObj.SetActive(false);
        StoryParent.SetActive(false);
        GoodsParent.SetActive(false);
        BaseObj.SetActive(false);
    }

    public override void initial()
    {
        //데이터를 읽어오고 초기화시키기
        furniture = MainManager.GetComponent<ControlGameData>().getFurniture();
        playnum = MainManager.GetComponent<ControlGameData>().getPlaynum();
        buycat = MainManager.GetComponent<ControlGameData>().getBuycat();


        //기본적으로 고양이를 켜고 시작하는 것으로.
        SettingCategory(0);
        BaseObj.SetActive(true);
    }

    public override void finish()
    {
        //해당 작업을 끝내기.
        save();
        InfoObj.SetActive(false);
        StoryParent.SetActive(false);
        GoodsParent.SetActive(false);
        BaseObj.SetActive(false);
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }

    public override void save()
    {
        MainManager.GetComponent<ControlGameData>().setFurniture(furniture);
        MainManager.GetComponent<ControlGameData>().setBuycat(buycat);
    }

    //카테고리 버튼 누를 때 생기는 변화 --> cat, 가구의 경우에는 sprite 세팅도 함께.
    public void SettingCategory(int index)
    {
        int goodsindex;

        for (i = 0; i < 3; i++)
        {
            if (i == index)
                activecategory[i] = true;
            else
                activecategory[i] = false;
        }

        if (index < 2)
        {
            GoodsParent.SetActive(true);
            StoryParent.SetActive(false);
        }
        else if (index == 2)
        {
            GoodsParent.SetActive(false);
            StoryParent.SetActive(true);
            
        }

        //sprite 세팅
        switch (index)
        {
            case 0:
                for (i = 0, goodsindex = 0; i < Goods.Length; i++)
                {
                    if (buycat[i] != -1)
                    {
                        Goods[goodsindex].GetComponent<SpriteRenderer>().sprite = catSpr[i];
                        goodsindex++;
                    }
                }
                break;
            case 1:
                for (i = 0, goodsindex = 0; i < Goods.Length; i++)
                {
                    if (furniture[i] != -1)
                    {
                        Goods[goodsindex].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i];
                        goodsindex++;
                    }
                }
                break;
            default:
                break;

        }
    }

    public void showInfo(int num)//해당 오브젝트에서 자신의 spritenum 잘라서 보내기{
    {
        turnOffCollider("Goods");

        if(activecategory[0] == true)
        {
            //고양이
            InfoObj.GetComponent<SpriteRenderer>().sprite = catInfoSpr[num];
            InfoButton_Placement.GetComponent<SpriteRenderer>().sprite = placementSpr[buycat[num]];
        }
        else if (activecategory[0] == true)
        {
            //가구
            InfoObj.GetComponent<SpriteRenderer>().sprite = furnitureInfoSpr[num];
            InfoButton_Placement.GetComponent<SpriteRenderer>().sprite = placementSpr[furniture[num]/3];
        }
    }

    /*이 함수는 나중에 오브젝트들 다 나오면 설정하기.
      해당 오브젝트의 index num을 전달해주니까 가구인지/고양이인지 체크해서 배치*/
    public void placeObj(int index)
    {

    }

    public void offInfo()
    {
        InfoObj.SetActive(false);
        turnOnCollider("Goods");
    }

    public void showStory(int num)//해당 오브젝트에서 자신의 스테이지num 잘라서 보내기
    {
        Debug.Log(playnum[num]);
        if (playnum[num] != 0)
            IntroObj.GetComponent<ShowingIntro>().callingIntro(gameObject, num);
    }

    public void finishStory()
    {
        turnOnCollider("Story");
    }

    void turnOffCollider(string category)
    {
        ExitButton.GetComponent<BoxCollider2D>().enabled = false;
        for (i = 0; i < Category.Length; i++)
        {
            Category[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        switch (category)
        {
            case "Goods":
                for (i = 0; i < Goods.Length; i++)
                {
                    Goods[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
            case "Story":
                for (i = 0; i < Story.Length; i++)
                {
                    Story[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
        }
    }

    //꺼줬던 콜라이더 켜 주는 작업 수행
    void turnOnCollider(string category)
    {
        ExitButton.GetComponent<BoxCollider2D>().enabled = true;
        for (i = 0; i < Category.Length; i++)
        {
            Category[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        switch (category)
        {
            case "Cat":
            case "Furniture":
                for (i = 0; i < Goods.Length; i++)
                {
                    Goods[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                break;
            case "Story":
                for (i = 0; i < Story.Length; i++)
                {
                    Story[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                break;
        }
    }
}