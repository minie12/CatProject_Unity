using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//털이 움직이고 + 클릭을 감지하면 상위클래스의 큐에 들어갈 수 있게 함수를 호출함
public class GetClick_fur : MonoBehaviour {
    GameObject parentObj;

    float toxPos;
    float toyPos;

    bool canMoving = false;

    Vector2 myPos;
    Vector2 targetPos;

    // Use this for initialization
    void Start () {
        parentObj = gameObject.GetComponent<Transform>().parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(canMoving == true)
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
        parentObj.GetComponent<FurManager>().GetObj(gameObject);
    }

    public void settingPos()
    {
        canMoving = false;
        toxPos = Random.Range(-8, 8);
        toyPos = Random.Range(-5, 5);
        targetPos = new Vector2(toxPos,toyPos);
        print("mypos is " + myPos + " and targetpos is " + targetPos);
        canMoving = true;
    }
}
