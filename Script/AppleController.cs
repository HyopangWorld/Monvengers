using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Btn.flag == 0) { 
            transform.Translate(0, -0.03f, 0);
            if (transform.position.y < -5.0f)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
