﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    GameObject AudioManager;
    AudioClip PresentClicking;
    AudioClip PutBoxDown;
    AudioClip CatCrying;
    Vector3 volVector;
    float effectvolume;

    float speed;
    float timer;
    Sprite boxTo;

    //sprites
    Sprite boxSpr;
    Sprite catSpr;
    Sprite[] toySpr = new Sprite[3];
    Sprite[] fakeSpr = new Sprite[3];
    Sprite presentSpr;
    Sprite mySprite; // used to determine which sprite the object is

    //related to up&down drag
    bool ableDrag = false; //used to determine whether the object is in the range where items can be dragged
    Vector3 mousePosOn;
    GameObject dust;
    Sprite dustSpr;

    //for cat walking on the shelf
    int walkindex = 0;
    Sprite[] catWalking = new Sprite[2];

    //moving to position
    Vector3 startPos;
    Vector3 endPos;
    bool isUp;
    bool isDown;

    // gameObject.transform.Find("Cat").gameObject;

    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        PresentClicking = AudioManager.GetComponent<Main_AudioManager>().PresentClicking;
        PutBoxDown = AudioManager.GetComponent<Main_AudioManager>().PutBoxDown;
        CatCrying = AudioManager.GetComponent<Main_AudioManager>().CatCrying;
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        //finding sprite resources
        for (int i = 0; i < 2; i++)
        {
            toySpr[i] = Resources.Load<Sprite>("Sprites/Item/Object_toy_" + (i + 1));
            fakeSpr[i] = Resources.Load<Sprite>("Sprites/Item/Object_fake_" + (i + 1));
            catWalking[i] = Resources.Load<Sprite>("Sprites/Cat_white_" + (i + 1));
        }
        toySpr[2] = Resources.Load<Sprite>("Sprites/Item/Object_toy_" + 3);
        fakeSpr[2] = Resources.Load<Sprite>("Sprites/Item/Object_fake_" + 3);
        catSpr = Resources.Load<Sprite>("Sprites/Item/Object_cat_white");
        presentSpr = Resources.Load<Sprite>("Sprites/fever_score");
        boxSpr = Resources.Load<Sprite>("Sprites/Item/Object_box");
        dustSpr = Resources.Load<Sprite>("Sprites/Dust");

        dust = GameObject.Find("DustUp");

        isUp = false;
        isDown = false;
    }

    void Update()
    {
        mySprite = this.GetComponent<SpriteRenderer>().sprite;

        timer = GameObject.Find("Manager").GetComponent<UIManager>().timer;
        speed = GameObject.Find("Manager").GetComponent<UIManager>().speed;

        float objPosition_x = gameObject.GetComponent<Transform>().position.x;

        startPos = this.transform.position;

        if (isUp == true)
        {
            endPos = new Vector3(5.1f, 3.9f, 0);
            gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, (18 + speed) * Time.deltaTime);

            if (objPosition_x > 5)
            {
                dust.transform.position = endPos;
                StartCoroutine("DustEffect");
                isUp = false;
            }
        }

        else if (isDown == true)
        {
            endPos = new Vector3(5.1f, -1.45f, 0);
            gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, (18 + speed) * Time.deltaTime);

            if (objPosition_x > 5)
            {
                dust.transform.position = endPos;
                StartCoroutine("DustEffect");
                isDown = false;
            }
        }

        else
        {
            if (objPosition_x < 1.25f || objPosition_x >= 3.6f)
            {
                gameObject.GetComponent<Transform>().position += new Vector3(speed, 0, 0);
            }
            else
            {
                gameObject.GetComponent<Transform>().position += new Vector3(0.01f, -(speed + 0.02f), 0);
            }
        }
    }

    IEnumerator DustEffect()
    {
        dust.GetComponent<SpriteRenderer>().sprite = dustSpr;
        //PutBoxDown@@@@
        if (effectvolume != 0)
            AudioSource.PlayClipAtPoint(PutBoxDown, volVector);
        yield return new WaitForSeconds(0.1f);
        dust.GetComponent<SpriteRenderer>().sprite = null;
    }

    void ChooseSprite()
    {
        //choosing which object is in the box
        int i = Random.Range(0, 100);
        Sprite setSprite = catSpr;

        if (0 <= i && i < 48) //Cat
        {
            setSprite = catSpr;
        }
        else if (48 <= i && i < 96) //Toy
        {
            int toy_index = i % 3;
            setSprite = toySpr[toy_index];
        }
        else //Present
        {
            setSprite = presentSpr;
        }

        this.GetComponent<SpriteRenderer>().sprite = setSprite;
    }

    //for the cat to walk (changing sprite constantly)
    IEnumerator WalkingCat()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.45f);
            walkindex = (walkindex + 1) % 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = catWalking[walkindex];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Spawning") //spawning new box
        {
            if (mySprite == boxSpr)
            {
                GameObject.Find("Warehouse").GetComponent<SpawnBox>().CallBox();
            }
        }

        if (other.gameObject.name == "BoxTo") //turning box to item
        {
            if (mySprite == boxSpr)
            {
                ChooseSprite();
            }
        }

        if (other.gameObject.name == "ToyTo") //turning box to cat
        {
            if (timer > 20)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == toySpr[i])
                    {
                        int fake_num = Random.Range(0, 3);//0.33 chance of toy changing to fake

                        if (fake_num == 0) //sprite == fake
                        {
                            this.GetComponent<SpriteRenderer>().sprite = fakeSpr[i];
                            //CatCrying@@@@
                            if (effectvolume != 0)
                                AudioSource.PlayClipAtPoint(CatCrying, volVector);
                        }
                    }
                }
            }
        }

        if (other.gameObject.name == "Fail") //when object falls
        {
            if (mySprite == catSpr)
            {
                GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
            }

            for (int i = 0; i < 3; i++)
            {
                if (mySprite == fakeSpr[i])
                {
                    GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
                    break;
                }

                if (mySprite == toySpr[i])
                {
                    GameObject.Find("Main Camera").GetComponent<TotalManager_3>().ToyEnd();
                    break;
                }
            }
        }

        if (other.gameObject.name == "CatWalk") //cat walking by itself
        {
            if (mySprite == catSpr)
            {
                StartCoroutine("WalkingCat");
            }

            for (int i = 0; i < 3; i++)
            {
                if (mySprite == fakeSpr[i])
                {
                    StartCoroutine("WalkingCat");
                    break;
                }
            }
        }

        if (other.gameObject.name == "Upward") //when object reach the end of shelf
        {
            for (int i = 0; i < 3; i++)
            {
                if (mySprite == toySpr[i])
                {
                    GameObject.Find("Main Camera").GetComponent<TotalManager_3>().ToyEnd();
                    break;
                }

                if (mySprite == catWalking[i % 2])
                {
                    this.transform.gameObject.SetActive(false);
                    StopCoroutine("WalkingCat");
                    GameObject.Find("Warehouse").GetComponent<SpawnBox>().OrganizeBox(gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.name == "Downward") //when object reach the end of conveyor belt
        {
            if (mySprite == catSpr)
            {
                GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
            }
            for (int i = 0; i < 3; i++)
            {
                if (mySprite == toySpr[i])
                {
                    this.transform.gameObject.SetActive(false);
                    GameObject.Find("Warehouse").GetComponent<SpawnBox>().OrganizeBox(gameObject);
                    break;
                }

                if (mySprite == fakeSpr[i])
                {
                    GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
                    break;
                }
            }
        }
    }

    //when the mouse is clicked 
    void OnMouseDown()
    {
        if (mySprite == presentSpr)
        {
            gameObject.SetActive(false);
            GameObject.Find("Warehouse").GetComponent<SpawnBox>().OrganizeBox(gameObject);
            GameObject.Find("Manager").GetComponent<UIManager>().PresentPlus();
            //PresentClicking@@@@
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(PresentClicking, volVector);
        }

        else
        {
            //range that object can be moved
            if (gameObject.GetComponent<Transform>().position.x > -3.55f && gameObject.GetComponent<Transform>().position.x < 0.8f)
            {
                ableDrag = true;
                mousePosOn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);   //get position of the mouse
            }
        }
    }

    //moving box to upper or down coneyor belt according to the direction of the swipe
    void OnMouseUp()
    {
        Vector3 mousePosOff = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);   //get position of the mouse

        Vector3 direction = mousePosOff - mousePosOn;

        //range that object can be moved
        if (ableDrag == true)
        {
            if (direction.y > 0)    //swipe upward
            {
                isUp = true;
            }

            else //swipe downward
            {
                isDown = true;
            }

            ableDrag = false;
        }
    }

    public void SetBool()
    {
        isUp = false;
        isDown = false;
    }
}
