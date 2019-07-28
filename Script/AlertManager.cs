using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour {

	private int minOfMonsterCard = -1;
	private int maxOfMonsterCard = -1;


	Vector3 mousePos;
	Vector2 mousePos2D;

	RaycastHit2D hit;

	GameObject clickedObject;

	private int presentCardNum ;

	private GameObject presentCard;

	public GameObject alertPosition;
	public GameObject buttonPosition;


	String[] alertCardNameArray = {"GR","OT","MT","H","II","SK"};


	void Start () {
		switch (Stage.stageNum) {
		case 1:
			minOfMonsterCard = 0;
			maxOfMonsterCard = 1;
			break;
		case 2:	
			minOfMonsterCard = 2;
			maxOfMonsterCard = 2;
			break;
		case 5:	
			minOfMonsterCard = 3;
			maxOfMonsterCard = 3;
			break;
		case 7:
			minOfMonsterCard = 4;
			maxOfMonsterCard = 4;
			break;
		case 8:
			minOfMonsterCard = 5;
			maxOfMonsterCard = 5;
		break;
		default:
			minOfMonsterCard = -1;
			minOfMonsterCard = -1;
			break;

		}



		presentCardNum = minOfMonsterCard;
		AlertMonsterCard();
		if (minOfMonsterCard >= 0) {
			//Resources/Prefab/Moster.prefab 로드 
			GameObject prefab = Resources.Load ("Prefab/NextButton") as GameObject;
			GameObject button = MonoBehaviour.Instantiate (prefab) as GameObject;

			//실제 인스턴스 생성 
			button.name = "NextButton";
			button.transform.position = buttonPosition.transform.position;
		}

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos2D = new Vector2 (mousePos.x, mousePos.y);


			hit = Physics2D.Raycast (mousePos2D, Vector2.zero);
			if (hit.collider != null) {
				if (hit.collider.gameObject.transform.tag.Equals ("next_button")) {
					clickedObject = hit.collider.gameObject;
					Renderer renderer = clickedObject.GetComponent<Renderer>();
					renderer.material.color = Color.gray;

				}

				Debug.Log (hit.collider.gameObject.transform.tag);



			}


		}else if(Input.GetMouseButtonUp (0)){
			if (clickedObject == null)
				return;
			Renderer renderer = clickedObject.GetComponent<Renderer>();
			renderer.material.color = Color.white;

			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePos2D = new Vector2 (mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast (mousePos2D, Vector2.zero);
			if (hit.collider != null) {
				GameObject gameObject = clickedObject;

				if (hit.collider.gameObject.transform.tag.Equals ("next_button")) {
					//다음으로
					presentCardNum++;
					AlertMonsterCard();
				}
			

			}	

	}


	}

	void AlertMonsterCard(){
		if(presentCardNum<0 || presentCardNum>maxOfMonsterCard){
			//넘어가기
			DontDestroyOnLoad(this);
			SceneManager.LoadScene(3);
		}
		else{
			if(presentCard!=null){
				Destroy(presentCard);
			}
			//Resources/Prefab/Moster.prefab 로드 
			GameObject prefab = Resources.Load ("Prefab/MV_alert" + alertCardNameArray[presentCardNum]) as GameObject;
			GameObject alertCard = MonoBehaviour.Instantiate (prefab) as GameObject;
		
			//실제 인스턴스 생성 
			alertCard.name = "MV_alert"+alertCardNameArray[presentCardNum];
			alertCard.transform.position = alertPosition.transform.position;

			presentCard = alertCard;

		}


	}


}
