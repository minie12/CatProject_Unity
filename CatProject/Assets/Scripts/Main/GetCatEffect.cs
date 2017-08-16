using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCatEffect : MonoBehaviour {

    int[] buycat;

    int[] effect = new int[7];
    float[] realeffect = new float[7];

    public float[] appliedeffect;

	// Use this for initialization
	void Start () {
        effect[0] = 1; realeffect[0] = 0.05f;
        effect[1] = 0; realeffect[1] = -2;
        effect[2] = 0; realeffect[2] = 30;
        effect[3] = 0; realeffect[3] = 100;
        effect[4] = 0; realeffect[4] = 3;
        effect[5] = 1; realeffect[5] = 2;
        effect[6] = 0; realeffect[6] = 0.1f;

        buycat = gameObject.GetComponent<ControlGameData>().getBuycat();
    }
	
	// Update is called once per frame
	public float[] SettingCatEffect() {
        appliedeffect = new float[7];

        for(int i = 1; i < buycat.Length; i++)
        {
            if (buycat[i] != -1)
                appliedeffect[i - 1] = realeffect[i - 1];
            else
                appliedeffect[i - 1] = effect[i-1];
        }

        return appliedeffect;
	}
}
