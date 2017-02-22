using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BingoCardMini : MonoBehaviour {

	public GM GM_Script;
	public MenuManagerScript MM_Script;
	public GameObject[] children;
	public List<bool> tempBlueButtonActiveList;
	public List<string> tempMovingPattern;
	public bool patternIsAnimatingAcross;
	public bool patternIsAnimatingDown;
	public string patternTest;
	public int tempZ;
	public bool coroutineHasStarted = false;
	// Use this for initialization
	void Start () {
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM> ();
		MM_Script = GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<MenuManagerScript> ();

		GetChildren ();
		
		tempBlueButtonActiveList = new List<bool>();


		tempMovingPattern = new List<string>();
		patternIsAnimatingAcross = false;
		patternIsAnimatingDown = false;
		patternTest = "01";
		tempZ = 0;

		InitializeBlueButtons ();
		
	}

	void InitializeBlueButtons(){
		for(int x=0; x<25;x++){
			tempBlueButtonActiveList.Add (false);
		}
		//GM_Script.blueIsActiveList = tempBlueButtonActiveList;
		//MM_Script.ConvertStringListToBool (GM_Script.patternStringList [4]);
		UpdateBingoCardSingleDisplay();
	}

	void OnEnable(){
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM> ();
		if(GM_Script.coroutineHasStarted){
			if(GM_Script.patternIsAnimatingAcross){
				tempMovingPattern = GM_Script.pattern2LinesAcrossList;
				patternIsAnimatingAcross=true;
				patternIsAnimatingDown=false;
				//StartAnimDisplayAcross();

			}else if(GM_Script.patternIsAnimatingDown){
				tempMovingPattern = GM_Script.pattern2LinesDownList;
				patternIsAnimatingAcross=false;
				patternIsAnimatingDown=true;
				//StartAnimDisplayDown();
			}
			else{
				patternIsAnimatingAcross=false;
				patternIsAnimatingDown=false;

			}



		}

	}
	// Update is called once per frame
	void Update () {
		//UpdateBingoCardDisplay ();
		//Debug.Log (tempZ);
	}
	
	//Start this is for getting the children and store it in a list
	public void GetChildren(){
		
		children = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
		}
		
	}
	//END this is for getting the children and store it in a list
	
	public void UpdateBingoCardSingleDisplay(){
		tempBlueButtonActiveList = GM_Script.blueIsActiveList;
		UpdateBingoCardDisplay (tempBlueButtonActiveList);
	}
	
	public void StartAnimDisplayAcross(){
		coroutineHasStarted = true;
		tempZ = 0;
		StartCoroutine (UpdateAnimAcrossMini());
	}
	
	public void StartAnimDisplayDown(){
		coroutineHasStarted = true;
		tempZ = 0;
		StartCoroutine (UpdateAnimDownMini());
	}
	
	
	public void EndAnimDisplay(){
		
	}
	
	IEnumerator UpdateAnimAcrossMini(){
		
		while(patternIsAnimatingAcross && !patternIsAnimatingDown){
			if(tempZ>=tempMovingPattern.Count){
				tempZ=0;
			}
			
			string tempPatternString = tempMovingPattern[tempZ];
			for (int x=0; x<tempPatternString.Length; x++) {
				if (tempPatternString [x] == patternTest [1]) {
					GM_Script.blueIsActiveList [x] = true;
				} else if (tempPatternString [x] == patternTest [0]) {
					GM_Script.blueIsActiveList [x] = false;
				}
			}
			
			UpdateBingoCardSingleDisplay ();
			//tempZ++;
			tempZ=tempZ+1;
			Debug.Log (tempZ);
			yield return new WaitForSeconds (1.0f);
		}
	}
	
	IEnumerator UpdateAnimDownMini(){
		
		while(patternIsAnimatingDown && !patternIsAnimatingAcross){
			if(tempZ>=tempMovingPattern.Count){
				tempZ=0;
			}
			
			string tempPatternString = tempMovingPattern[tempZ];
			for (int x=0; x<tempPatternString.Length; x++) {
				if (tempPatternString [x] == patternTest [1]) {
					GM_Script.blueIsActiveList [x] = true;
				} else if (tempPatternString [x] == patternTest [0]) {
					GM_Script.blueIsActiveList [x] = false;
				}
			}
			
			UpdateBingoCardSingleDisplay ();
			//tempZ++;
			tempZ=tempZ+1;
			yield return new WaitForSeconds (1.0f);
		}
	}
	
	public void UpdateBingoCardDisplay(List<bool> tempList){
		
		
		for(int x=0; x<children.Length; x++){
			if(tempList[x]==true && x!=12){
				children[x].SetActive(true);
			}else{
				children[x].SetActive(false);
			}
		}
		
		
	}
}
