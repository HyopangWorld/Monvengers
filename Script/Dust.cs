using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dust : MonoBehaviour
{
    int time = 0; // 프레임 반복
    public float speed;
    public int dustHeart;
    public int dustAttack;
    Monster ms;
    gru gr;
    int monsterFlag;
    public static Collider2D colMonster = null;

	private bool colorFlag = false;


    private string TAG = "Dust : ";

    void OnTriggerEnter2D(Collider2D col)
    {
        //미세먼지와 몬스터가 부딪혔을 때
        //Debug.Log(TAG + " 부딪힌 객체 이름" + col.name);
		if (col.transform.tag.Equals("monster")) 
        {
            gameObject.transform.tag = "stopDust"; //부딪힌 미세먼지의 태그를 stopDust
            ms = col.GetComponent<Monster>(); // 부딪힌 몬스터의 객체를 ms에 넣는다.
            GetComponent<Animator>().SetBool("isCracked", true);
            this.monsterFlag = 0;
        }
        //미세먼지와 그루토리가 부딪혔을때
        else if (col.transform.tag.Equals("gru"))
        {
            gameObject.transform.tag = "stopDust"; //부딪힌 미세먼지의 태그를 stopDust
            gr = col.GetComponent<gru>(); // 부딪힌 몬스터의 객체를 ms에 넣는다.
            this.monsterFlag = 1;
        }

        //미세먼지와 무기가 부딪혔을 때
        else if (col.transform.tag.Equals("weapon"))
        {
			dustHeart-=col.GetComponent<Weapon>().demage;
			Destroy(col.gameObject);

			//빤짝거리기
			changeColor ();

			Invoke ("changeColor",0.2f);
        }
    }
    public Animation attack;
    void Update()
    {
        // 게임이 종료되지 않았을 때
        if (GameController.heart > 0 && Btn.flag == 0)
        {
            if (gameObject.transform.tag.Equals("dust")) // 일반 미세먼지는
            {
                gameObject.transform.Translate(Vector2.left * speed); //전진
                
            }

            else if (gameObject.transform.tag.Equals("stopDust") && this.monsterFlag == 0  && ms.monsterHeart >= 0) // 미세먼지와 몬스터가 부딪히고 부딪힌 몬스터의 목숨이 남아있으면
            {
                this.time++;
                GetComponent<Animator>().SetBool("isCracked", true);

                if (this.time % 20 == 0) { ms.monsterHeart -= this.dustAttack; } //미세먼지가 몬스터를 공격, 몬스터의 목숨이 준다.
            }
            else if (gameObject.transform.tag.Equals("stopDust") && this.monsterFlag == 0  && ms.monsterHeart < 0) // 미세먼지와 몬스터가 부딪히고 부딪힌 몬스터의 목숨이 다하면
            {
                GetComponent<Animator>().SetBool("isCracked", false);
                Destroy(ms.gameObject); //몬스터 제거

                //기존 위치 다시 배치 가능하게
                String mosterName = ms.gameObject.name.Replace("MV_monster", "");
                ClickManager.isMonsterPlaced[mosterName[0] - '0', mosterName[1] - '0'] = false;


                gameObject.transform.tag = "dust"; // 미세먼지는 다시 전진
                GetComponent<Animator>().SetBool("attack", false);
            }

            else if (gameObject.transform.tag.Equals("stopDust") && this.monsterFlag == 1  && gr.monsterHeart >= 0) // 미세먼지와 그루토리 부딪히고 부딪힌 몬스터의 목숨이 남아있으면
            {
                this.time++;
                if (this.time % 20 == 0) { gr.monsterHeart -= this.dustAttack;} //미세먼지가 몬스터를 공격, 몬스터의 목숨이 준다.
            }
            else if (gameObject.transform.tag.Equals("stopDust") && this.monsterFlag == 1 && gr.monsterHeart < 0) // 미세먼지와 그루토리 부딪히고 부딪힌 몬스터의 목숨이 다하면
            {
                Destroy(gr.gameObject); //몬스터 제거
                gameObject.transform.tag = "dust"; // 미세먼지는 다시 전진
            }

        }

        // 미세먼지의 목숨이 다하면 사라진다.
        if (dustHeart <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die_t");
            GameController.dustPlacedNum [gameObject.name.Replace ("dust", "") [0]-'0']--;
            ClickManager.gameClearGage--;
            Destroy(gameObject);
        }
        
    }

    //미세먼지와 무기가 부딪히면 호출되는 함수
	void changeColor(){

        Debug.Log("무기에 맞음 : " + gameObject.name);

        Renderer renderer = gameObject.GetComponent<Renderer>();

		if (colorFlag) {
			renderer.material.color = Color.white;
			colorFlag = false;

		} else {
			renderer.material.color = Color.gray;
			colorFlag = true;
		}
			
	}
    
}
