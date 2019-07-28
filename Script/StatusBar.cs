using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {

    public Slider statusBar;


	// Use this for initialization
	void Start () {
        statusBar.value = 1;

	}
	
	// Update is called once per frame
	void Update () {
        if (statusBar.value >= 0)
            statusBar.value = (float)1.0 / ClickManager.gameClearMax * ClickManager.gameClearGage;

      //  Debug.Log("status : "+statusBar.value);
     //   Debug.Log(" ClickManager.gameClearMax : " + ClickManager.gameClearMax);

       
	}
}
