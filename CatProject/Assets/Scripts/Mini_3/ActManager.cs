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

    public void JudgeAct(GameObject callobj)
    {
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
                requestSelected[i] = true;
            else
                requestSelected[i] = false;
        }
    }
}
