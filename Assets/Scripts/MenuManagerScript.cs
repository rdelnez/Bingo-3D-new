using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManagerScript : MonoBehaviour {

	public string tempButtonName;
	public string patternTest;
	public SoundManagerScript SM_Script;
	public BingoCardBig BingoCardBigScript;
	public BingoCardMini BingoCardMiniScript;
	public GM GM_Script;
	public List<string> voicesString;
	public List<GameObject> tempPattern;
	public List<GameObject> tempVoices;
	public List<GameObject> tempAutoTumbler;
	public List<GameObject> tempTumbler;
	public List<GameObject> tempVolume;
	// Use this for initialization
	void Start () {
		patternTest = "01";

		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();

		voicesString = new List<string> ();
		voicesString.Add ("jo");
		voicesString.Add ("pam");
		voicesString.Add ("ad");

		tempPattern = GM_Script.MenuButtonListPattern;
		tempVoices = GM_Script.MenuButtonListVoices;
		tempAutoTumbler = GM_Script.MenuButtonListAutoTumbler;
		tempTumbler = GM_Script.MenuButtonListTumbler;
		tempVolume = GM_Script.MenuButtonListVolume;
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void ValidateMenuButtons(List<GameObject> tempList, int tempIndex){
		for(int x=0; x<tempList.Count; x++){
			if(x == tempIndex)
			{
				tempList[x].GetComponent<MenuButton>().isPressed=true;
			}
			else
			{
				if(tempList[x].GetComponent<MenuButton>().isPressed){
					tempList[x].GetComponent<MenuButton>().isPressed=false;
					tempList[x].GetComponent<MenuButton>().DePressButton();
				}
			}
		}
	}

	public void CallFunction(){
		// value = value.Substring(0, value.Length - 1);

		Debug.Log (tempButtonName.Substring (0, tempButtonName.Length - 1));
		Invoke (tempButtonName.Substring(0, tempButtonName.Length - 1),0.0f);
	}


	//START Change Active Voices
	public void Voices(){
		//Debug.Log (tempButtonName [tempButtonName.Length - 1]-1);
		int tempNum = (int)char.GetNumericValue(tempButtonName [tempButtonName.Length - 1]);
		//Debug.Log (tempNum);
		SM_Script.setNum = tempNum;	//This is a parameter for Resource.Load on the sound file
		SM_Script.fileName = voicesString [tempNum-1]; //This is to change the soundfile to be played in SoundManager
		ValidateMenuButtons(tempVoices, tempNum-1);	//This is needed on all functions - this is to validate what is pressed, keep it pressed and unpress the others
	}
	//END Change Active Voices

	//START Change Pattern
	public void Pattern(){
		if(!BingoCardBigScript){
			BingoCardBigScript = GameObject.FindGameObjectWithTag ("BingoCardBig").GetComponent<BingoCardBig>();
		}

		int tempNum = (int)char.GetNumericValue (tempButtonName [tempButtonName.Length - 1]); //This is the last number on the pattern string i.e. Pattern1: where 1 is tempNum.

		if (tempButtonName != "Pattern1" && tempButtonName != "Pattern2") {	//This if is to check whether the pattern will animate or not. Pattern1 and Pattern2 needs to be animated
			//Debug.Log (tempButtonName [tempButtonName.Length - 1]-1);

			//Debug.Log (tempNum);
			string tempPatternString = GM_Script.patternStringList [tempNum - 1];
			for (int x=0; x<tempPatternString.Length; x++) {
				if (tempPatternString [x] == patternTest [1]) {
					GM_Script.blueIsActiveList [x] = true;
				} else if (tempPatternString [x] == patternTest [0]) {
					GM_Script.blueIsActiveList [x] = false;
				}
			}

			BingoCardBigScript.UpdateBingoCardSingleDisplay ();

			BingoCardBigScript.patternIsAnimatingAcross=false;
			BingoCardBigScript.patternIsAnimatingDown=false;

		} else {
			//Debug.Log ("This is Pattern1 or Pattern2");
			if(tempNum == 1){
				BingoCardBigScript.tempMovingPattern = GM_Script.pattern2LinesAcrossList;
				BingoCardBigScript.patternIsAnimatingAcross=true;
				GM_Script.patternIsAnimatingAcross=true;		//For GM and BingoCardMini
				BingoCardBigScript.patternIsAnimatingDown=false;
				GM_Script.patternIsAnimatingDown=false;			//For GM and BingoCardMini
				GM_Script.coroutineHasStarted=true;				//For GM and BingoCardMini
				GM_Script.activePatternName=tempButtonName;		//For GM and BingoCardMini
				BingoCardBigScript.StartAnimDisplayAcross();

			}else if(tempNum == 2){
				BingoCardBigScript.tempMovingPattern = GM_Script.pattern2LinesDownList;
				BingoCardBigScript.patternIsAnimatingDown=true;
				GM_Script.patternIsAnimatingDown=true;			//For GM and BingoCardMini
				BingoCardBigScript.patternIsAnimatingAcross=false;
				GM_Script.patternIsAnimatingAcross=false;		//For GM and BingoCardMini
				GM_Script.coroutineHasStarted=true;				//For GM and BingoCardMini
				GM_Script.activePatternName=tempButtonName;		//For GM and BingoCardMini
				BingoCardBigScript.StartAnimDisplayDown();
			}



		}

		ValidateMenuButtons (tempPattern, tempNum - 1);	//This is needed on all functions - this is to validate what is pressed, keep it pressed and unpress the others
	}
	//END Change Pattern

}
