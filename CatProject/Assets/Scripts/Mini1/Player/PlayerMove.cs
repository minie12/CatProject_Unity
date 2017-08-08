using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    void OnMouseDrag()
    {
         Vector2 mouseDragPosition = new Vector2(450, Input.mousePosition.y);
         Vector2 worldObjectPosition = Camera.main.ScreenToWorldPoint(mouseDragPosition);
         this.transform.position = worldObjectPosition;
              
    }
}
