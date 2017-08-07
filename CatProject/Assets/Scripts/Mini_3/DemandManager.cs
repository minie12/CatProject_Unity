using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//요구사항 및 고양이의 감정표현 수행하는 코드
public class DemandManager : MonoBehaviour
{
    int feeling = 50;

    float demandTime = 5f;
    public float waitTime = 7f;

    string filedir = "Pic_3/Cat/cat_white_";
    string nowfiledir;

    int feelindex = 0;

    int indexnum = -1;

    int nowFeel = 0;

    bool trysatisfy = false;

    public Sprite[] demandSprite = new Sprite[3];
    public Sprite[] feelSprite = new Sprite[4];
    Sprite[][] catfeeling = new Sprite[4][];


    public GameObject demandObject;
    public GameObject catsprObject;
    public GameObject feelObject;

    GameObject AudioManager;
    AudioClip feelingGood;
    AudioClip feelingBad;
    Vector3 effectVector;
    float effectvolume;

    GameObject actManager;

    // Use this for initialization
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        feelingGood = AudioManager.GetComponent<Main_AudioManager>().cat_feelingGood;
        feelingBad = AudioManager.GetComponent<Main_AudioManager>().cat_feelingBad;
        effectVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;


        demandSprite[0] = Resources.Load<Sprite>("Pic_3/want_brush");
        demandSprite[1] = Resources.Load<Sprite>("Pic_3/want_can");
        demandSprite[2] = Resources.Load<Sprite>("Pic_3/want_toy");

        feelSprite[0] = Resources.Load<Sprite>("Pic_3/Bad");
        feelSprite[1] = Resources.Load<Sprite>("Pic_3/SoSo");
        feelSprite[2] = Resources.Load<Sprite>("Pic_3/Good");
        feelSprite[3] = Resources.Load<Sprite>("Pic_3/Happy");

        for (int i = 0; i < 4; i++)
        {
            catfeeling[i] = new Sprite[2];

            switch (i)
            {
                case 0:
                    nowfiledir = filedir + "angry";
                    break;
                case 1:
                    nowfiledir = filedir + "normal";
                    break;
                case 2:
                    nowfiledir = filedir + "good";
                    break;
                case 3:
                    nowfiledir = filedir + "happy";
                    break;
            }

            catfeeling[i][0] = Resources.Load<Sprite>(nowfiledir + "1");
            catfeeling[i][1] = Resources.Load<Sprite>(nowfiledir + "2");

        }

        demandObject = gameObject.transform.Find("CatSprite").gameObject.transform.Find("Cat_demand").gameObject;
        catsprObject = gameObject.transform.Find("CatSprite").gameObject;
        feelObject = gameObject.transform.Find("CatSprite").gameObject.transform.Find("Cat_feel").gameObject;

        actManager = GameObject.Find("Acts");

        demandObject.GetComponent<SpriteRenderer>().sprite = null;
        setfeeling();
        StartCoroutine("shakingTail");

        StartCoroutine(appearDemand());
    }

    public void isRightAct()
    {
        //요구사항이 존재할 때
        if (indexnum != -1)
        {
            //요구사항 제대로 충족
            if (actManager.GetComponent<ActManager>().requestSelected[indexnum] == true)
            {
                feeling += 10;
                if (effectvolume != 0)
                    AudioSource.PlayClipAtPoint(feelingGood, effectVector);
            }
            else // 잘못된 요구 충족
            {
                feeling -= 10;
                if (effectvolume != 0)
                    AudioSource.PlayClipAtPoint(feelingBad, effectVector);
            }

            trysatisfy = true;
            indexnum = -1;
            demandObject.GetComponent<SpriteRenderer>().sprite = null;

        }
        else
        {
            //요구사항이 없을 때
            feeling -= 5;
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(feelingBad, effectVector);
        }

        if (feeling < 0)
            feeling = 0;
        else if (feeling > 100)
            feeling = 100;

        setfeeling();

    }

    void setfeeling()
    {
        if (feeling < 20 && feeling >= 0)
        {
            nowFeel = 0;
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[0];
            gameObject.GetComponent<FurManager>().waitTime = 0.5f;
        }
        else if (feeling >= 20 && feeling < 65)
        {
            nowFeel = 1;
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[1];
            gameObject.GetComponent<FurManager>().waitTime = 1f;
        }
        else if (feeling >= 65 && feeling < 90)
        {
            nowFeel = 2;
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[2];
            gameObject.GetComponent<FurManager>().waitTime = 1.5f;
        }
        else if (feeling >= 90 && feeling <= 100)
        {
            nowFeel = 3;
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[3];
            gameObject.GetComponent<FurManager>().waitTime = 3f;
        }
    }

    public IEnumerator shakingTail()
    {
        yield return new WaitForSeconds(0.45f);
        feelindex = (feelindex + 1) % 2;
        catsprObject.GetComponent<SpriteRenderer>().sprite = catfeeling[nowFeel][feelindex];
        StartCoroutine("shakingTail");

    }

    public IEnumerator appearDemand()
    {
        trysatisfy = false;
        yield return new WaitForSeconds(waitTime);
        indexnum = (int)Random.Range(0, 3);
        demandObject.GetComponent<SpriteRenderer>().sprite = demandSprite[indexnum];
        yield return new WaitForSeconds(demandTime);
        indexnum = -1;
        if (trysatisfy != true) // 아예 클릭조차 못 했을 때
        {
            feeling -= 10;
            setfeeling();
        }
        demandObject.GetComponent<SpriteRenderer>().sprite = null;
        StartCoroutine(appearDemand());
    }
}
