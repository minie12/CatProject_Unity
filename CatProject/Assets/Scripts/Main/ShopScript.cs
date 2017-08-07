using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopScript : CommonJob
{
    GameObject backButton;
    GameObject[] Button = new GameObject[2];
    GameObject[] Goods = new GameObject[4];
    GameObject[] Arrow = new GameObject[2];
    GameObject Shop_Background;
    GameObject SelectObj;
    GameObject InfoObj;

    Text MoneyText;

    Sprite[] catSpr = new Sprite[8];
    Sprite[] furnitureSpr = new Sprite[8];

    Sprite[] catInfoSpr = new Sprite[8];
    Sprite[] furnitureInfoSpr = new Sprite[8];

    string sprdir = "Main/ShopAndCollection/";


    int i;

    bool want_cat;
    bool want_furniture;

    /* 상점에 필요한, 데이터에서 읽어와야 하는 정보들
      고양이의 구매 여부
      가구의 구매 여부 및 디벨롭 수준
      소유한 금액
     */
    int[] buycat = new int[8]; //정수를 읽어와서 이진수로 나누면서 판단해야 함 --> 128 : 8번째만 구매함
    int[] furniture = new int[8]; //구매여부 및 디벨롭 여부 판가름할것. -1(구매안함)/012(구매&레벨업에 따라 1/2/3), 설치한 것은 레벨따라 345
    int money;

    /* 정의해줘야 하는 부분 
     고양이 가격
     가구별 가격 + 레별 별 가격변화
     */
    int[] catCost = new int[8];
    int[] furnitureCost = new int[8];


    // Use this for initialization
    public override void Start()
    {
        base.Start();

        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        MoneyText.text = "";

        backButton = GameObject.Find("Button_back");
        Button[0] = GameObject.Find("Button_cat");
        Button[1] = GameObject.Find("Button_furniture");

        Arrow[0] = GameObject.Find("Arrow_Left");
        Arrow[1] = GameObject.Find("Arrow_Right");

        InfoObj = GameObject.Find("Info_Obj");

        //goods object를 찾아서 넣어주고
        //고양이 / 가구 sprite를 찾아서 넣어준다
        for (i = 0; i < 4; i++)
        {
            Goods[i] = GameObject.Find("Goods_" + i);
            catSpr[i] = Resources.Load<Sprite>(sprdir + "Cat_" + i);
            catSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Cat_" + (i + 4));
            furnitureSpr[i] = Resources.Load<Sprite>(sprdir + "Furniture_" + i);
            furnitureSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Furniture_" + (i + 4));
            catInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_C_" + i);
            catInfoSpr[i+4] = Resources.Load<Sprite>(sprdir + "Info_C_" + (i + 4));
            furnitureInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_F_" + i);
            furnitureInfoSpr[i+4] = Resources.Load<Sprite>(sprdir + "Info_F_" + (i + 4));
        }

        Shop_Background = GameObject.Find("Shop_Main"); // 
        SelectObj = GameObject.Find("Base_Shop");

        want_cat = false;
        want_furniture = false;

        backButton.SetActive(false);
        Button[0].SetActive(false);
        Button[1].SetActive(false);
        InfoObj.SetActive(false);
        SelectObj.SetActive(false);
        Shop_Background.SetActive(false);
        //Debug.Log("hi");
    }

    //데이터를 읽어오고 초기화시키기
    public override void initial()
    {
        money = DataManager.GetComponent<ControlGameData>().getMoney();
        buycat= DataManager.GetComponent<ControlGameData>().getBuycat();
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();

        Shop_Background.SetActive(true);
        Button[0].SetActive(true);
        Button[1].SetActive(true);
        backButton.SetActive(true);

        MoneyText.text = money.ToString();
    }

    public override void finish()
    {
        //해당 작업을 끝내기.
        
        Button[0].SetActive(false);
        Button[1].SetActive(false);
        backButton.SetActive(false);
        InfoObj.SetActive(false);
        SelectObj.SetActive(false);
        Shop_Background.SetActive(false);

        MoneyText.text = "";

        save();
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }

    public override void save()
    {
        DataManager.GetComponent<ControlGameData>().setMoney(money);
        DataManager.GetComponent<ControlGameData>().setFurniture(furniture);
        DataManager.GetComponent<ControlGameData>().setBuycat(buycat);
        DataManager.GetComponent<ControlGameData>().Save("cat");
        DataManager.GetComponent<ControlGameData>().Save("furniture");
        DataManager.GetComponent<ControlGameData>().Save("money");
    }

    //furniture / cat 중 뭘 눌렀는지 확인해서 로딩
    public void getCategorybutton(string objname)
    {
        SelectObj.SetActive(true);
        switch (objname)
        {
            case "Button_cat":
                want_cat = true;
                want_furniture = false;
                for (i = 0; i < 4; i++)
                {
                    Goods[i].GetComponent<SpriteRenderer>().sprite = catSpr[i];
                }
                break;
            case "Button_furniture":
                want_cat = false;
                want_furniture = true;
                for (i = 0; i < 4; i++)
                {
                    Goods[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i];
                }
                break;
        }
    }

    //왼쪽인지 / 오른쪽인지 판단하고 고양이/가구 판단해서 sprite 로딩
    public void getArrow(string objname)
    {
        int index = 0;

        if (objname == "Arrow_Left")
            index = 0;
        else if(objname == "Arrow_Right")
            index = 4;

        if (want_cat == true)
        {
            for (i = 0; i < 4; i++, index++)
            {
                Goods[i].GetComponent<SpriteRenderer>().sprite = catSpr[index];
                //Debug.Log(i + ", " + index);
            }
        }
        else if (want_furniture == true)
        {
            for (i = 0; i < 4; i++, index++)
            {
                Goods[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[index];
            }
        }
    }

    //해당 오브젝트의 설명을 띄워줌
    //해당 함수의 인자는 sprite의 이름을 보내준다.
    //이미 구매가 진행된 고양이/업그레이드가 완료된 가구의 경우에는 로딩 자체를 해주지 않는 것으로 한다.
    public void showObjinfo(string clickedSpr)
    {
        //string spritename = clickedObj.GetComponent<SpriteRenderer>().sprite.name;
        //int sprindex = int.Parse(spritename.Substring(spritename.Length - 1));
        int sprindex = int.Parse(clickedSpr.Substring(clickedSpr.Length - 1));

        if (want_cat == true && buycat[sprindex] != 1)
        {
            InfoObj.SetActive(true);
            InfoObj.GetComponent<SpriteRenderer>().sprite = catInfoSpr[sprindex];
            turnOffCollider();
        }
        else if(want_furniture == true && furniture[sprindex]%3 != 2)
        {
            InfoObj.SetActive(true);
            InfoObj.GetComponent<SpriteRenderer>().sprite = furnitureInfoSpr[sprindex];
            turnOffCollider();
        }
    }

    //해당 오브젝트의 설명을 끔 
    public void offObjinfo()
    {
        //해당 오브젝트 꺼주기
        InfoObj.SetActive(false);
        turnOnCollider();
    }

    //해당 클릭오브젝트는 (끄기 버튼) 해당 함수를 호출할때 자신의 parent의 sprite name을 매개변수로 넘긴다.
    public void buyObj(string clickedSpr)
    {
        //해당 오브젝트의 sprite 체크를 통해서 인덱스 받아옴
        //string spritename = clickedObj.GetComponent<SpriteRenderer>().sprite.name;
        //int sprindex = int.Parse(spritename.Substring(spritename.Length - 1));

        int sprindex = int.Parse(clickedSpr.Substring(clickedSpr.Length - 1));

        if (want_cat == true && buycat[sprindex] == -1)
        {
            if (money >= catCost[sprindex])
            {
                money -= catCost[sprindex];
                buycat[sprindex] = 0;
                //*******여기에 나중에 구매완료 표시 시켜주기
            }
        }
        else if (want_furniture == true)
        {
            if (money >= furnitureCost[sprindex])
            {
                //구매 진행 및 업그레이드 가격 갱신
                money -= furnitureCost[sprindex];
                furniture[sprindex]++;
            }
        }

        offObjinfo();
    }

    //선택 과정에서 오류 없도록 콜라이더 꺼 주는 작업 수행
    void turnOffCollider()
    {
        backButton.GetComponent<BoxCollider2D>().enabled = false;
        for (i = 0; i < 2; i++)
        {
            Button[i].GetComponent<BoxCollider2D>().enabled = false;
            Arrow[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        for (i = 0; i < 4; i++)
        {
            Goods[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //꺼줬던 콜라이더 켜 주는 작업 수행
    void turnOnCollider()
    {
        backButton.GetComponent<BoxCollider2D>().enabled = true;
        for (i = 0; i < 2; i++)
        {
            Button[i].GetComponent<BoxCollider2D>().enabled = true;
            Arrow[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        for (i = 0; i < 4; i++)
        {
            Goods[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
