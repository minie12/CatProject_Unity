using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandManager : MonoBehaviour {
    int feeling = 50;

    float demandTime = 5f;
    float waitTime = 7f;

    int indexnum = -1;

    bool trysatisfy = false;

    public Sprite[] demandSprite = new Sprite[3];
    public Sprite[] feelSprite = new Sprite[4];

    public GameObject demandObject;
    public GameObject feelObject;
    
    GameObject actManager;

    // Use this for initialization
    void Start () {
        demandSprite[0] = Resources.Load<Sprite>("Pic_3/Cat_touch");
        demandSprite[1] = Resources.Load<Sprite>("Pic_3/Cat_eating");
        demandSprite[2] = Resources.Load<Sprite>("Pic_3/Cat_playing");

        feelSprite[0] = Resources.Load<Sprite>("Pic_3/Bad");
        feelSprite[1] = Resources.Load<Sprite>("Pic_3/SoSo");
        feelSprite[2] = Resources.Load<Sprite>("Pic_3/Good");
        feelSprite[3] = Resources.Load<Sprite>("Pic_3/Happy");

        demandObject = gameObject.transform.Find("CatSprite").gameObject.transform.Find("Cat_demand").gameObject;
        feelObject = gameObject.transform.Find("CatSprite").gameObject.transform.Find("Cat_feel").gameObject;

        actManager = GameObject.Find("Acts");

        demandObject.GetComponent<SpriteRenderer>().sprite = null;
        setfeeling();


        StartCoroutine(appearDemand());
    }

    public void isRightAct()
    {
        //요구사항이 존재할 때
        if (indexnum != -1)
        {
            //요구사항 제대로 충족
            if (actManager.GetComponent<ActManager>().requestSelected[indexnum] == true)
                feeling += 10;
            else // 잘못된 요구 충족
                feeling -= 10;

            trysatisfy = true;
            indexnum = -1;
            demandObject.GetComponent<SpriteRenderer>().sprite = null;
            
        }
        else //요구사항이 없을 때
            feeling -= 5;

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
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[0];
            gameObject.GetComponent<FurManager>().waitTime = 0.5f;
        }
        else if (feeling >= 20 && feeling < 65)
        {
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[1];
            gameObject.GetComponent<FurManager>().waitTime = 1f;
        }
        else if (feeling >= 65 && feeling < 90)
        {
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[2];
            gameObject.GetComponent<FurManager>().waitTime = 1.5f;
        }
        else if (feeling >= 90 && feeling <= 100)
        {
            feelObject.GetComponent<SpriteRenderer>().sprite = feelSprite[3];
            gameObject.GetComponent<FurManager>().waitTime = 3f;
        }
    }

    public IEnumerator appearDemand()
    {
        Debug.Log("appeardemand called");
        trysatisfy = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("appeardemand called-waitTime over");
        indexnum = (int)Random.Range(0, 3);
        Debug.Log(indexnum);
        demandObject.GetComponent<SpriteRenderer>().sprite = demandSprite[indexnum];
        yield return new WaitForSeconds(demandTime);
        indexnum = -1;
        if(trysatisfy!=true) // 아예 클릭조차 못 했을 때
        {
            feeling -= 10;
            setfeeling();
        }
        demandObject.GetComponent<SpriteRenderer>().sprite = null;
        StartCoroutine(appearDemand());
    }
}
