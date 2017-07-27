using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//초반 인트로를 보여줄 수 있게!
public class ShowingIntro : MonoBehaviour {
    
    public Sprite[][] introSprite = new Sprite[3][];
    string spritedir;

    public bool finishIntro;

	// Use this for initialization
	void Start () {
        int i,j;
        

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

        finishIntro = false;

       // callingIntro(2);
    }

    public void callingIntro(GameObject callobj,int i)
    {
        StartCoroutine(callIntro(callobj, i));
    }

    //스테이지 선택할 때 얘로 코루틴 바로 돌려버리면 됨 
    IEnumerator callIntro(GameObject callobj, int stagenum)
    {
        
        for(int i = 0; i < introSprite[stagenum].Length; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = introSprite[stagenum][i];
            yield return new WaitForSeconds(2.0f);
        }

        //미니게임에서 부른거면 게임 플레이로 돌아가고
        if(callobj.name == "Real_MiniGame")
            callobj.GetComponent<MiniGameScript>().playGame(stagenum);
        //콜렉션에서 부른거면 콜렉션 화면으로 다시 돌아감
    }


}
