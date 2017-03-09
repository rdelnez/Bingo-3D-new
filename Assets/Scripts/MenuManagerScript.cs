using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour {

	public string tempButtonName;
	public string patternTest;
	public SoundManagerScript SM_Script;
	public BingoCardBig BingoCardBigScript;
	public BingoCardMini BingoCardMiniScript;
	public GM GM_Script;
	public DisplayManagerScript DM_Script;
	public List<string> voicesString;
	public List<GameObject> tempPattern;
	public List<GameObject> tempVoices;
	public List<GameObject> tempAutoTumbler;
	public List<GameObject> tempAutoTumblerSpeed;
	public List<GameObject> tempTumbler;
	public List<GameObject> tempVolume;
	public Vector3 VolumeBigHexPos;	
	public GameObject volumeText;
	public GameObject gameCanvas;
	//public GameObject Canvas;
	//public GUIText VolumeText;

	// Use this for initialization

	void Awake(){

		
	}

	void Start () {


		//gameCanvas = new GameObject ("Canvas");
		 
		//Instantiate (volumeText);
		//volumeText.AddComponent <GUIText>();
		//VolumeText.SetActive(false);
		//Canvas = new GameObject ("Canvas");
		//Canvas gameCanvas = Canvas.AddComponent<Canvas>();
		//gameCanvas.renderMode = RenderMode.WorldSpace;
		//Instantiate (gameCanvas);
		// volumeText.transform.localPosition 

		//volumeText.transform.SetParent(
		//volumeText.
		//Canvas.AddComponent<Text> ();


		patternTest = "01";

		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();
		DM_Script = GameObject.FindGameObjectWithTag ("DisplayManager").GetComponent<DisplayManagerScript>();

		voicesString = new List<string> ();
		voicesString.Add ("jo");
		voicesString.Add ("pam");
		voicesString.Add ("ad");

		tempPattern = GM_Script.MenuButtonListPattern;
		tempVoices = GM_Script.MenuButtonListVoices;
		tempAutoTumbler = GM_Script.MenuButtonListAutoTumbler;
		tempAutoTumblerSpeed = GM_Script.MenuButtonListAutoTumblerSpeed;
		tempTumbler = GM_Script.MenuButtonListTumbler;
		tempVolume = GM_Script.MenuButtonListVolume;

		//START for initial pattern when starting the game
		ValidateMenuButtons (tempPattern, 4);
		tempButtonName = "pattern5";
		ConvertStringListToBool (GM_Script.patternStringList [4]);
		//END for initial pattern when starting the game

		//START for initial voice when starting the game
		ValidateMenuButtons (tempVoices, 0);
		tempButtonName = "voice1";
		//END for initial voic when starting the game

		//START for whether tumbler is enabled when starting the game
		ValidateMenuButtons (tempTumbler, 0);
		tempButtonName = "tumbler1";
		//END for whether tumbler is enabled when starting the game

		//START for whether auto tumbler is enabled when starting the game
		ValidateMenuButtons (tempAutoTumbler, 1);
		tempButtonName = "autotumbler2";
		//END for whether auto tumbler is enabled when starting the game

		//tempButtonName = "volume1";

	}
	
	// Update is called once per frame
	void Update () {

	}

	private void ValidateMenuButtons(List<GameObject> tempList, int tempIndex){
		for(int x=0; x<tempList.Count; x++){
			tempList[x].GetComponent<MenuButton>().isPressed=false;
			if(!tempList[x].GetComponent<MenuButton>().IsDisplay){
				tempList[x].GetComponent<MenuButton>().DePressButton();
			}
			if(x == tempIndex)
			{
				tempList[x].GetComponent<MenuButton>().isPressed=true;
				tempList[x].GetComponent<MenuButton>().PressButton();
			}
			else
			{
				if(tempList[x].GetComponent<MenuButton>().isPressed){
			//		tempList[x].GetComponent<MenuButton>().isPressed=false;
			//		tempList[x].GetComponent<MenuButton>().DePressButton();
				}
			}
		}
	}

	/* private void RefreshMenuButtons(List<GameObject> tempList, int tempIndex) {
		for(int x=0; x<tempList.Count; x++){
			tempList[x].GetComponent<MenuButton>().DePressButton();
		}
	} */

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

	public void ConvertStringListToBool(string tempPatternString){

		for (int x=0; x<tempPatternString.Length; x++) {
			if (tempPatternString [x] == patternTest [1]) {
				GM_Script.blueIsActiveList [x] = true;
			} else if (tempPatternString [x] == patternTest [0]) {
				GM_Script.blueIsActiveList [x] = false;
			}
		}
	}

	//START Change Pattern
	public void Pattern(){
		if(!BingoCardBigScript){
			BingoCardBigScript = GameObject.FindGameObjectWithTag ("BingoCardBig").GetComponent<BingoCardBig>();
		}

		int tempNum = (int)char.GetNumericValue (tempButtonName [tempButtonName.Length - 1]); //This is the last number on the pattern string i.e. Pattern1: where 1 is tempNum.

		if(tempButtonName == "Pattern9"){
			BingoCardBigScript.isCustomPattern=true;

			BingoCardBigScript.patternIsAnimatingAcross=false;
			BingoCardBigScript.patternIsAnimatingDown=false;
			GM_Script.patternIsAnimatingAcross=false;
			GM_Script.patternIsAnimatingDown=false;

			BingoCardBigScript.ResetBingoCards();



		}else if (tempButtonName != "Pattern1" && tempButtonName != "Pattern2") {	//This if is to check whether the pattern will animate or not. Pattern1 and Pattern2 needs to be animated

			BingoCardBigScript.isCustomPattern = false;

			//Debug.Log (tempButtonName [tempButtonName.Length - 1]-1);

			//Debug.Log (tempNum);
			//string tempPatternString = GM_Script.patternStringList [tempNum - 1];
			ConvertStringListToBool(GM_Script.patternStringList [tempNum - 1]);

			BingoCardBigScript.UpdateBingoCardSingleDisplay ();

			BingoCardBigScript.patternIsAnimatingAcross=false;
			BingoCardBigScript.patternIsAnimatingDown=false;
			GM_Script.patternIsAnimatingAcross=false;
			GM_Script.patternIsAnimatingDown=false;

		}else {
			BingoCardBigScript.isCustomPattern =false;

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

	public void Tumbler(){
		//GameObject tumblerReference = GameObject.FindGameObjectWithTag ("SpinButton");
		int tempNum = (int)char.GetNumericValue (tempButtonName [tempButtonName.Length - 1]); //This is the last number on the pattern string i.e. Pattern1: where 1 is tempNum.
		ValidateMenuButtons (tempTumbler, tempNum - 1);	//This is needed on all functions - this is to validate what is pressed, keep it pressed and unpress the others
		//GameObject.FindGameObjectWithTag ("SpinButton").GetComponent<BallManager>();

		if (GM_Script.TumblerIsEnabled == true) {
			GM_Script.TumblerIsEnabled = false;
			// GameObject.F
			//Debug.Log (tumblerReference);
			//tumblerReference.SetActive(false);
			//GM_Script.spinButtonPrefabs.SetActive(false);
		} else {
			GM_Script.TumblerIsEnabled = true;
			//Debug.Log (tumblerReference);
			//tumblerReference.SetActive(true);
			//GM_Script.spinButtonPrefabs.SetActive(true);
		}
	}

	public void AutoTumbler(){
		int tempNum = (int)char.GetNumericValue (tempButtonName [tempButtonName.Length - 1]); //This is the last number on the pattern string i.e. Pattern1: where 1 is tempNum.
		//ValidateMenuButtons (tempAutoTumbler, tempNum - 1);	//This is needed on all functions - this is to validate what is pressed, keep it pressed and unpress the others
		if(tempButtonName == "AutoTumbler1" || tempButtonName == "AutoTumbler2"){
			ValidateMenuButtons (tempAutoTumblerSpeed, tempNum - 1);	//This is needed on all functions - this is to validate what is pressed, keep it pressed and unpress the others
		}
	
	}

	public void Volume(){
		//(buttonName == "Volume1" || buttonName == "Volume2")
		int tempNum = (int)char.GetNumericValue (tempButtonName [tempButtonName.Length - 1]); //This is the last number on the pattern string i.e. Pattern1: where 1 is tempNum.
		Debug.Log (tempNum);

		if (tempNum == 1) {
			SM_Script.IncreaseVolume ();
		} else {
			SM_Script.DecreaseVolume ();
		}

		//VolumeBigHexPos = GM_Script.hexListBig[4].GetComponent<HexBig> ().menuPosBig;
		//volumeText.transform.position = VolumeBigHexPos;


		//volumeText.transform.position = VolumeBigHexPos;

		//Debug.Log (VolumeBigHexPos);

		//RefreshMenuButtons (tempVolume, tempNum - 1);

		//Debug.Log (tempVolume [tempNum].GetComponent<MenuButton>().buttonName);
		//ValidateMenuButtons (tempVolume, tempNum);
		//tempVolume [tempNum].GetComponent<MenuButton> ().PressButton ();

		//ValidateMenuButtons (tempVolume, tempNum);
		//tempVolume;

		VolumeSubTask ();
	}

	public void VolumeSubTask(){
		float volumeLevel = Mathf.Round(SM_Script.FX_Player.volume * 100);
		DM_Script.DisplayImageNum (GameObject.FindGameObjectWithTag("VolumeDisplay").transform.GetChild(0).gameObject, GameObject.FindGameObjectWithTag("VolumeDisplay").transform.GetChild(1).gameObject, (int)volumeLevel);

	}
	/* IEnumerator TumblerIsActive() {
		while(GM_Script.TumblerIsEnabled == false){
			
			yield return new WaitForSeconds(0.5f);
			//GM_Script.BingoTumbler.SetActive = false;
			GM_Script.BingoTumbler.GetComponent<SpriteRenderer>().sprite = null;
		}
		
	} */

}
