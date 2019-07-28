using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public int scorePoint = 50;
    //private Text scoreTx;

	public Text airText = null;

    void Awake()
    {
        //scoreTx = GameObject.Find("NormalUI").transform.Find("scoreTx").GetComponent<Text>();
		//airText.text = "50";
       // StartCoroutine("PlusScoreRoutine");
    }

    public void PlusScore(int plusPoint)
    {
		airText.text = Convert.ToString(Convert.ToInt32(airText.text)+plusPoint);
    }

	// Use this for initialization
	void Start () {
		//scorePoint = Convert.ToInt32(airText.text);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
