using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTime : MonoBehaviour
{
    GameObject AudioManager;
    AudioClip PresentClicking;
    Vector3 volVector;
    float effectvolume;

    // Use this for initialization
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        PresentClicking = AudioManager.GetComponent<Main_AudioManager>().PresentClicking;
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        this.GetComponent<Transform>().position = new Vector3(-11.57f, 0.38f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float objPosition_x = gameObject.GetComponent<Transform>().position.x;

        if (objPosition_x < 1.55f)
        {
            gameObject.GetComponent<Transform>().position += new Vector3(0.05f, 0, 0);
        }

        else
        {
            gameObject.GetComponent<Transform>().position += new Vector3(0.008f, -0.08f, 0);
        }
    }

    void OnMouseDown()
    {
        if (gameObject.GetComponent<Transform>().position.x < 1.55f)
        {
            GameObject.Find("Manager").GetComponent<UIManager>().PresentPlus();
            //PresentClickingAudio@@@@
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(PresentClicking, volVector);
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Fail") //when object falls
        {
            
            GameObject.Find("Main Camera").GetComponent<TotalManager_3>().MainGameOn();
            gameObject.SetActive(false);            
                      
        }
    }
}
