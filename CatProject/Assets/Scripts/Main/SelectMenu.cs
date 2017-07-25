using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{

    GameObject mainManager;

    GameObject[] furnitureObj = new GameObject[4];
    Sprite[][] furnitureSpr = new Sprite[4][];
    bool[] selectedBool = new bool[4];

    int selectedIndex;
    int beforeselected;

    string sprdir = "Main/";
    string tempdir;

    // Use this for initialization
    void Start()
    {
        mainManager = GameObject.Find("MainManager");

        selectedIndex = -1;
        beforeselected = -1;

        for (int i = 0; i < 4; i++)
        {
            furnitureSpr[i] = new Sprite[2];

            switch (i)
            {
                case 0:
                    furnitureObj[0] = GameObject.Find("Shop_Sprite");
                    tempdir = sprdir + "door";
                    break;
                case 1:
                    tempdir = sprdir + "food";
                    furnitureObj[1] = GameObject.Find("Collection_Sprite");
                    break;
                case 2:
                    tempdir = sprdir + "toy";
                    furnitureObj[2] = GameObject.Find("MiniGame_Sprite");
                    break;
                case 3:
                    furnitureObj[3] = GameObject.Find("Setting_Sprite");
                    tempdir = sprdir + "window";
                    break;
            }

            furnitureSpr[i][0] = Resources.Load<Sprite>(tempdir);
            furnitureSpr[i][1] = Resources.Load<Sprite>(tempdir + "_selected");
        }
    }


    public void judgeSelect(GameObject selectedObj)
    {
        switch (selectedObj.name)
        {
            case "Background":
                selectedIndex = -1;
                break;
            case "Shop_Sprite":
                selectedIndex = 0;
                break;
            case "Setting_Sprite":
                selectedIndex = 3;
                break;
            case "MiniGame_Sprite":
                selectedIndex = 2;
                break;
            case "Collection_Sprite":
                selectedIndex = 1;
                break;
        }


        //선택한 걸 다시 한 번 선택한 경우
        if (beforeselected == selectedIndex && beforeselected != -1)
        {
            //Debug.Log("selected again!" + beforeselected + "beforeselected is ");
            mainManager.GetComponent<Main_Manager>().doyourJob(selectedIndex);

            selectedIndex = -1;
            beforeselected = -1;
        }
        else // 아니라면
        {
            //이전 선택을 업그레이드시켜줌
            beforeselected = selectedIndex;
            for (int i = 0; i < 4; i++)
            {
                if (i == beforeselected) // 선택한 것의 경우
                {
                    selectedBool[i] = true;
                    furnitureObj[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i][1];
                }
                else
                {
                    selectedBool[i] = false;
                    furnitureObj[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i][0];
                }
            }
        }
    }
}
