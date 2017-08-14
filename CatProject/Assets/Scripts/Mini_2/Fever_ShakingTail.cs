using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever_ShakingTail : MonoBehaviour {

    Sprite[] catfeeling = new Sprite[2];
    string filedir;

    int feelindex;

    string Objfeeling;

    // Use this for initialization
    void Start()
    {
        filedir = "Pic_3/Cat/";
        feelindex = 0;

        Objfeeling = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        Objfeeling = Objfeeling.Substring(0, Objfeeling.Length - 1);

        catfeeling[0] = Resources.Load<Sprite>(filedir + Objfeeling + "1");
        catfeeling[1] = Resources.Load<Sprite>(filedir + Objfeeling + "2");

    }

    public void fever_tail()
    {
        StartCoroutine("fever_shakingTail");
    }

    IEnumerator fever_shakingTail()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.45f);
            feelindex = (feelindex + 1) % 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = catfeeling[feelindex];
            //StartCoroutine("shakingTail");
        }
    }
}
