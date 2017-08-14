using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//털이 움직이고 + 클릭을 감지하면 상위클래스의 큐에 들어갈 수 있게 함수를 호출함
public class GetClick_fur : MonoBehaviour
{
    GameObject parentObj;
    GameObject AudioManager;

    AudioClip furdisappear;

    Vector3 volVector;

    float effectvolume;

    float toxPos;
    float toyPos;

    bool canMoving = false;

    Vector2 myPos;
    Vector2 targetPos;

    // Use this for initialization
    void Start()
    {
        parentObj = gameObject.GetComponent<Transform>().parent.gameObject;
        AudioManager = GameObject.Find("AudioManager");

        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;

        furdisappear = AudioManager.GetComponent<Main_AudioManager>().furdisappear;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoving == true)
        {
            myPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            gameObject.transform.position = Vector2.MoveTowards(myPos, targetPos, 1 * Time.deltaTime);
            if (Vector2.Distance(myPos, targetPos) < 0.01f)
            {
                settingPos();
            }
        }
    }

    private void OnMouseDown()
    {
        if (effectvolume != 0)
        {
            AudioSource.PlayClipAtPoint(furdisappear, volVector);

        }
        parentObj.GetComponent<FurManager>().GetObj(gameObject);


    }

    public void settingPos()
    {
        canMoving = false;
        toxPos = Random.Range(-8, 8);
        toyPos = Random.Range(-5, 5);
        targetPos = new Vector2(toxPos, toyPos);
        canMoving = true;
    }

    /*IEnumerator disappearfur()
    {

        AudioSource.PlayClipAtPoint(furdisappear, volVector);
        yei
        

    }*/
}
