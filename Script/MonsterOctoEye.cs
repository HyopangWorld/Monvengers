using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOctoEye : MonoBehaviour {
	
	public GameObject arrowObj;
	public Transform shootPostf;

	public int monsterHeart; //몬스터의 목숨
	public int monsterDemage; //몬스터가 주는 데미지
	public int attackCoolTime; //몬스터가 주는 데미지


	private bool isThrowWeapon = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameController.dustPlacedNum [gameObject.name.Replace ("MV_monster", "") [0] - '0'] != 0) {
			Debug.Log("이거봐 이름 : "+gameObject.name+" INDEX : "+ GameController.dustPlacedNum[gameObject.name.Replace("MV_monster", "")[0] - '0']);
			StartCoroutine(Attack());
			if (gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).nameHash == Animator.StringToHash ("Base Layer.Attack")) {
				if (isThrowWeapon) {
					isThrowWeapon = false;
					Invoke ("CreateWeapon", 0.3f);
				}

			} 
			else {

				isThrowWeapon = true;
			}

		}

	}

	void CreateWeapon(){
		Instantiate (arrowObj).transform.position = shootPostf.position;
	}


	IEnumerator Attack()
	{
		GetComponent<Animator> ().SetTrigger ("Shoot_t");
		yield return new WaitForSeconds (3);

	}

}


