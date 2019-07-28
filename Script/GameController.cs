using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public Text gameOvertext = null;

    public GameObject  BoxMakePosition1, BoxMakePosition2, BoxMakePosition3, BoxMakePosition4, BoxMakePosition5; // 미세먼지 생성위치
    public GameObject MV_dustDD, MV_dustGG, MV_dustPP, MV_dustTT;
    GameObject BoxMakePosition = null;
    GameObject BoxPrefab = null;

    int time = 0; // 프레임 반복

    public Text heartText = null; // 목숨 text
    public static int heart = 1; //목숨 값이 들어 있는 변수

    static int count = 0;//미세먼지 생성 갯수

	public static int[] dustPlacedNum = new int[5];//해당 행에 설치된 먼지 수

    void Start()
    {
        //목숨 초기화
        heart = 1;
       // Debug.Log("Stage:"+Stage.stageNum+" level:"+Stage.level);
        for (int i = 0; i < 5; i++) {
			dustPlacedNum [i] = 0;
		}
        //CreateDust();
    }

    // 반복
    void Update()
    {
        //게임 클리어 체크
        if (ClickManager.gameClearGage <= 0)
        {
            GameClear();
        }
       

        //게임오버 체크
        if (heart <= 0)
        {
            heartText.text = "0";
            GameOver();
        }
        else
        {
            if (Btn.flag == 0)
            { 
            time++;

            //미세먼지 생성
            if ((time % Stage.level) == 0 || time == 10)
            {
                CreateDust();
            }

            heartText.text = heart.ToString();
        }
        }
    }
    void GameClear()
    {
       
        //Debug.Log("게임 걸린 시간 : "+sw.ElapsedMilliseconds.ToString() + "ms");

        SceneManager.LoadScene(4);


    }
    void GameOver()
    {
        SceneManager.LoadScene(5);

    }

    // 반복하면서 나올 캐릭터
    void CreateDust()
    {
        int rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                BoxMakePosition = BoxMakePosition1;
                break;
            case 2:
                BoxMakePosition = BoxMakePosition2;
                break;
            case 3:
                BoxMakePosition = BoxMakePosition3;
                break;
            case 4:
                BoxMakePosition = BoxMakePosition4;
                break;
            case 5:
                BoxMakePosition = BoxMakePosition5;
                break;
        }

        int rand2 = Random.Range(1, Stage.stageNum);
        switch(rand2)
        {
            case 9: BoxPrefab = MV_dustPP; break;
            case 8: BoxPrefab = MV_dustPP; break;
            case 7: BoxPrefab = MV_dustPP; break;
            case 6: BoxPrefab = MV_dustGG; break;
            case 5: BoxPrefab = MV_dustGG; break;
            case 4: BoxPrefab = MV_dustTT; break;
            case 3: BoxPrefab = MV_dustTT; break;
            case 2: BoxPrefab = MV_dustDD; break;
            case 1: BoxPrefab = MV_dustDD; break;
        }

        Renderer monRenderer = BoxPrefab.GetComponent<Renderer>();
        monRenderer.sortingOrder = (rand - 1) * 10 + 8;

        //Resources/Prefab/Moster.prefab 로드 
        GameObject prefab = BoxPrefab as GameObject;
        GameObject dust = MonoBehaviour.Instantiate(prefab) as GameObject;

        //실제 인스턴스 생성 
		dust.name = "dust" + (rand-1) + count.ToString();
        dust.transform.position = BoxMakePosition.transform.position;

		dustPlacedNum [rand - 1]++;
        count++;
    }
}
