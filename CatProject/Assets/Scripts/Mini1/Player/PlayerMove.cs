using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public int check = 0;

    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    

    void OnMouseDrag()
    {
        //Debug.Log("onmousedown");
        if (check == 0)
        {
            Vector2 mouseDragPosition = new Vector2(550, Input.mousePosition.y);
            Vector2 worldObjectPosition = Camera.main.ScreenToWorldPoint(mouseDragPosition);
           
                Player.transform.position = worldObjectPosition;
            this.transform.position = worldObjectPosition;
            
        }
        else { }
              
    }
}
