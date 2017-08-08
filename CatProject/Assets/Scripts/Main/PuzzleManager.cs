using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{

    //퍼즐을 얻을 수 있는 각각의 조건들
    int[] ScorePuzzle = new int[8];
    //int[] PlaynumPuzzle = new int[8];

    int i;

    int havingPuzzle;

    int sceneIndex;

    int[] totalplaynum;
    int myplaynum;

    int[] datapuzzle = new int[6];
    int[] mypuzzle = new int[2];
    int[] tempPuzzle = new int[8];

    List<int> puzzleList;

    // Use this for initialization
    void Start()
    {
        ScorePuzzle[0] = 10;
        //PlaynumPuzzle[0] = 0;

        for (i = 1; i < 8; i++)
        {
            ScorePuzzle[i] = ScorePuzzle[i - 1] + 10;
           // PlaynumPuzzle[i] = PlaynumPuzzle[i - 1] + 10; ;
        }
    }

    public void setting_Puzzle(int myscore)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                break;
            case "Mini_1":
                sceneIndex = 0;
                break;
            case "Mini_2":
                sceneIndex = 2;
                break;
            case "Mini_3":
                sceneIndex = 4;
                break;
        }

        datapuzzle = gameObject.GetComponent<ControlGameData>().getPuzzle();
        totalplaynum = gameObject.GetComponent<ControlGameData>().getPlaynum();

        mypuzzle[0] = datapuzzle[sceneIndex]; //플레이 횟수로 인한 퍼즐
        Debug.Log("mypuzzle0 is " + mypuzzle[0]);
        mypuzzle[1] = datapuzzle[sceneIndex + 1]; //스코어를 통한 펴즐
        Debug.Log("mypuzzle1 is " + mypuzzle[1]);
        myplaynum = totalplaynum[sceneIndex / 2];
        Debug.Log("myPlaynum is " + myplaynum);

        getPuzzle_Playnum();
        getPuzzle_Score(myscore);

        for (i = 0; i < 6; i++)
        {
            Debug.Log("i val is " + datapuzzle[i]);
        }


        gameObject.GetComponent<ControlGameData>().setPuzzle(datapuzzle);
        gameObject.GetComponent<ControlGameData>().Save("puzzle");

    }

    void getPuzzle_Playnum()
    {
        System.Array.Clear(tempPuzzle, 0, tempPuzzle.Length);
        puzzleList = new List<int>();
        havingPuzzle = 0;

        for (i = 0; i < 8; i++)
        {
            tempPuzzle[i] = mypuzzle[0] % 2;
            mypuzzle[0] /= 2;

            if (tempPuzzle[i] == 0) //아직 퍼즐이 없는 조각이라면
            {
                puzzleList.Add(i); //리스트에 해당 인덱스 추가 추가
            }
            else //조각이 있는 퍼즐이면 퍼즐 개수 추가
                havingPuzzle++;
        }

        if (havingPuzzle != 8)
        {
            //퍼즐을 얻었다!
            if ((myplaynum / 10) + 1 > havingPuzzle)
            {
                int pIndex = Random.Range(0, puzzleList.Count);
                tempPuzzle[puzzleList[pIndex]] = 1;
                Debug.Log("playnum, get puzzle at " + puzzleList.IndexOf(pIndex));
            }

            int returnval = 0;
            int exp = 1;

            for (i = 0; i < 8; i++)
            {
                returnval = tempPuzzle[i] * exp + returnval;
                exp *= 2;
                Debug.Log(returnval);
            }

            datapuzzle[sceneIndex] = returnval;
        }
        
    }

    void getPuzzle_Score(int myscore)
    {
        System.Array.Clear(tempPuzzle, 0, tempPuzzle.Length);
        puzzleList = new List<int>();
        havingPuzzle = 0;

        for (i = 0; i < 8; i++)
        {
            tempPuzzle[i] = mypuzzle[1] % 2;
            mypuzzle[1] /= 2;

            if (tempPuzzle[i] == 0) //아직 퍼즐이 없는 조각이라면
            {
                puzzleList.Add(i); //리스트에 해당 인덱스 추가 추가
                //Debug.Log(puzzleList.in)
            }
            else //조각이 있는 퍼즐이면 퍼즐 개수 추가
                havingPuzzle++;
        }

        Debug.Log(havingPuzzle);

        //퍼즐을 얻었다!
        if (havingPuzzle != 8)
        {
            if (myscore > ScorePuzzle[havingPuzzle])
            {
                int pIndex = Random.Range(0, puzzleList.Count);
                Debug.Log(pIndex);
                Debug.Log("get puzzle at " + puzzleList.IndexOf(pIndex));
                tempPuzzle[puzzleList[pIndex]] = 1;
            }

            int returnval = 0;
            int exp = 1;

            for (i = 0; i < 8; i++)
            {
                returnval = tempPuzzle[i] * exp + returnval;
                exp *= 2;
                Debug.Log(returnval);
            }

            Debug.Log(returnval);

            datapuzzle[sceneIndex + 1] = returnval;
        }
    }
       
}
