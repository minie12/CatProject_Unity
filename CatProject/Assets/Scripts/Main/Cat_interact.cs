using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_interact : MonoBehaviour {

    //랜덤하게 고양이들 울게 함
    AudioClip[] catmeow = new AudioClip[3];
    GameObject AudioManager;
    Vector3 volVector;
    float effectvolume;

    Sprite[][][] catInteractSpr = new Sprite[8][][];//고양이마다

    // Use this for initialization
    void Start () {
       for(int i = 0; i < catInteractSpr.Length; i++)
        {
            catInteractSpr[i] = new Sprite[4][];//각각 위치 네개씩
            for(int j = 0; j < catInteractSpr[i].Length; j++)
            {
                catInteractSpr[i][j] = new Sprite[2];//변화 두장
                if(j==0 || j==1)
                {
                    catInteractSpr[i][j][0] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_" + (j + 7));
                    catInteractSpr[i][j][1] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_" + (j + 7)+"_touch");
                   

                }
                else
                {
                    catInteractSpr[i][j][0] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_"+(j + 9));
                    catInteractSpr[i][j][1] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_" + (j + 9) + "_touch");
                }
                //Debug.Log(catInteractSpr[i][j][0].name);
                //Debug.Log(catInteractSpr[i][j][1].name);
            }
        }

        AudioManager = GameObject.Find("AudioManager");
        catmeow = AudioManager.GetComponent<Main_AudioManager>().Cat_Meow;

        CatVolSetting();

    }
    
    public void CatVolSetting()
    {
        AudioManager.GetComponent<Main_AudioManager>().changeVolumn();
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        Debug.Log("in setting, volvector is " + volVector + "and effectvolume is " + effectvolume);
    }

    public void getreaction_Cat(GameObject cat, int catnum, int posnum)
    {
        Debug.Log(catnum + "and posnum is " + posnum);
        switch (posnum)
        {
            case 7:
            case 8:
            case 11:
            case 12:
                Debug.Log(effectvolume);
                Debug.Log(volVector);
                StartCoroutine(sprchange(cat, catnum, posnum));
                break;
            default:
                Debug.Log("hi");
                int i = Random.Range(0, catmeow.Length);
                Debug.Log(catmeow[i].name);
                Debug.Log(effectvolume);
                Debug.Log(volVector);
                if (effectvolume!=0)
                    AudioSource.PlayClipAtPoint(catmeow[i], volVector);
                break;

        }
    }

    IEnumerator sprchange(GameObject cat, int catnum, int posnum)
    {
        Debug.Log("hi??");
        int i = Random.Range(0, catmeow.Length);
        if (effectvolume != 0)
            AudioSource.PlayClipAtPoint(catmeow[i], volVector);
        if(posnum == 7||posnum == 8)
        {
            Debug.Log("catnum is " + catnum + " and posnum is " + posnum);
            cat.GetComponent<SpriteRenderer>().sprite = catInteractSpr[catnum][posnum-7][1];
            yield return new WaitForSeconds(0.5f);
            cat.GetComponent<SpriteRenderer>().sprite = catInteractSpr[catnum][posnum-7][0];
        }
        else
        {
            Debug.Log("catnum is " + catnum + " and posnum is " + posnum);
            cat.GetComponent<SpriteRenderer>().sprite = catInteractSpr[catnum][2][1];
            yield return new WaitForSeconds(0.5f);
            cat.GetComponent<SpriteRenderer>().sprite = catInteractSpr[catnum][2][0];
        }
        
    }
}
