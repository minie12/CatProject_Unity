using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTime : MonoBehaviour
{
    //for audio effect
    GameObject AudioManager;
    AudioClip PresentClicking;
    Vector3 volVector;
    float effectvolume;

    public int presentAdd;
    public bool fevercall;

    // Use this for initialization
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        PresentClicking = AudioManager.GetComponent<Main_AudioManager>().PresentClicking;
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        fevercall = false;
    }

    // Update is called once per frame
    void Update()
    {
        float objPosition_x = gameObject.GetComponent<Transform>().position.x;

        if (objPosition_x < 1.6f)
        {
            gameObject.GetComponent<Transform>().position += new Vector3(0.05f, 0, 0);
        }
    }

    //setting where the present will come out when fever time starts
    public void SettingPos()
    {
        gameObject.transform.localPosition = new Vector3(-11.57f, 0.02f, 1);
    }

    // showing 00HIT when present is pressed 
    void OnMouseDown()
    {
        if (gameObject.GetComponent<Transform>().position.x < 1.55f)
        {
            presentAdd++;
            GameObject.Find("Manager").GetComponent<UIManager>().TextPlus();
            //PresentClickingAudio@@@@
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(PresentClicking, volVector);
        }        
    }

    //used to expand the time in fever
    IEnumerator TimeExpand()
    {
        int feverPlayTime = GameObject.Find("Main Camera").GetComponent<TotalManager_3>().feverPlayTime;
        yield return new WaitForSeconds(feverPlayTime);
        FeverBonus();
        GameObject.Find("Main Camera").GetComponent<TotalManager_3>().MainGameOn();
        GameObject.Find("Manager").GetComponent<UIManager>().TextReset();
        gameObject.SetActive(false);
    }

    //adding bonus to normal score after fever ends
    void FeverBonus()
    {
        int bonusAfterFever = GameObject.Find("Main Camera").GetComponent<TotalManager_3>().bonusAfterFever;
        GameObject.Find("Manager").GetComponent<UIManager>().seconds += bonusAfterFever;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Falling" && fevercall == false)
        {
            fevercall = true;
            Debug.Log("stayfor3sec");
            StartCoroutine("TimeExpand");            
        }
    }   
}
