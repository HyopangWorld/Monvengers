using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickManager : MonoBehaviour {


    public static int gameClearGage = 0;
    public static int gameClearMax = 0;

    public GameObject backgroundObject;


    public Sprite backgroundSprite2;
    public Sprite backgroundSprite1;

    private Sprite[] backgroundSprite = new Sprite[2];

    private int backStatus = 2; 

	private int numOfMonsterCard = 0;

    

    private long gameTime=0;

	public Text airText;
	private String TAG = "ClickManager : ";
	public static String clickedMonsterCardKeyword=null;

	public GameObject[] monsterCard= new GameObject[6];


	private	GameObject clickedCardObject;



	GameObject clickedObject;


	Vector3 mousePos;
	Vector2 mousePos2D;

	RaycastHit2D hit;

	public static bool[,] isMonsterPlaced = new bool[5, 8];

	String[] monsterNameArray = {"OT","GR","MT","H","II","SK"};
	//0 : 옥토아이50, 1 : 그루토리40, 2 : 민토르100, 3 : 헐150, 4 : 아이매워200, 5 : 스칼렛워치250 
	//50 40 100 150 200 250
	int[] monsterPrice = {50,40,100,150,200,250};

	// Use this for initialization
	void Start () {

		switch (Stage.stageNum) {
		case 1:
			numOfMonsterCard = 1;
            gameClearMax = 10;
			break;
		case 2:	case 3:case 4:
			numOfMonsterCard = 2;
            gameClearMax = 15;
			break;
		case 5:	case 6: 
			numOfMonsterCard = 3;
            gameClearMax = 18;
			break;
		case 7:
			numOfMonsterCard = 4;
            gameClearMax = 20;
			break;
		case 8:case 9:
			numOfMonsterCard = 5;
            gameClearMax = 25;
			break;
		default:
			numOfMonsterCard = 5;
            gameClearMax = 25;
			break;

		}

        gameClearGage = gameClearMax;
        backgroundSprite[0] = backgroundSprite1;
        backgroundSprite[1] = backgroundSprite2;

		for (int i = numOfMonsterCard+1; i < monsterCard.Length; i++) {
			monsterCard[i].SetActive(false);

		}
        
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 8; j++) {
				isMonsterPlaced [i,j] = false;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

        
        if (backStatus >= 1 && gameClearMax / (float)3.0 * backStatus >= gameClearGage)
        {
            SpriteRenderer renderer = backgroundObject.GetComponent<SpriteRenderer>();
            renderer.sprite = backgroundSprite[backStatus-1];
            backStatus--;

        }

        if (Btn.flag == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickedObject = null;

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos2D = new Vector2(mousePos.x, mousePos.y);


                hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {

                    clickedObject = hit.collider.gameObject;


                    if (clickedMonsterCardKeyword == null)
                    {//몬스터카드를 눌렀거나 몬스터카드를 누르지않은채로 필드를 눌렀을 때
                        Color color = new Color(233, 233, 239, 0.2f);
                        if (clickedObject.transform.tag.Equals("monstercard"))
                        {
                            Renderer renderer = clickedObject.GetComponent<Renderer>();
                            renderer.material.color = Color.gray;
                            clickedCardObject = clickedObject;
                        }

                    }
                    else
                    {//필드를 눌렀을 때
                        Renderer renderer = clickedObject.GetComponent<Renderer>();
                        renderer.material.color = Color.gray;
                    }
                }


            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (clickedObject == null)
                    return;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject gameObject = clickedObject;
                    //Debug.Log (TAG + "click up gameobject name" + gameObject.name);

                    if (clickedObject.name.Contains("FieldTile"))
                    {
                        MonsterGenerator(clickedObject);
                        clickedMonsterCardKeyword = null;

                        Renderer fieldRenderer = clickedObject.GetComponent<Renderer>();
                        fieldRenderer.material.color = Color.white;

                        if (clickedCardObject != null)
                        {
                            Renderer cardRenderer = clickedCardObject.GetComponent<Renderer>();
                            cardRenderer.material.color = Color.white;

                            clickedCardObject = null;
                        }

                        clickedObject = null;

                    }
                    else if (clickedObject.name.Contains("MonsterCard"))
                    {

                        if (clickedMonsterCardKeyword != null)
                        {
                            Renderer cardRenderer = clickedCardObject.GetComponent<Renderer>();
                            cardRenderer.material.color = Color.white;
                            clickedMonsterCardKeyword = null;
                        }
                        else
                        {
                            MonsterCardIndexChanger(clickedObject);
                        }
                        return;
                    }
                    else
                    {
                        clickedMonsterCardKeyword = null;
                        Renderer fieldRenderer = clickedObject.GetComponent<Renderer>();
                        fieldRenderer.material.color = Color.white;
                        if (clickedCardObject != null)
                        {

                            Renderer cardRenderer = clickedCardObject.GetComponent<Renderer>();
                            cardRenderer.material.color = Color.white;
                            clickedCardObject = null;
                            clickedObject = null;
                        }
                    }

                }
                else
                {//타일을 벗어났다면 

                    clickedMonsterCardKeyword = null;
                    if (clickedObject != null)
                    {
                        Renderer fieldRenderer = clickedObject.GetComponent<Renderer>();
                        fieldRenderer.material.color = Color.white;
                        clickedObject = null;
                    }
                }
            }
        }
	}

	void MonsterGenerator(GameObject fieldTile){//몬스터 생성 
		//TODO: clickedMonsterCardIndex>5  == > 5를 해당 맵의 몬스터 총 갯수로
		if (clickedMonsterCardKeyword == null)//몬스터 카드가 선택된 것이 아님 
			return;
		
		int price = 0;
		int i;
		for (i = 0; i < monsterPrice.Length; i++) {
			if (clickedMonsterCardKeyword.Equals (monsterNameArray [i])) {
				price = monsterPrice [i];
				break;
			}

		}
		float placeGap=0.0f;

		//0 : 옥토아이50, 1 : 그루토리40, 2 : 민토르100, 3 : 헐150, 4 : 아이매워200, 5 : 스칼렛워치250
		switch (i) {
		case 0:case 3:
			placeGap = 0.0f;
			break;
		case 2:
			placeGap=0.3f;
			break;
		case 4:
			placeGap=0.1f;
			break;
		case 5:
			placeGap=-0.1f;
			break;

		}

		//TODO : 선택한 몬스터카드에 따라 가격 바뀌게
			String fieldTileName = fieldTile.name.Replace ("FieldTile_", "");
			if (!isMonsterPlaced [fieldTileName [0] - '0',fieldTileName [1] - '0']) {//해당 자리가 비어있을 때만
				//Resources/Prefab/Moster.prefab 로드 
				GameObject prefab = Resources.Load ("Prefab/MV_monster" + clickedMonsterCardKeyword) as GameObject;
				GameObject monster = MonoBehaviour.Instantiate (prefab) as GameObject;
				//실제 인스턴스 생성 
				monster.name = "MV_monster" + fieldTile.name.Replace ("FieldTile_", "");
				monster.transform.position = hit.collider.gameObject.transform.position;

			

				Vector3 vector = new Vector3 (0, placeGap);
				monster.transform.position += vector;

				Renderer monRenderer = monster.GetComponent<Renderer> ();

				monRenderer.sortingOrder = Convert.ToInt32 (fieldTile.name.Replace ("FieldTile_", ""));

				isMonsterPlaced [fieldTileName [0] - '0',fieldTileName [1] - '0'] = true;
				airText.text = Convert.ToString(Convert.ToInt32 (airText.text) -price);
				clickedMonsterCardKeyword = null;
			} 
			else {
				clickedMonsterCardKeyword = null;
			}

	}

	void MonsterCardIndexChanger(GameObject monsterCard){

		clickedMonsterCardKeyword = monsterCard.name.Replace ("MonsterCard_", "");

		int price = 0;
		int i;
		for (i = 0; i < monsterPrice.Length; i++) {
			if (clickedMonsterCardKeyword.Equals (monsterNameArray [i])) {
				price = monsterPrice [i];
				break;
			}

		}

		if (Convert.ToInt32 (airText.text) >= price) {

		} else {
			clickedMonsterCardKeyword = null;
			if (clickedCardObject != null) {
				Renderer cardRenderer = clickedCardObject.GetComponent<Renderer> ();
				cardRenderer.material.color = Color.white;
				clickedCardObject = null;
			}
		}


	}
		
}
