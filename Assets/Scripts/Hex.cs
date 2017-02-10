using UnityEngine;
using System.Collections;

public class Hex : MonoBehaviour {



	public bool rotating;
	public bool moving;
	public bool movingBackTitle;
	public bool movingBackGame;
	public Vector3 gamePos;
	public Vector3 menuPos;
	public Vector3 menuTitlePos;
	public float speedRotation;
	public float yPosTitle;
	public float backTitleNum;
	public float backTitleNumXPos; //position for moving back title
	public Vector3 backPosTemp;
	public int rotateTempNum;
	public bool changeLength;
	public bool hasChild;


	// Use this for initialization
	void Start () {

		hasChild = false;
		rotating = false;
		moving = false;
		movingBackTitle = false;
		movingBackGame = false;

		speedRotation = 100.0f;

		changeLength = false;




	
	}
	
	// Update is called once per frame
	void Update () {
		StartRotating ();
		StartMoving ();
		MoveMenuButton ();
		MoveGameButton ();

	
	

	}

	public void SetBackTitle(Vector3 tempVect){
		backPosTemp = tempVect;
	}


	void StartRotating(){

		if (rotating)
		{
			menuPos = new Vector3(0, rotateTempNum, 0);
			if (Vector3.Distance(transform.eulerAngles, menuPos) > 0.1f)
			{
				transform.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, menuPos, Time.deltaTime*speedRotation, 3.0f);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localEulerAngles = menuPos;
				rotating = false;
			}
		}

	}//End StartRotating();


	void StartMoving(){
		if (moving)
		{
			menuTitlePos = new Vector3(transform.localPosition.x, yPosTitle, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, menuTitlePos) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, menuTitlePos, Time.deltaTime*speedRotation);
				//transform.localEulerAngles += new Vector3(1,0,0);
			}
			else {
				transform.localPosition = menuTitlePos;
				moving = false;
			}
		}

	}

	void MoveMenuButton(){
	
		if (movingBackTitle) {
			if (Vector3.Distance(transform.localPosition, backPosTemp) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, backPosTemp , Time.deltaTime * speedRotation);
			}
			else{
				transform.localPosition = backPosTemp;
				movingBackTitle = false;
			}
		} 

	
	}

	void MoveGameButton(){
		if (movingBackGame) {
			if (Vector3.Distance(transform.localPosition, gamePos) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, gamePos , Time.deltaTime * speedRotation);
			}
			else{
				transform.localPosition = gamePos;
				movingBackGame = false;
			}
		}

	}

	public void ChangeLengthOfHex(float temp){
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, temp);
	}






}
