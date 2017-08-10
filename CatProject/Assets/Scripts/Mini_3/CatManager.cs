using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{

    int catsize = 7;

    float assignedTime = 30;
    public float waitTime = 15;
    public float realWaitTime;

    public bool nowWait;

    public int nowCat = 1;
    public List<GameObject> CatList = new List<GameObject>();

    List<Vector3> CatPos = new List<Vector3>();

    // Use this for initialization
    void Start()
    {

        assignedTime = 30;
        waitTime = 10;

        GameObject inst_cat = GameObject.Find("Cat");
        CatList.Add(inst_cat);

        CatPos.Add(new Vector3(-0.8f, 1.5f, 0));
        CatPos.Add(new Vector3(3.2f, 2.1f, 0));
        CatPos.Add(new Vector3(7f, 2.3f, 0));
        CatPos.Add(new Vector3(3.8f, -1.6f, 0));
        CatPos.Add(new Vector3(7.5f, -0.8f, 0));
        CatPos.Add(new Vector3(14.5f, 0.85f, 0));
        CatPos.Add(new Vector3(11f, 0.45f, 0));

        for (int i = 0; i < catsize; i++)
        {
            GameObject obj_cat = (GameObject)Instantiate(inst_cat);
            //GameObject spr_cat = obj_cat.transform.GetChild(0).gameObject;
            setPos(obj_cat);
            obj_cat.transform.parent = gameObject.transform;
            CatList.Add(obj_cat);
            obj_cat.SetActive(false);
        }

        realWaitTime = waitTime;
        nowWait = false;

        StartCoroutine("appearCat");
    }

    private void Update()
    {
        if (nowWait == true)
            realWaitTime -= Time.deltaTime;
    }


    public IEnumerator appearCat()
    {
        Debug.Log("appearcat started"+waitTime);
        if (nowCat == 8)
            StopCoroutine("appearCat");

        nowWait = true;
        yield return new WaitForSeconds(waitTime);
        nowWait = false;
        Debug.Log("nowcat is "+nowCat);
        if (nowCat != 8)
        {
            Debug.Log("nowcat is ");
            GameObject cat = CatList[nowCat];
            cat.SetActive(true);
            //waitTime = 30;
            //assignedTime += 30 + (nowCat - 1) * 10;
            nowCat++;
            assignedTime = 10 + (nowCat - 1) * 15;
            waitTime = assignedTime;
            realWaitTime = waitTime;
            //Debug.Log(waitTime);
            StartCoroutine("appearCat");
        }

    }

    void setPos(GameObject obj)
    {
        int index = Random.Range(0, CatPos.Count);
        obj.transform.position = CatPos[index];
        
        Debug.Log(CatPos[index]);
        CatPos.RemoveAt(index);
    }

}
