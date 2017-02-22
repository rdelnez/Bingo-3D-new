using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BingoCardBig : MonoBehaviour {

	public GM GM_Script;
	public GameObject[] children;
	public List<bool> tempBlueButtonActiveList;
	public List<bool> resetBlueButtonActiveList;
	public List<string> tempMovingPattern;
	public bool patternIsAnimatingAcross;
	public bool patternIsAnimatingDown;
	public string patternTest;
	public int tempY;
	public bool coroutineHasStarted=false;
	public bool isCustomPattern;



	// Use this for initialization
	void OnEnable(){
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM> ();
		GetChildren ();

		if(coroutineHasStarted){
			StartAnimDisplayAcross();
			StartAnimDisplayDown();
		}

		UpdateBingoCardSingleDisplay ();
	}

	void Awake(){


	}
	void Start () {
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM> ();
		GetChildren ();

		isCustomPattern = false;
		tempBlueButtonActiveList = new List<bool>();
		resetBlueButtonActiveList = new List<bool>();
		for(int x=0; x<25; x++){
			resetBlueButtonActiveList.Add (false);
		}

		tempMovingPattern = new List<string>();
		patternIsAnimatingAcross = false;
		patternIsAnimatingDown = false;
		patternTest = "01";
		tempY = 0;

		UpdateBingoCardSingleDisplay ();


			
	}
	
	// Update is called once per frame
	void Update () {
		//UpdateBingoCardDisplay ();
	}

	//Start this is for getting the children and store it in a list
	public void GetChildren(){
	
		children = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].GetComponent<BingoBlueButton>().buttonIndex = i;

		}

	}
	//END this is for getting the children and store it in a list

	public void UpdateBingoCardSingleDisplay(){

		for (int i = 0; i < transform.childCount; i++)
		{
			if(children[i].GetComponent<SpriteRenderer>().enabled==false){
				children[i].GetComponent<SpriteRenderer>().enabled=true;
			}
		}


		tempBlueButtonActiveList = GM_Script.blueIsActiveList;
		UpdateBingoCardDisplay (tempBlueButtonActiveList);
	}

	public void StartAnimDisplayAcross(){
		coroutineHasStarted = true;
		tempY = 0;
		StartCoroutine (UpdateAnimAcross());
	}

	public void StartAnimDisplayDown(){
		coroutineHasStarted = true;
		tempY = 0;
		StartCoroutine (UpdateAnimDown());
	}


	public void EndAnimDisplay(){

	}

	IEnumerator UpdateAnimAcross(){

		while(patternIsAnimatingAcross){
			if(tempY>=tempMovingPattern.Count){
				tempY=0;
			}

			string tempPatternString = tempMovingPattern[tempY];
			for (int x=0; x<tempPatternString.Length; x++) {
				if (tempPatternString [x] == patternTest [1]) {
					GM_Script.blueIsActiveList [x] = true;
				} else if (tempPatternString [x] == patternTest [0]) {
					GM_Script.blueIsActiveList [x] = false;
				}
			}

			UpdateBingoCardSingleDisplay ();
			tempY++;
			yield return new WaitForSeconds (1.0f);
		}
	}

	IEnumerator UpdateAnimDown(){
		
		while(patternIsAnimatingDown){
			if(tempY>=tempMovingPattern.Count){
				tempY=0;
			}
			
			string tempPatternString = tempMovingPattern[tempY];
			for (int x=0; x<tempPatternString.Length; x++) {
				if (tempPatternString [x] == patternTest [1]) {
					GM_Script.blueIsActiveList [x] = true;
				} else if (tempPatternString [x] == patternTest [0]) {
					GM_Script.blueIsActiveList [x] = false;
				}
			}
			
			UpdateBingoCardSingleDisplay ();
			tempY++;
			yield return new WaitForSeconds (1.0f);
		}
	}

	public void UpdateBingoCardDisplay(List<bool> tempList){


		for(int x=0; x<children.Length; x++){
			if(tempList[x]==true && x!=12){
				//children[x].SetActive(true);
				children[x].GetComponent<SpriteRenderer>().enabled=true;
				children[x].GetComponent<BingoBlueButton>().isPressed = true;

			}else{
				//children[x].SetActive(false);
				children[x].GetComponent<SpriteRenderer>().enabled=false;
				children[x].GetComponent<BingoBlueButton>().isPressed = false;
			}
		}
	

	}

	public void ResetBingoCards(){
		for(int x=0; x<25; x++){
			GM_Script.blueIsActiveList[x] = resetBlueButtonActiveList[x];
			tempBlueButtonActiveList[x] = resetBlueButtonActiveList[x];
		}

		UpdateBingoCardDisplay (tempBlueButtonActiveList);
	
	}

//	IEnumerator StartMovingPattern(){

//	}
}
