using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecallNumbersScript : MonoBehaviour {

	public int indexNumber;
	public int value;
	public bool isOccupied;
	public Sprite recallNumberSprite;
	public SpriteRenderer recallNumberSR;
	public GM GM_Script;
	public BallManager BM_Script;
	public SoundManagerScript SM_Script;

	public Vector3 gamePos;
	public Vector3 menuPos;

	public float speedMoving;
	
	
	public bool moving;
	public bool movingToGame;

	public bool isToBeScaled;
	public Vector3 gameScale;
	public Vector3 menuScale;

	// Use this for initialization
	void Start () {

		moving = false;
		movingToGame = false;
		speedMoving = 50.0f;

		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();
		BM_Script = GameObject.FindGameObjectWithTag ("BallManager").GetComponent<BallManager>();
		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
		StartMoving ();
		StartMovingToGame ();
	
	}


	void StartMoving(){
		if (moving)
		{
			//menuTitlePos = new Vector3(transform.localPosition.x, menuPos.y, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, menuPos) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, menuPos, Time.deltaTime*speedMoving);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localPosition = menuPos;
				transform.localScale = menuScale;
				moving = false;
			}


			if (Vector3.Distance(transform.localScale, menuScale) > 0.1f)
			{
				transform.localScale = Vector3.MoveTowards(transform.localScale, menuScale, Time.deltaTime*speedMoving);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localScale = menuScale;
				transform.localPosition = menuPos;
				moving = false;
			}
		}
		
	}
	
	void StartMovingToGame(){
		if (movingToGame)
		{
			//menuTitlePos = new Vector3(transform.localPosition.x, menuPos.y, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, gamePos) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, gamePos, Time.deltaTime*speedMoving);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localPosition = gamePos;
				transform.localScale = gameScale;
				movingToGame = false;
			
			}

			if (Vector3.Distance(transform.localScale, gameScale) > 0.1f)
			{
				transform.localScale = Vector3.MoveTowards(transform.localScale, gameScale, Time.deltaTime*speedMoving);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localScale = gameScale;
				transform.localPosition = gamePos;
				movingToGame = false;
				
			}
		}
		
	}
	void OnTriggerEnter(Collider other){
		if(other.CompareTag ("Ball3D")){
			
			//Vector3.Distance(transform.eulerAngles, new Vector3(90,270,0)) > 5.0f

			SM_Script.Play_SFX(other.gameObject.GetComponent<Ball3D>().value);
			GM_Script.DisplayRecallNumbers(other.gameObject.GetComponent<Ball3D>().value);
			BM_Script.DisplayGameNumbers(other.gameObject);

			Debug.Log ("Hex got Hit ontrigger");
			
		}
	}


}
