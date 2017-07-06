using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour {

    public bool[] requestSelected = new bool[3];
    GameObject[] requestObj = new GameObject[3];

    // Use this for initialization
    void Start () {
        requestObj[0] = gameObject.transform.Find("Touch").gameObject;
        requestObj[1] = gameObject.transform.Find("Eat").gameObject;
        requestObj[2] = gameObject.transform.Find("Play").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void JudgeAct(GameObject callobj)
    {
        Debug.Log(callobj.name);
        if (callobj.transform.name == "Touch")
            setting(0);
        
        else if (callobj.transform.name == "Eat")
            setting(1);

        else if (callobj.transform.name == "Play")
            setting(2);
    }

    void setting(int num)
    {
        for(int i = 0; i < 3; i++)
        {
            if (i == num)
            {
                requestObj[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                requestSelected[i] = true;
            }
            else
            {
                requestObj[i].GetComponent<SpriteRenderer>().color = Color.white;
                requestSelected[i] = false;
            }
        }
    }
}
