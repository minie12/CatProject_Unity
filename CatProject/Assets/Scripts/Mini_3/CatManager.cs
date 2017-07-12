using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{

    int catsize = 7;

    float assignedTime = 30;
    public float waitTime = 30;
    public float realWaitTime;

    public bool nowWait;

    public int nowCat = 1;
    public List<GameObject> CatList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        GameObject inst_cat = GameObject.Find("Cat");
        CatList.Add(inst_cat);

        for (int i = 0; i < catsize; i++)
        {
            GameObject obj_cat = (GameObject)Instantiate(inst_cat);
            GameObject spr_cat = obj_cat.transform.GetChild(0).gameObject;
            setPos(spr_cat);
            obj_cat.transform.parent = gameObject.transform;

            obj_cat.SetActive(false);
        }

        realWaitTime = waitTime;
        nowWait = false;

        StartCoroutine(appearCat());
    }

    private void Update()
    {
        if (nowWait == true)
            realWaitTime -= Time.deltaTime;
    }


    public IEnumerator appearCat()
    {
        Debug.Log("appearcat called");
        Debug.Log("appearcat waittime is "+waitTime);
        if (nowCat == 8)
            StopCoroutine("appearCat");

        nowWait = true;
        yield return new WaitForSeconds(waitTime);
        nowWait = false;

        if (nowCat != 8)
        {
            GameObject cat = CatList[nowCat - 1];
            cat.SetActive(true);
            //waitTime = 30;
            assignedTime += 30 + (nowCat - 1) * 10;
            waitTime = assignedTime;
            realWaitTime = waitTime;
            nowCat++;
            //Debug.Log(waitTime);
            StartCoroutine(appearCat());
        }

    }

    void setPos(GameObject obj)
    {
        float xpos = Random.Range(-7, 7);
        float ypos = Random.Range(-2.5f, -1);
        obj.transform.position = new Vector3(xpos, ypos, 1);
    }

}
