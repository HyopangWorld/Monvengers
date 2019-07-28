using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gru : MonoBehaviour
{
    int time = 0;
    public GameObject air;
    GameObject go;
    RaycastHit2D hit;
    public Transform gruPos; 
    public int monsterHeart = 40; //몬스터의 목숨

    void Start()
    {
        this.gruPos = this.gameObject.transform;
        Debug.Log(this.gruPos);
        Debug.Log(this.gruPos.position.y);
    }

    void Update()
    {
        time++;
        if (time % 800 == 0)
        {
            Debug.Log("그루토리의 공기생성");
            this.go = Instantiate(air) as GameObject;
            this.go.transform.position = new Vector3(this.gruPos.position.x+1, this.gruPos.position.y, this.gruPos.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if (hit.collider != null && hit.collider.tag == "gr_air")
            {
                GameObject.Find("GameManager").GetComponent<ScoreManager>().PlusScore(10);//ScoreManager에서 PlusScore함수 호출
                Destroy(this.go);
            }
        }

    }
 }
