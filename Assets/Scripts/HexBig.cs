using UnityEngine;
using System.Collections;

public class HexBig : MonoBehaviour {

	public bool rotating;
	public bool rotatingToGame;
	public bool moving;
	public bool movingToGame;
	public bool movingBackTitle;
	public bool movingBackGame; // game position
	public Vector3 gamePos;
	public Vector3 menuPos;
	public Vector3 menuPosBig; // menu position
	public Vector3 menuPosBigGame;
	public Vector3 bigRotateToGame;
	public Vector3 menuTitlePos;
	public float speedRotation;
	public float speedRotationBack;
	public float speedMoving;
	public float speedMovingBack;
	public float yPosTitle;
	public float backTitleNum;
	public float backTitleNumXPos; //position for moving back title
	public Vector3 backPosTemp;
	public int rotateTempNum;
	public bool changeLength;
	public GameObject GM;
	public GM GM_Script;

	public string menuButtonName;

	//for Big Hex Sprite

	public Sprite bigHexSprite;
	public string bigHexSpriteString;
	public SpriteRenderer bigHexSpriteRenderer;

	public Vector3 newRotationForTumbler;

	//End Big Hes Sprite


	
	
	// Use this for initialization
	void Start () {
		
		rotating = false;
		rotatingToGame = false;
		moving = false;
		movingToGame = false;

		speedRotation = 40.0f;
		speedRotationBack = 75.0f;
		speedMoving = 60.0f;
		speedMovingBack = 85.0f;
		GM = GameObject.FindWithTag("GameManager");
		GM_Script = GM.GetComponent<GM> ();
		
	}

	public void LoadSprite(){
		bigHexSprite = Resources.Load<Sprite>("MenuButtons/"+bigHexSpriteString);

					
		bigHexSpriteRenderer = transform.GetChild (0).GetComponent<SpriteRenderer>();

		//transform.GetChild (0).transform.localScale = new Vector3(0.58f, 0.58f, 0.58f);
		//menuButtonSpriteRenderer = GetComponent<SpriteRenderer> ();
		bigHexSpriteRenderer.sprite = bigHexSprite;
		//transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
	}

	
	// Update is called once per frame
	void Update () {
		StartRotating ();
		StartMoving ();

		StartMovingToGame ();
		StartRotatingToGame ();
	
		
		
		
	}
	

	
	
	void StartRotating(){
		
		if (rotating)
		{
			menuPos = new Vector3(0, rotateTempNum, 0);
			if (Vector3.Distance(transform.eulerAngles, menuPos) > 0.1f)
			{
				transform.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, menuPos, Time.deltaTime*speedRotationBack, 10.5f);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localEulerAngles = menuPos;
				rotating = false;
				GM_Script.MoveBigMenuButtonToMenu(menuButtonName);
			}
		}
		
	}//End StartRotating();

	void StartRotatingToGame(){
		
		if (rotatingToGame)
		{
			//menuPos = new Vector3(rotateTempNum,0, 0);
			if (Vector3.Distance(transform.eulerAngles, bigRotateToGame) > 0.1f)
			{
				transform.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, bigRotateToGame, Time.deltaTime*speedRotationBack, 10.5f);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localEulerAngles = bigRotateToGame;
				rotatingToGame = false;
			}
		}
		
	}//End StartRotating();
	
	
	void StartMoving(){
		if (moving)
		{
			//menuTitlePos = new Vector3(transform.localPosition.x, menuPos.y, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, menuPosBig) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, menuPosBig, Time.deltaTime*speedMoving);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localPosition = menuPosBig;
				moving = false;

			}
		}
		
	}

	void StartMovingToGame(){
		if (movingToGame)
		{
			//menuTitlePos = new Vector3(transform.localPosition.x, menuPos.y, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, menuPosBigGame) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, menuPosBigGame, Time.deltaTime*speedMoving);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localPosition = menuPosBigGame;
				movingToGame = false;
			}
		}
		
	}
	/*--
	void OnTriggerEnter(Collider other){
		if(other.CompareTag ("Ball3D")){

			//Vector3.Distance(transform.eulerAngles, new Vector3(90,270,0)) > 5.0f

			if(Vector3.Distance(transform.eulerAngles, new Vector3(90,270,0)) < 5.0f){
				newRotationForTumbler = new Vector3(90,90,0);
			}
			else if(Vector3.Distance(transform.eulerAngles, new Vector3(90,90,0)) < 5.0f){
				newRotationForTumbler = new Vector3(90,270,0);
			}

			StartCoroutine(RotateForBall3D());
			Debug.Log ("Hex got Hit ontrigger");

		}
	}

	


	IEnumerator RotateForBall3D(){
		while(Vector3.Distance(transform.eulerAngles, newRotationForTumbler) > 5.0f){

			transform.eulerAngles = Vector3.MoveTowards(transform.rotation.eulerAngles, transform.up, Time.deltaTime*speedRotationBack);
			//transform.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, newRotationForTumbler, Time.deltaTime*speedRotationBack, 10.0f);
			//transform.eulerAngles += new Vector3(0,5,0);
			yield return new WaitForSeconds (0.1f);
		}
		transform.localEulerAngles = newRotationForTumbler;


	}

	--*/


	
	
	

}
