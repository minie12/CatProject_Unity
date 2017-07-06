using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurManager : MonoBehaviour
{

    int fursize = 14;
    public float waitTime = 5f;
    

    public Queue<GameObject> fur_q;

    GameObject GameManager;

    // Use this for initialization
    void Start()
    {
        GameObject inst_fur = gameObject.transform.Find("Cat_furs").gameObject;
        GameManager = GameObject.Find("GameManager");
        fur_q = new Queue<GameObject>();

        fur_q.Enqueue(inst_fur);
        inst_fur.SetActive(false);

        for (int i = 0; i < fursize; i++)
        {
            GameObject obj_fur = (GameObject)Instantiate(inst_fur);
            setPos(obj_fur);
            obj_fur.transform.parent = gameObject.transform;
            fur_q.Enqueue(obj_fur);
            obj_fur.SetActive(false);
        }

        StartCoroutine(appearFur());
        Debug.Log(fur_q.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator appearFur()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (fur_q.Count != 0)
            {
                GameObject fur = fur_q.Dequeue();
                fur.SetActive(true);
                GameManager.GetComponent<TotalManager>().TotalFurNum++;
                GameManager.GetComponent<TotalManager>().appearFurText();

                if (GameManager.GetComponent<TotalManager>().TotalFurNum >= 12 && GameManager.GetComponent<TotalManager>().startcor == false)
                {
                    Debug.Log("called!");
                    GameManager.GetComponent<TotalManager>().startcor = true;
                }

            }
        }
    }

    //랜덤으로 포지션을 정해주는 함수
    void setPos(GameObject obj)
    {
        float xpos = Random.Range(-7, 7);
        float ypos = Random.Range(-4, 4);
        obj.transform.position = new Vector3(xpos, ypos, 0);
    }

    //클릭이 감지된 함수의 위치를 재설정해주고 큐에 넣은 후 disable시켜줌.
    public void GetObj(GameObject obj)
    {
        setPos(obj);//위치 재설정해주고
        fur_q.Enqueue(obj);//큐에 넣어주고
        obj.SetActive(false);//disable
        GameManager.GetComponent<TotalManager>().TotalFurNum--;
        GameManager.GetComponent<TotalManager>().appearFurText();
    }

}
