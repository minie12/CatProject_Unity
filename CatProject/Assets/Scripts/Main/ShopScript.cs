using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : CommonJob
{
    GameObject backButton;
    GameObject[] Button = new GameObject[2];
    GameObject[] Goods = new GameObject[4];
    GameObject Base_Shop;

    Sprite[] catSpr = new Sprite[8];
    Sprite[] furnitureSpr = new Sprite[8];

    string sprdir = "Main/ShopSprite";

    int i;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        backButton = GameObject.Find("Button_Back");
        Button[0] = GameObject.Find("Button_cat");
        Button[1] = GameObject.Find("Button_furniture");

        for (i = 0; i < 4; i++)
        {
            Goods[i] = GameObject.Find("Goods_" + i);
        }

        Base_Shop = GameObject.Find("Shop_Main");

        for (i = 0; i < 8; i++)
        {

        }

        backButton.SetActive(false);
        Button[0].SetActive(false);
        Button[1].SetActive(false);
        Base_Shop.SetActive(false);

    }



    public override void initial()
    {
        //데이터를 읽어오고 초기화시키기
        //Debug.Log("virtual function");
    }

    public override void finish()
    {
        //해당 작업을 끝내기.
    }

    public override void save()
    {
        //데이터값 저장
    }
}
