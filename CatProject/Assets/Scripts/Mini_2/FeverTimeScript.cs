﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTimeScript : MonoBehaviour
{
    public int touchnum;
    GameObject toy;
    Text FeverText;

    Vector3 DownScale;
    Vector3 UpScale;

    GameObject AudioManager;
    AudioClip fever_toySwing;
    Vector3 volVector;
    float effectvolume;

    void Start()
    {
        toy = GameObject.Find("FeverTime").transform.Find("Item").gameObject;
        FeverText = GameObject.Find("FeverText").GetComponent<Text>();

        DownScale = new Vector3(0.7f, 0.7f, 1);
        UpScale = new Vector3(1, 1, 1);

        AudioManager = GameObject.Find("AudioManager");
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        fever_toySwing = AudioManager.GetComponent<Main_AudioManager>().fever_toySwing;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;
    }

    void OnMouseDown()
    {
        if (effectvolume != 0)
        {
            AudioSource.PlayClipAtPoint(fever_toySwing, volVector);

        }
        toy.transform.eulerAngles += new Vector3(0, 180, 0);
        touchnum++;

        FeverText.transform.localScale = DownScale;
        FeverText.text = touchnum.ToString() + " HIT!";
    }

    private void OnMouseUp()
    {
        FeverText.transform.localScale = UpScale;
    }
}
