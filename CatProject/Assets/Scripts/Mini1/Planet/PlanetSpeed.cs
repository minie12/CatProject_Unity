using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpeed : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        speed = 0.19f;
        StartCoroutine("SpeedUp");
	}

    public IEnumerator SpeedUp()
    {

        yield return new WaitForSeconds(15);

        if (speed > 1.5f)
        {
            speed = 1.6f;
            StopCoroutine("SpeedUp");
        }
        speed += 0.05f;

        StartCoroutine("SpeedUp");
    }
}
