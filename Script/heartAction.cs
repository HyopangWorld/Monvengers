using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartAction : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		if(col.transform.tag.Equals("dust")){
			GameController.heart--;
			Destroy (col.gameObject);
		}
	}
}
