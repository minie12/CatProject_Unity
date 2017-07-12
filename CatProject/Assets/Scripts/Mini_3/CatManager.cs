using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{

    int catsize = 7;
    float waitTime = 30;

    public int nowCat = 1;
    public List<GameObject> CatList;

    // Use this for initialization
    void Start()
    {
        GameObject inst_cat = GameObject.Find("Cat");
        CatList = new List<GameObject>();

        for (int i = 0; i < catsize; i++)
        {
            GameObject obj_cat = (GameObject)Instantiate(inst_cat);
            GameObject spr_cat = obj_cat.transform.GetChild(0).gameObject;
            setPos(spr_cat);
            obj_cat.transform.parent = gameObject.transform;
            CatList.Add(obj_cat);

            obj_cat.SetActive(false);
        }

        StartCoroutine(appearCat());
    }


    public IEnumerator appearCat()
    {
        Debug.Log("appearcat called");
        Debug.Log(CatList.Count);
        if (nowCat == 8)
            StopCoroutine("appearCat");

        yield return new WaitForSeconds(waitTime);
        if (nowCat != 8)
        {
            GameObject cat = CatList[nowCat - 1];
            cat.SetActive(true);
            waitTime += 30 + (nowCat - 1) * 10;
            nowCat++;
            Debug.Log(waitTime);
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
