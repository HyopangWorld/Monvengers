using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDestroy : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log(col.transform.tag);
		if (col.transform.tag.Equals ("weapon")) {
			Destroy (col.gameObject);
		}
	}
}
