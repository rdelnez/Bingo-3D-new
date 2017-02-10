using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BingoCardMini : MonoBehaviour {

	public GM GM_Script;
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
		GetChildren ();
		
		tempBlueButtonActiveList = new List<bool>();
		tempMovingPattern = new List<string>();
		patternIsAnimatingAcross = false;
		patternIsAnimatingDown = false;
		patternTest = "01";
		tempZ = 0;
		
		
	}


	void OnEnable(){
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM> ();
		if(GM_Script.coroutineHasStarted){
			if(GM_Script.patternIsAnimatingAcross){
				tempMovingPattern = GM_Script.pattern2LinesAcrossList;
				patternIsAnimatingAcross=true;
				patternIsAnimatingDown=false;
				StartAnimDisplayAcross();

			}else if(GM_Script.patternIsAnimatingDown){
				tempMovingPattern = GM_Script.pattern2LinesDownList;
				patternIsAnimatingAcross=false;
				patternIsAnimatingDown=true;
				StartAnimDisplayDown();
			}



		}
	}
	// Update is called once per frame
	void Update () {
		//UpdateBingoCardDisplay ();
		Debug.Log (tempZ);
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
		
		while(patternIsAnimatingAcross){
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
			tempZ++;
			yield return new WaitForSeconds (1.0f);
		}
	}
	
	IEnumerator UpdateAnimDownMini(){
		
		while(patternIsAnimatingDown){
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
			tempZ++;
			yield return new WaitForSeconds (1.0f);
		}
	}
	
	public void UpdateBingoCardDisplay(List<bool> tempList){
		
		
		for(int x=0; x<children.Length; x++){
			if(tempList[x]==true){
				children[x].SetActive(true);
			}else{
				children[x].SetActive(false);
			}
		}
		
		
	}
}
