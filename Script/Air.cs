using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour{
    //public GameObject air;
    //RaycastHit2D hit;
 /*private AirManager airManager; //아까 만든 코드 
 private void Start(){ 
  airManager = GameObject.Find("spawn").GetComponent<AirManager>(); //Canvas오브젝트의 AirManager 불러오기 
 } */
 public void OnClick(){
     Debug.Log("onclick 클릭!");
     GameObject.Find("GameManager").GetComponent<ScoreManager>().PlusScore(10);//ScoreManager에서 PlusScore함수 호출
     Destroy(this.gameObject); //이 오브젝트는 파괴하자. 
 } 
}    
