using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameScript : CommonJob {

    int[] playnum = new int[3];;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void initial()
    {
        Debug.Log("initial function from minigamescript");
    }
}
