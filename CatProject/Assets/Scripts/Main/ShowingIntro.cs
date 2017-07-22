using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//초반 인트로를 보여줄 수 있게!
public class ShowingIntro : MonoBehaviour {

    GameObject storyBackground;
    Sprite[][] introSprite = new Sprite[3][];
    string spritedir;

	// Use this for initialization
	void Start () {
        int i,j;

        storyBackground = GameObject.Find("Story_Background");
        storyBackground.SetActive(false);

        introSprite[0] = new Sprite[7];
        introSprite[1] = new Sprite[7];
        introSprite[2] = new Sprite[6];

        for (i = 0; i < 3; i++)
        {
            for (j = 0; j < introSprite[i].Length ; j++)
            {
                spritedir = "Story_" + (i + 1) + "/STORY_" + (j + 1);
                introSprite[i][j] = Resources.Load<Sprite>(spritedir);
            }
        }

        callingIntro(0);
    }

    void callingIntro(int i)
    {
        StartCoroutine(callIntro(i));
    }

    //스테이지 선택할 때 얘로 코루틴 바로 돌려버리면 됨 
    IEnumerator callIntro(int stagenum)
    {
        storyBackground.SetActive(true);
        for(int i = 0; i < introSprite[stagenum].Length; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = introSprite[stagenum][i];
            yield return new WaitForSeconds(2.0f);
        }
        storyBackground.SetActive(false);
    }
}
