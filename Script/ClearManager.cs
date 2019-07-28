using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour {


    Vector3 mousePos;
    Vector2 mousePos2D;

    RaycastHit2D hit;

    GameObject clickedObject;

    public GameObject alertPosition;

    public GameObject buttonPosition1;
    public GameObject buttonPosition2;


    public Text stageText;


    void Start()
    {
        stageText.text = "STAGE " + String.Format("{0:00}", Stage.stageNum);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos2D = new Vector2(mousePos.x, mousePos.y);


            hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                clickedObject = hit.collider.gameObject;
                Renderer renderer = clickedObject.GetComponent<Renderer>();
                renderer.material.color = Color.gray;

            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (clickedObject == null)
                return;
            Renderer renderer = clickedObject.GetComponent<Renderer>();
            renderer.material.color = Color.white;

            //넘어가기
            DontDestroyOnLoad(this);

            if (hit.collider.gameObject.transform.tag.Equals("next_button"))
            {
                Stage.stageNum++;
                SceneManager.LoadScene(3);

            }
            else if (hit.collider.gameObject.transform.tag.Equals("menu_button"))
            {
                SceneManager.LoadScene(1);

            }

        }


    }

   
}
