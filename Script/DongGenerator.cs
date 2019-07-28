using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DongGenerator : MonoBehaviour
{
    public GameObject MV_air_original1;
    GameObject go;
    RaycastHit2D hit;
    float span = 6.0f;
    float delta = 0;
    int speed = 20;
    // Use this for initialization
 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Btn.flag == 0) { 
            this.delta += Time.deltaTime;//Time.deltaTime은 앞 프레임과 현재 프레임의 시간 차이
            if (this.delta > this.span)
            {
                this.delta = 0;
                go = Instantiate(MV_air_original1) as GameObject;

                int px = Random.Range(-5, 8);//x좌표 랜덤 범위
                
                Debug.Log("DongGenerator 공기생성");
                go.transform.position = new Vector3(px, 5, 0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
			    if (hit.collider !=null && hit.collider.tag == "air")
                {
                    GameObject.Find("GameManager").GetComponent<ScoreManager>().PlusScore(10);//ScoreManager에서 PlusScore함수 호출
                    Destroy(go);
                }
            }
        }
            
    }
}
 
 