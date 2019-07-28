using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Btn : MonoBehaviour {
    private bool pauseOn = false;
    public GameObject PauseUI;
    public static int flag = 0;

 
    public void ActivePauseBt()//일시정지 버튼 눌렀을 때
    {
        if (!pauseOn) //일시정지 아니면 일시정지
        {
            PauseUI.gameObject.SetActive(true);
            flag = 1;
            Time.timeScale = 0;
            
        }
        else
        { //일시정지 중이면 해제
            PauseUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        pauseOn = !pauseOn;
    }

    public void ResumeBt() //재시작
    {
        PauseUI.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
