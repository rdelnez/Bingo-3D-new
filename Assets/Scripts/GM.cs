using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GM : MonoBehaviour {

	public MenuManagerScript MM_Script;

	public GameObject hexPrefabs;
	public GameObject hexPrefabsBig;
	public Object hexPrefabsLoaded;
	public Object hexPrefabsLoadedBig;
	public Object displayMenuButton;

	//Start This is for connecting Bingo Card and GM
	public List<bool> blueIsActiveList;
	public List<string> patternStringList;
	public List<string> pattern2LinesAcrossList;
	public List<string> pattern2LinesDownList;

	public bool patternIsAnimatingAcross;
	public bool patternIsAnimatingDown;
	public string activePatternName;
	public bool coroutineHasStarted = false;

	//END This is for connecting Bingo Card and GM

	public bool menuLock;

	//Start This if for BingoCardMini

	public BingoCardMini BingoCardMiniScript;
	public BingoCardBig BingoCardBigScript;

	//END This if for BingoCardMini


	//END Ball3D


	//Start for Menu Button
	public Object MenuButton;		//for small menu buttons
	public GameObject menuButtonPrefabs;
	public int tempIndexOfMenuButton;
	public MenuButton tempMenuButtonScript;

	public GameObject BingoCard;		//for other menu buttons
	public GameObject BingoClose;		//for other menu buttons
	public GameObject BingoReset;		//for other menu buttons
	public GameObject BingoTumbler;		//for other menu buttons
	public GameObject LittleMenu;		//for other menu buttons

		
	public GameObject spinButtonPrefabs;	//for game buttons
	public SpinScript tempSpinButtonScript;

	public GameObject winButtonPrefabs;	//for game buttons
	public SpinScript tempWinButtonScript;

	public GameObject bingoCardMiniPrefabs;	//for game buttons
	public SpinScript tempbingoCardMiniScript;

	//Start Recall Numbers
	public List<GameObject> RecallNumberObjectList = new List<GameObject>();	// For Recal numbers
	public RecallNumbersScript recallNumbersScript;

	public GameObject recallTextPrefab;

	public GameObject recallNumberPrefabs0;	//for game buttons
	public SpinScript temprecallNumberScript0;

	public GameObject recallNumberPrefabs1;	//for game buttons
	public SpinScript temprecallNumberScript1;

	public GameObject recallNumberPrefabs2;	//for game buttons
	public SpinScript temprecallNumberScript2;

	public GameObject recallNumberPrefabs3;	//for game buttons
	public SpinScript temprecallNumberScript3;
	//END Recall Numbers

	// Tumbler
	public bool TumblerIsEnabled;


	//Start MENU LEtters
	public List<GameObject> MenuLettertList = new List<GameObject>();	// For MENU Letters
	public GameObject menuLetterPrefabs;

	//END MENU LEtters
	
	public List<Vector3> MenuButtonListMainPos = new List<Vector3>();
	//public List<Vector3> MenuButtonListSub = new List<Vector3>();
	public List<Vector3> MenuButtonListPatternPos = new List<Vector3>();
	public List<Vector3> MenuButtonListVoicesPos = new List<Vector3>();
	public List<Vector3> MenuButtonListAutoTumblerPos = new List<Vector3>();
	public List<Vector3> MenuButtonListTumblerPos = new List<Vector3>();
	public List<Vector3> MenuButtonListVolumePos = new List<Vector3>();


	public List<GameObject> MenuButtonList = new List<GameObject>();
	public List<string> MenuButtonName = new List<string>();
	public List<string> MenuButtonSpriteName = new List<string>();

	public List<GameObject> MenuButtonListPattern = new List<GameObject>();
	public List<string> MenuButtonPatternSpriteName = new List<string>();
	public List<string> MenuButtonPattern1SpriteName = new List<string>();

	public List<GameObject> MenuButtonListVoices = new List<GameObject>();
	public List<string> MenuButtonVoicesSpriteName = new List<string>();
	public List<string> MenuButtonVoices1SpriteName = new List<string>();

	public List<GameObject> MenuButtonListAutoTumbler = new List<GameObject>();
	public List<GameObject> MenuButtonListAutoTumblerSpeed = new List<GameObject>();
	public List<string> MenuButtonAutoTumblerSpriteName = new List<string>();
	public List<string> MenuButtonAutoTumbler1SpriteName = new List<string>();

	public List<GameObject> MenuButtonListTumbler = new List<GameObject>();
	public List<string> MenuButtonTumblerSpriteName = new List<string>();
	public List<string> MenuButtonTumbler1SpriteName = new List<string>();

	public List<GameObject> MenuButtonListVolume = new List<GameObject>();
	public List<string> MenuButtonVolumeSpriteName = new List<string>();
	public List<string> MenuButtonVolume1SpriteName = new List<string>();




	//END for Menu Button

	public Vector3 startingPos;
	public Vector3 startingPosBig;
	public Vector3 startingPosChanging;
	public Vector3 startingPosInside;
	public GameObject startingPosHex;
	public GameObject startingPosBigHex;
	public bool addX;
	public float xValue;
	public int reverseLoop;
	public bool isMenuMoving;

	public bool toggleGameMenu;
	public Hex tempHex;
	public HexBig tempHexBig;


	//public List<List<GameObject>> hexList= new List<List<GameObject>>();

	public List<List<GameObject>> hexList = new List<List<GameObject>>();
	public List<GameObject> hexListBig = new List<GameObject>();

	public List<float> indexYPos = new List<float> ();
	public List<Vector3> indexXPosBackTitle = new List<Vector3> ();

	public float yPosChange;

	//public Vector3 VolumeBigHexPos; // Position for Volume Hex on Menu

	// Use this for initialization

	void Awake(){
		Time.timeScale = 1.5f;
		
		
		InitialisedVariables();
		InitialisedObjects ();
	}

	void Start () {

	







	
	}
	
	// Update is called once per frame
	void Update () {
	/*--

		//if (Input.GetKeyDown ("space") && isMenuMoving == false) {
		if (Input.GetButtonDown ("Fire1") && isMenuMoving == false) {

			if (toggleGameMenu) {
				MoveToMenu ();
				toggleGameMenu = false;
			} else {
				MoveToGame ();
				toggleGameMenu = true;
				StartCoroutine(DisableLittleMenu());
			}


			StartCoroutine(CheckDisable ());

		}
--*/

	}

	public void GoToMenu(){
		if(isMenuMoving==false){
			MoveToMenu ();
			StartCoroutine(CheckDisable (1.3f));

		}
	}


	public void GoToGame(){
		if (isMenuMoving == false) {
			MoveToGame ();
			StartCoroutine (DisableLittleMenu ());
			StartCoroutine (CheckDisable (1.3f));
		}

	}

	public void SpinTumbler(){

		BingoTumbler.GetComponent<Animator> ().Play ("Tumbling");
		Debug.Log ("Tumbling");

	}



	IEnumerator DisableLittleMenu(){
		yield return new WaitForSeconds(1.3f);
		LittleMenu.SetActive (true); // set BingoCard Active
	}

	IEnumerator CheckDisable(float tempTime){

			
		yield return new WaitForSeconds(tempTime);
		isMenuMoving = false;

	}

	void InitialisedVariables(){

		menuLock = false;

		// Tumbler is On
		TumblerIsEnabled = true;

		//Start this is for Bingo Card Blue Button Active List //START This is the default pattern to be displayed
		blueIsActiveList = new List<bool> ();
		//for(int x=0; x<25; x++){
		blueIsActiveList.Add (true);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (true);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (true);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (false);
		blueIsActiveList.Add (true);
		//}
		//END this is for Bingo Card Blue Button Active List //END This is the default pattern to be displayed

		//Start Pattern List String
		patternStringList = new List<string>();
		patternStringList.Add ("2linesacross");
		patternStringList.Add ("2linesdown");
		patternStringList.Add ("1111110001100011000111111"); // Square
		patternStringList.Add ("0010000100111110010000100"); // + 
		patternStringList.Add ("1000100000000000000010001"); // 4 Corners
		patternStringList.Add ("1000101010001000101010001"); // X Cross
		patternStringList.Add ("1000110011101011100110001"); // Z
		patternStringList.Add ("1111111111111111111111111"); // All Filled

		pattern2LinesDownList = new List<string>();
		pattern2LinesDownList.Add ("1111111111000000000000000");
		pattern2LinesDownList.Add ("1111100000111110000000000");
		pattern2LinesDownList.Add ("1111100000000001111100000");
		pattern2LinesDownList.Add ("1111100000000000000011111");
		pattern2LinesDownList.Add ("0000011111111110000000000");
		pattern2LinesDownList.Add ("0000011111000001111100000");
		pattern2LinesDownList.Add ("0000011111000000000011111");
		pattern2LinesDownList.Add ("0000000000111111111100000");
		pattern2LinesDownList.Add ("0000000000111110000011111");
		pattern2LinesDownList.Add ("0000000000000001111111111");

		pattern2LinesAcrossList = new List<string>();
		pattern2LinesAcrossList.Add ("1100011000110001100011000");
		pattern2LinesAcrossList.Add ("1010010100101001010010100");
		pattern2LinesAcrossList.Add ("1001010010100101001010010");
		pattern2LinesAcrossList.Add ("1000110001100011000110001");
		pattern2LinesAcrossList.Add ("0110001100011000110001100");
		pattern2LinesAcrossList.Add ("0101001010010100101001010");
		pattern2LinesAcrossList.Add ("0100101001010010100101001");
		pattern2LinesAcrossList.Add ("0011000110001100011000110");
		pattern2LinesAcrossList.Add ("0010100101001010010100101");
		pattern2LinesAcrossList.Add ("0001100011000110001100011");
		//END Pattern List String

		isMenuMoving = false;
		toggleGameMenu = true;

		yPosChange = 3;
		startingPos = startingPosHex.transform.localPosition;
		startingPosBig = startingPos;
		startingPosChanging = startingPos;
		addX = false;

		indexYPos.Add (11);
		indexYPos.Add (11);
		indexYPos.Add (11);
		indexYPos.Add (11);
		indexYPos.Add (11);
		//indexYPos.Add (8.5f);

		reverseLoop = 13;

		indexXPosBackTitle.Add (new Vector3(23.9f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(22.8f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(21.7f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(20.6f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(19.5f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(18.4f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(17.3f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(16.2f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(15.1f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(14.0f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(12.9f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(11.8f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(10.7f, 6, 0));
		indexXPosBackTitle.Add (new Vector3(9.6f, 6, 0));


		//Start for Menu Button

		tempIndexOfMenuButton = 0;

		MenuButtonListMainPos.Add (new Vector3(-4.6f,5.9f,0));	//MenuButtonMain
		MenuButtonListMainPos.Add (new Vector3(2.25f,5.9f,0));
		MenuButtonListMainPos.Add (new Vector3(9.09f,5.9f,0));
		MenuButtonListMainPos.Add (new Vector3(15.95f,5.9f,0));
		MenuButtonListMainPos.Add (new Vector3(22.8f,5.9f,0));

		MenuButtonName.Add ("Pattern");							//MenuButtonName Name of Big Hex
		MenuButtonName.Add ("Voices");	
		MenuButtonName.Add ("AutoTumbler");	
		MenuButtonName.Add ("Tumbler");	
		MenuButtonName.Add ("Volume");	

		MenuButtonSpriteName.Add ("PatternBigHex");	
		MenuButtonSpriteName.Add ("VoicesBigHex");	
		MenuButtonSpriteName.Add ("AutoTumblerBigHex");	
		MenuButtonSpriteName.Add ("TumblerBigHex");	
		MenuButtonSpriteName.Add ("VolumeBigHex");	


		MenuButtonListPatternPos.Add (new Vector3(-6.08f,4f,0));	//Pattern
		MenuButtonListPatternPos.Add (new Vector3(-3.15f,4f,0));
		MenuButtonListPatternPos.Add (new Vector3(-4.6f,1.51f,0));		
		MenuButtonListPatternPos.Add (new Vector3(-6.08f,-1.0f,0));
		MenuButtonListPatternPos.Add (new Vector3(-3.15f,-1.0f,0));
		MenuButtonListPatternPos.Add (new Vector3(-4.6f,-3.52f,0));
		MenuButtonListPatternPos.Add (new Vector3(-6.08f,-6.07f,0));
		MenuButtonListPatternPos.Add (new Vector3(-3.15f,-6.07f,0));
		MenuButtonListPatternPos.Add (new Vector3(-4.6f,-8.62f,0));

		MenuButtonPatternSpriteName.Add ("2linesacrosspattern1"); //Pattern Sprites Name
		MenuButtonPatternSpriteName.Add ("2linesdownpattern1");
		MenuButtonPatternSpriteName.Add ("borderpattern1");
		MenuButtonPatternSpriteName.Add ("crosspattern1");
		MenuButtonPatternSpriteName.Add ("fourcornerspattern1");
		MenuButtonPatternSpriteName.Add ("xpattern1");
		MenuButtonPatternSpriteName.Add ("zpattern1");
		MenuButtonPatternSpriteName.Add ("fullpattern1");
		MenuButtonPatternSpriteName.Add ("custompattern1");// temp pattern for custom pattern

		MenuButtonPattern1SpriteName.Add ("2linesacrosspattern"); //Pattern Sprites Name
		MenuButtonPattern1SpriteName.Add ("2linesdownpattern");
		MenuButtonPattern1SpriteName.Add ("borderpattern");
		MenuButtonPattern1SpriteName.Add ("crosspattern");
		MenuButtonPattern1SpriteName.Add ("fourcornerspattern");
		MenuButtonPattern1SpriteName.Add ("xpattern");
		MenuButtonPattern1SpriteName.Add ("zpattern");
		MenuButtonPattern1SpriteName.Add ("fullpattern");
		MenuButtonPattern1SpriteName.Add ("custompattern");// temp pattern for custom pattern

		////////////////////////////////////////////////////////


		MenuButtonListVoicesPos.Add (new Vector3(0.79f,4f,0));	//Voices
		MenuButtonListVoicesPos.Add (new Vector3(3.72f,4f,0));
		MenuButtonListVoicesPos.Add (new Vector3(2.27f,1.51f,0));

		MenuButtonVoicesSpriteName.Add ("female1");
		MenuButtonVoicesSpriteName.Add ("female2");
		MenuButtonVoicesSpriteName.Add ("male");

		MenuButtonVoices1SpriteName.Add ("female1pressed");
		MenuButtonVoices1SpriteName.Add ("female2pressed");
		MenuButtonVoices1SpriteName.Add ("malepressed");

		//////////////////////////////////////////////////

		MenuButtonListAutoTumblerPos.Add (new Vector3(7.67f,4f,0));	//Auto Tumbler
		MenuButtonListAutoTumblerPos.Add (new Vector3(10.6f,4f,0));
		MenuButtonListAutoTumblerPos.Add (new Vector3(9.15f,1.51f,0));
		MenuButtonListAutoTumblerPos.Add (new Vector3(7.67f,-1.0f,0));
		MenuButtonListAutoTumblerPos.Add (new Vector3(10.6f,-1.0f,0));

		MenuButtonAutoTumblerSpriteName.Add ("on");
		MenuButtonAutoTumblerSpriteName.Add ("off");
		MenuButtonAutoTumblerSpriteName.Add ("88");
		MenuButtonAutoTumblerSpriteName.Add ("plussign");
		MenuButtonAutoTumblerSpriteName.Add ("minussign");

		MenuButtonAutoTumbler1SpriteName.Add ("on1");
		MenuButtonAutoTumbler1SpriteName.Add ("off1");
		MenuButtonAutoTumbler1SpriteName.Add ("88_1");
		MenuButtonAutoTumbler1SpriteName.Add ("plussign1");
		MenuButtonAutoTumbler1SpriteName.Add ("minussign1");


		//////////////////////////////////////////////////



		MenuButtonListTumblerPos.Add (new Vector3(14.47f,4f,0));	//Tumbler
		MenuButtonListTumblerPos.Add (new Vector3(17.4f,4f,0));

		MenuButtonTumblerSpriteName.Add ("on");
		MenuButtonTumblerSpriteName.Add ("off");

		MenuButtonTumbler1SpriteName.Add ("on1");
		MenuButtonTumbler1SpriteName.Add ("off1");

		//////////////////////////////////////////////////

		MenuButtonListVolumePos.Add (new Vector3(21.34f,4f,0));	//Sound
		MenuButtonListVolumePos.Add (new Vector3(24.27f,4f,0));
		MenuButtonListVolumePos.Add (new Vector3(22.8f,1.51f,0));

		MenuButtonVolumeSpriteName.Add ("plussign");
		MenuButtonVolumeSpriteName.Add ("minussign");
		MenuButtonVolumeSpriteName.Add ("88");

		MenuButtonVolume1SpriteName.Add ("plussign1");
		MenuButtonVolume1SpriteName.Add ("minussign1");
		MenuButtonVolume1SpriteName.Add ("88_1");


		//END for Menu Button

		//Start for Menu Button

		
		//END for Menu Button

		hexPrefabsLoaded = Resources.Load ("Prefabs/hex");
		hexPrefabsLoadedBig = Resources.Load ("Prefabs/hexBig");
		MenuButton = Resources.Load ("Prefabs/menuButton");
		displayMenuButton = Resources.Load ("Prefabs/displayMenuButton");

	}

	void CheckPos(){
		if (addX == true) {
			xValue = 1.1f;
			addX = false;
		} else {
			xValue = -1.1f;
			addX = true;
		}

	}

	void InitialisedObjects(){


		InitializeSmallHex ();
		InitializeBigHex ();
		InitializeMenuButton ();
		InitializeOtherMenuItems ();
		InitializeOtherGameItems ();




		//	hexList.Add (new List<GameObject>());

			//hexPrefabs = Instantiate(Resources.Load("Prefabs/hex"), startingPos, Quaternion.identity) as GameObject;
		//	hexList[y].Add (hexPrefabs);

	}

	void InitializeSmallHex(){

		int tempHexIndex=1;

		for(int y=0; y<5; y++){
			
			
			//hexPrefabs = Instantiate(Resources.Load("Prefabs/hex"), startingPos, Quaternion.identity) as GameObject;
			hexPrefabs = Instantiate(hexPrefabsLoaded, startingPos, Quaternion.identity) as GameObject;
			
			hexList.Add (new List<GameObject>());
			
			hexPrefabs.GetComponent<Hex>().gamePos = hexPrefabs.transform.localPosition;
			hexPrefabs.GetComponent<Hex>().yPosTitle = indexYPos[y];
			hexList[y].Add (hexPrefabs);

			hexPrefabs.gameObject.tag="Hex"+tempHexIndex;
			
			
			CheckPos ();
			startingPos += new Vector3(xValue,-1.9f,0);

			tempHexIndex++;

			for(int x=0; x<14; x++){

				startingPosInside = hexPrefabs.transform.localPosition + new Vector3(2.2f,0,0);
				hexPrefabs = Instantiate(Resources.Load("Prefabs/hex"), startingPosInside, Quaternion.identity) as GameObject;
				hexPrefabs.GetComponent<Hex>().gamePos = hexPrefabs.transform.localPosition;
				
				hexPrefabs.GetComponent<Hex>().yPosTitle = indexYPos[y];
				hexPrefabs.GetComponent<Hex>().backTitleNumXPos += 0.5f; 
				hexPrefabs.gameObject.tag="Hex"+tempHexIndex;
				
				hexList[y].Add (hexPrefabs);

				tempHexIndex++;
				
				
				
			}
		}


	}

	void InitializeBigHex(){

		for(int x=0; x<5; x++){

			hexPrefabsBig = Instantiate(hexPrefabsLoadedBig, new Vector3(startingPosBig.x-4.8f,hexList[x][0].GetComponent<Hex>().gamePos.y,0), Quaternion.identity) as GameObject;
			//hexPrefabsBig = Instantiate(hexPrefabsLoadedBig, startingPosBig+ new Vector3(0,x*-1.2f,0), Quaternion.identity) as GameObject;
			hexPrefabsBig.transform.eulerAngles = new Vector3(90,0,90);
			hexPrefabsBig.GetComponent<HexBig>().bigRotateToGame = hexPrefabsBig.transform.eulerAngles;
			hexPrefabsBig.GetComponent<HexBig>().menuPosBig = new Vector3(startingPosBig.x+1.2f+(x*6.85f), startingPosBig.y+1.5f, 0); //working on it for now
			hexPrefabsBig.GetComponent<HexBig>().menuPosBigGame = hexPrefabsBig.transform.localPosition; //is the location when game is running
			hexPrefabsBig.GetComponent<HexBig>().menuButtonName = MenuButtonName[x];
			hexPrefabsBig.GetComponent<HexBig>().bigHexSpriteString = MenuButtonSpriteName[x];
			hexPrefabsBig.GetComponent<HexBig>().LoadSprite();
			hexListBig.Add (hexPrefabsBig);


				//transform.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, menuPos, Time.deltaTime*speedRotation, 3.0f);
		}

	}

	void InitializeMenuButton(){
		/*--
		for(int x=0;x<MenuButtonListSub.Count; x++ ){

			if(x<8){
				tempIndexOfMenuButton = 0;
			}
			else if(x<11){
				tempIndexOfMenuButton = 1;
			}
			else if(x<16){
				tempIndexOfMenuButton = 2;
			}
			else if(x<18){
				tempIndexOfMenuButton = 3;

			}
			else{
				tempIndexOfMenuButton = 4;
			}


			menuButtonPrefabs = Instantiate(MenuButton, MenuButtonListMain[tempIndexOfMenuButton], Quaternion.identity) as GameObject;
			menuButtonPrefabs.GetComponent<MenuButton>().origPos = menuButtonPrefabs.transform.localPosition;
			menuButtonPrefabs.GetComponent<MenuButton>().menuPos = MenuButtonListSub[x];
			MenuButtonList.Add (menuButtonPrefabs);

			menuButtonPrefabs.SetActive(false);
		}
		--*/

		//START Pattern Small Hex Menu Buttons
		for(int x=0; x<MenuButtonListPatternPos.Count; x++){
			menuButtonPrefabs = Instantiate(MenuButton, MenuButtonListMainPos[0], Quaternion.identity) as GameObject;
			menuButtonPrefabs.GetComponent<MenuButton>().origPos = menuButtonPrefabs.transform.localPosition;
			menuButtonPrefabs.GetComponent<MenuButton>().menuPos = MenuButtonListPatternPos[x];

			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite = MenuButtonPatternSpriteName[x];
			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite1 = MenuButtonPattern1SpriteName[x];

			menuButtonPrefabs.GetComponent<MenuButton>().LoadSprite();
			menuButtonPrefabs.GetComponent<MenuButton>().buttonName = "Pattern"+(x+1);	//this is for naming the buttons for Invoke


			MenuButtonListPattern.Add (menuButtonPrefabs);

			menuButtonPrefabs.SetActive(false);
		}
		////END pattern Small Hex Menu Buttons

		//START Voices Small Hex Menu Buttons
		for(int x=0; x<MenuButtonListVoicesPos.Count; x++){
			menuButtonPrefabs = Instantiate(MenuButton, MenuButtonListMainPos[1], Quaternion.identity) as GameObject;
			menuButtonPrefabs.GetComponent<MenuButton>().origPos = menuButtonPrefabs.transform.localPosition;
			menuButtonPrefabs.GetComponent<MenuButton>().menuPos = MenuButtonListVoicesPos[x];

			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite = MenuButtonVoicesSpriteName[x];
			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite1 = MenuButtonVoices1SpriteName[x];

			menuButtonPrefabs.GetComponent<MenuButton>().LoadSprite();
			menuButtonPrefabs.GetComponent<MenuButton>().buttonName = "Voices"+(x+1);	//this is for naming the buttons for Invoke

			MenuButtonListVoices.Add (menuButtonPrefabs);
			
			menuButtonPrefabs.SetActive(false);
		}
		//END Voices Small Hex Menu Buttons

		//START AutoTumbler Small Hex Menu Buttons
		for(int x=0; x<MenuButtonListAutoTumblerPos.Count; x++){

			if(3==x+1){
				menuButtonPrefabs = Instantiate(displayMenuButton, MenuButtonListMainPos[2], Quaternion.identity) as GameObject;
				menuButtonPrefabs.gameObject.tag = "AutoTumblerDisplay";
			}
			else{
				menuButtonPrefabs = Instantiate(MenuButton, MenuButtonListMainPos[2], Quaternion.identity) as GameObject;
			}
			menuButtonPrefabs.GetComponent<MenuButton>().origPos = menuButtonPrefabs.transform.localPosition;
			menuButtonPrefabs.GetComponent<MenuButton>().menuPos = MenuButtonListAutoTumblerPos[x];
			
	
			menuButtonPrefabs.GetComponent<MenuButton>().buttonName = "AutoTumbler"+(x+1);	//this is for naming the buttons for Invoke

			if(menuButtonPrefabs.GetComponent<MenuButton>().buttonName == "AutoTumbler3"){
				menuButtonPrefabs.GetComponent<MenuButton>().IsDisplay = true;
			}
			else{
				menuButtonPrefabs.GetComponent<MenuButton>().IsDisplay = false;


				menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite1 = MenuButtonAutoTumbler1SpriteName[x];


			}
			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite = MenuButtonAutoTumblerSpriteName[x];
			menuButtonPrefabs.GetComponent<MenuButton>().LoadSprite();
			MenuButtonListAutoTumbler.Add (menuButtonPrefabs); //new
		
			if(menuButtonPrefabs.GetComponent<MenuButton>().buttonName == "AutoTumbler1" || menuButtonPrefabs.GetComponent<MenuButton>().buttonName == "AutoTumbler2"){
				MenuButtonListAutoTumblerSpeed.Add (menuButtonPrefabs);	//new
			}

			
			menuButtonPrefabs.SetActive(false);
		}
		//END AutoTumbler Small Hex Menu Buttons

		//START Tumbler Small Hex Menu Buttons
		for(int x=0; x<MenuButtonListTumblerPos.Count; x++){
			menuButtonPrefabs = Instantiate(MenuButton, MenuButtonListMainPos[3], Quaternion.identity) as GameObject;
			menuButtonPrefabs.GetComponent<MenuButton>().origPos = menuButtonPrefabs.transform.localPosition;
			menuButtonPrefabs.GetComponent<MenuButton>().menuPos = MenuButtonListTumblerPos[x];

			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite = MenuButtonTumblerSpriteName[x];
			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite1 = MenuButtonTumbler1SpriteName[x];
			
			menuButtonPrefabs.GetComponent<MenuButton>().LoadSprite();
			menuButtonPrefabs.GetComponent<MenuButton>().buttonName = "Tumbler"+(x+1);	//this is for naming the buttons for Invoke

			MenuButtonListTumbler.Add (menuButtonPrefabs);
			
			menuButtonPrefabs.SetActive(false);
		}
		//END Tumbler Small Hex Menu Buttons


		//START Volume Small Hex Menu Buttons
		for(int x=0; x<MenuButtonListVolumePos.Count; x++){

			if(3==x+1){
				menuButtonPrefabs = Instantiate(displayMenuButton, MenuButtonListMainPos[4], Quaternion.identity) as GameObject;
				menuButtonPrefabs.gameObject.tag = "VolumeDisplay";
			}
			else{
				menuButtonPrefabs = Instantiate(MenuButton, MenuButtonListMainPos[4], Quaternion.identity) as GameObject;
			}
			menuButtonPrefabs.GetComponent<MenuButton>().origPos = menuButtonPrefabs.transform.localPosition;
			menuButtonPrefabs.GetComponent<MenuButton>().menuPos = MenuButtonListVolumePos[x];

			menuButtonPrefabs.GetComponent<MenuButton>().buttonName = "Volume"+(x+1);	//this is for naming the buttons for Invoke

			if(menuButtonPrefabs.GetComponent<MenuButton>().buttonName == "Volume3"){
				menuButtonPrefabs.GetComponent<MenuButton>().IsDisplay = true;

			}
			else{
				menuButtonPrefabs.GetComponent<MenuButton>().IsDisplay = false;
				
				
				menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite1 = MenuButtonVolume1SpriteName[x];
				
				
			}
			menuButtonPrefabs.GetComponent<MenuButton>().menuButtonSprite = MenuButtonVolumeSpriteName[x];
			menuButtonPrefabs.GetComponent<MenuButton>().LoadSprite();
			MenuButtonListAutoTumbler.Add (menuButtonPrefabs);
			

			menuButtonPrefabs.SetActive(false);
		}
		//END Volume Small Hex Menu Buttons


	}

	void InitializeOtherMenuItems(){
		BingoCard = Instantiate(Resources.Load("Prefabs/BingoCard"), new Vector3(19.52f, -5.3f, 0), Quaternion.identity) as GameObject;
		BingoCard.SetActive (false);

		BingoReset = Instantiate(Resources.Load("Prefabs/BingoReset"), new Vector3(25.84f, -5.36f, 0), Quaternion.identity) as GameObject;
		BingoReset.SetActive (false);

		BingoClose = Instantiate(Resources.Load("Prefabs/BingoClose"), new Vector3(25.84f, -8.64f, 0), Quaternion.identity) as GameObject;
		BingoClose.GetComponent<BingoClose> ().LoadAssets ();
		BingoClose.SetActive (false);

		BingoTumbler = Instantiate(Resources.Load("Prefabs/Tumbler"), new Vector3(-10.7f, 9.19f, 0), Quaternion.identity) as GameObject;
	//	BingoTumbler.SetActive (false);

		LittleMenu = Instantiate(Resources.Load("Prefabs/menulittlev2"), new Vector3(24f, 9.7f, 0), Quaternion.identity) as GameObject;
		LittleMenu.GetComponent<MenuTitle> ().LoadAssets ();

	}

	void InitializeOtherGameItems(){

		//Start Spin
		spinButtonPrefabs = Instantiate(Resources.Load("Prefabs/spinButton"), new Vector3(22.75f, -6.77f, 0), Quaternion.identity) as GameObject;

		spinButtonPrefabs.GetComponent<SpinScript>().ButtonSpriteString = "spiniconv1";
		spinButtonPrefabs.GetComponent<SpinScript>().ButtonSpriteString1 = "spiniconpressedv1";
		spinButtonPrefabs.GetComponent<SpinScript>().ButtonSpriteString2 = "autotumblerstart";
		spinButtonPrefabs.GetComponent<SpinScript>().ButtonSpriteString3 = "autotumblerstop";

		spinButtonPrefabs.GetComponent<SpinScript> ().LoadSprite ();
		//END Spin

		//Start Win
		winButtonPrefabs = Instantiate(Resources.Load("Prefabs/winButton"), new Vector3(15.83f, -6.77f, 0), Quaternion.identity) as GameObject;
		
		winButtonPrefabs.GetComponent<WinScript>().ButtonSpriteString = "winbutton";
		winButtonPrefabs.GetComponent<WinScript>().ButtonSpriteString1 = "winbuttonpressed";
		
		winButtonPrefabs.GetComponent<WinScript> ().LoadSprite ();
		//END Win


		//Start BingoCardMini
		bingoCardMiniPrefabs = Instantiate(Resources.Load("Prefabs/BingoCardMini"), new Vector3(8.8f, -6.77f, 0), Quaternion.identity) as GameObject;
		//END BingoCardMini


		//Start RecallNumbers
		recallTextPrefab = Instantiate(Resources.Load("Prefabs/recalltext"), new Vector3(-10.7f, -4.93f, 0), Quaternion.identity) as GameObject;

		recallNumberPrefabs0 = Instantiate(Resources.Load("Prefabs/recallballplacement"), new Vector3(-10.7f, -7.32f, 0), Quaternion.identity) as GameObject;
		recallNumberPrefabs0.transform.localScale = new Vector3 (0.5f, 0.5f,5);
		recallNumberPrefabs0.transform.GetChild(0).transform.localScale = new Vector3 (2.0f, 2.0f,1);
		recallNumberPrefabs0.transform.GetChild(0).transform.localPosition = new Vector3 (0.0f, 0.14f,0.5f);
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().indexNumber = 0;
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().value = 0;
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().isOccupied = false;
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().gamePos = recallNumberPrefabs0.transform.localPosition;
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().menuPos = recallNumberPrefabs0.transform.localPosition;
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().isToBeScaled = false;
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().gameScale = new Vector3(0.5f, 0.5f, 3);
		recallNumberPrefabs0.GetComponent<RecallNumbersScript> ().menuScale = new Vector3(0.5f, 0.5f, 3);


		RecallNumberObjectList.Add (recallNumberPrefabs0);


		recallNumberPrefabs1 = Instantiate(Resources.Load("Prefabs/recallballplacement"), new Vector3(-6.097f, -7.7f, 0), Quaternion.identity) as GameObject;
		recallNumberPrefabs1.transform.localScale = new Vector3 (0.4f, 0.4f,5);
		recallNumberPrefabs1.transform.GetChild(0).transform.localScale = new Vector3 (2.0f, 2.0f,1);
		recallNumberPrefabs1.transform.GetChild(0).transform.localPosition = new Vector3 (0.0f, 0.14f,0.5f);
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().indexNumber = 1;
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().value = 0;
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().isOccupied = false;
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().gamePos = recallNumberPrefabs1.transform.localPosition;
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().menuPos = recallNumberPrefabs0.transform.localPosition;
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().isToBeScaled = false;
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().gameScale = new Vector3(0.4f, 0.4f, 3);
		recallNumberPrefabs1.GetComponent<RecallNumbersScript> ().menuScale = new Vector3(0.4f, 0.4f, 3);
		
		RecallNumberObjectList.Add (recallNumberPrefabs1);

		
		recallNumberPrefabs2 = Instantiate(Resources.Load("Prefabs/recallballplacement"), new Vector3(-1.96f, -7.7f, 0), Quaternion.identity) as GameObject;
		recallNumberPrefabs2.transform.localScale = new Vector3 (0.4f, 0.4f,5);
		recallNumberPrefabs2.transform.GetChild(0).transform.localScale = new Vector3 (2.0f, 2.0f,1);
		recallNumberPrefabs2.transform.GetChild(0).transform.localPosition = new Vector3 (0.0f, 0.14f,0.5f);
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().indexNumber = 2;
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().value = 0;
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().isOccupied = false;
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().gamePos = recallNumberPrefabs2.transform.localPosition;
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().menuPos = recallNumberPrefabs0.transform.localPosition;
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().isToBeScaled = true;
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().gameScale = new Vector3(0.4f, 0.4f, 3);
		recallNumberPrefabs2.GetComponent<RecallNumbersScript> ().menuScale = new Vector3(0.3f, 0.3f, 3);
		
		RecallNumberObjectList.Add (recallNumberPrefabs2);

		
		recallNumberPrefabs3 = Instantiate(Resources.Load("Prefabs/recallballplacement"), new Vector3(2.08f, -7.7f, 0), Quaternion.identity) as GameObject;
		recallNumberPrefabs3.transform.localScale = new Vector3 (0.4f, 0.4f,5);
		recallNumberPrefabs3.transform.GetChild(0).transform.localScale = new Vector3 (2.0f, 2.0f,1);
		recallNumberPrefabs3.transform.GetChild(0).transform.localPosition = new Vector3 (0.0f, 0.14f,0.5f);
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().indexNumber = 3;
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().value = 0;
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().isOccupied = false;
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().gamePos = recallNumberPrefabs3.transform.localPosition;
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().menuPos = recallNumberPrefabs0.transform.localPosition;
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().isToBeScaled = true;
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().gameScale = new Vector3(0.4f, 0.4f, 3);
		recallNumberPrefabs3.GetComponent<RecallNumbersScript> ().menuScale = new Vector3(0.2f, 0.2f, 3);
		
		RecallNumberObjectList.Add (recallNumberPrefabs3);
		//END RecallNumbers

		//Start Menu Letters
		menuLetterPrefabs = Instantiate(Resources.Load("Prefabs/M"), new Vector3(-10.7f, 5.0f, 0), Quaternion.identity) as GameObject;
		MenuLettertList.Add (menuLetterPrefabs);
		menuLetterPrefabs.SetActive (false);

		menuLetterPrefabs = Instantiate(Resources.Load("Prefabs/E"), new Vector3(-10.7f, 2.5f, 0), Quaternion.identity) as GameObject;
		MenuLettertList.Add (menuLetterPrefabs);
		menuLetterPrefabs.SetActive (false);

		menuLetterPrefabs = Instantiate(Resources.Load("Prefabs/N"), new Vector3(-10.7f, 0.0f, 0), Quaternion.identity) as GameObject;
		MenuLettertList.Add (menuLetterPrefabs);
		menuLetterPrefabs.SetActive (false);

		menuLetterPrefabs = Instantiate(Resources.Load("Prefabs/U"), new Vector3(-10.7f, -2.5f, 0), Quaternion.identity) as GameObject;
		MenuLettertList.Add (menuLetterPrefabs);
		menuLetterPrefabs.SetActive (false);
		//END Menu Letters


	}

	void MoveToMenu(){
		isMenuMoving = true;

		LittleMenu.SetActive (false); // set BingoCard Active

		StartCoroutine(MoveSmallHexToMenu ());
		StartCoroutine(MoveBigHexToMenu());
		//StartCoroutine (MoveMenuButtonToMenu("Pattern"));
		StartCoroutine(MoveRecallNumbersToMenu());


		StartCoroutine(DisableStaticGameItem ());


	}

	IEnumerator DisableStaticGameItem(){
		spinButtonPrefabs.SetActive (false);
		winButtonPrefabs.SetActive (false);
		bingoCardMiniPrefabs.SetActive (false);
		recallTextPrefab.SetActive (false);

		//Start This is for Recall Numbers
		for(int x=0; x<4; x++){
			RecallNumberObjectList[x].transform.GetChild(0).gameObject.SetActive(false);	
			
		}
		//END This is for Recall Numbers



		for(int x=0; x<MenuLettertList.Count; x++){
			MenuLettertList [x].SetActive (true);
			yield return new WaitForSeconds (0.25f);
		}


	}

	
	void MoveToGame(){
		
		isMenuMoving = true;
		
		StartCoroutine(MoveMenuButtonToGame ());
		
		StartCoroutine (MoveSmallHexToGame ());
		StartCoroutine (MoveBigHexToGame());

		StartCoroutine(MoveRecallNumbersToGame());

		StartCoroutine(DisableStaticMenuItem ());



	}

	IEnumerator DisableStaticMenuItem(){



		for(int x=0; x<MenuLettertList.Count; x++){
			MenuLettertList [x].SetActive (false);
			yield return new WaitForSeconds (0.3f);
		}

		//yield return new WaitForSeconds (1.3f);
		if (TumblerIsEnabled) {
			spinButtonPrefabs.SetActive (true);
		}
		winButtonPrefabs.SetActive (true);
		bingoCardMiniPrefabs.SetActive (true);
		bingoCardMiniPrefabs.SetActive (true);
		recallTextPrefab.SetActive (true);

		//Start This is for Recall Numbers
		for(int x=0; x<4; x++){
			RecallNumberObjectList[x].transform.GetChild(0).gameObject.SetActive(true);	
			
		}

		//Start This is for BingoCardMiniDisplay
		BingoCardMiniScript = GameObject.FindGameObjectWithTag ("BingoCardMini").GetComponent<BingoCardMini> ();
//		BingoCardBigScript = GameObject.FindGameObjectWithTag ("BingoCardBig").GetComponent<BingoCardBig> ();
		BingoCardMiniScript.UpdateBingoCardSingleDisplay ();
		BingoCardMiniScript.StartAnimDisplayAcross ();
		BingoCardMiniScript.StartAnimDisplayDown ();
		//END This is for BingoCardMiniDisplay

		//END This is for Recall Numbers



	}

	IEnumerator MoveRecallNumbersToMenu(){
		for(int x=0; x<RecallNumberObjectList.Count; x++){

			RecallNumberObjectList[x].GetComponent<RecallNumbersScript>().moving=true;
			//tempMenuButtonScript = MenuButtonListPattern[x].GetComponent<MenuButton> ();
			//tempMenuButtonScript.moving = true;
			yield return new WaitForSeconds(0.2f);
		}

	}

	IEnumerator MoveSmallHexToMenu(){
		//reverseLoop = 13; //forbacktitle
		for(int y=0; y<5 ;y++){
			
			for(int x=0; x<15; x++){
				tempHex = hexList[y][x].GetComponent<Hex>();
				tempHex.rotateTempNum=90;
				tempHex.rotating = true; 
				tempHex.moving = true; 

				if(tempHex.GetComponent<Hex>().hasChild){
					tempHex.transform.GetChild(0).gameObject.SetActive(false);
				}
				
				if(y==4 && x<14){
					
					//hexList[y][reverseLoop].GetComponent<Hex>().movingBackTitle = true; //forbacktitle
					//hexList[y][reverseLoop].GetComponent<Hex>().backTitleNumXPos = 1.1f;  //forbacktitle
					//hexList[y][reverseLoop].GetComponent<Hex>().SetBackTitle(indexXPosBackTitle[x]);  //forbacktitle
					
					//reverseLoop--;  //forbacktitle
				}

			}
			yield return new WaitForSeconds(0.03f);

			
		}
		
		//yield return new WaitForSeconds(0.4f);
		hexList [1] [7].GetComponent<Hex> ().ChangeLengthOfHex(16);
		//hexList [3] [7].GetComponent<Hex> ().ChangeLengthOfHex(16);
		//isMenuMoving = false;

	}

	IEnumerator MoveBigHexToMenu(){ 
		
		for (int x=0; x<5; x++) {
			tempHexBig = hexListBig[x].GetComponent<HexBig> ();
			//tempHex.rotateTempNum = 90;
			//tempHex.rotating = true; 
			tempHexBig.rotateTempNum = 0; 
			tempHexBig.rotating = true; 
			tempHexBig.moving = true; 
			tempHexBig.rotating = true; 
			
			
		}
		yield return null;


		
	}

	public void MoveBigMenuButtonToMenu(string tempName){
		MoveMenuButtonToMenu(tempName);
	}

	void MoveMenuButtonToMenu(string tempName){  //Start Optimize in the future. Use string to call functions

		if(tempName=="Pattern"){
			StartCoroutine(MovePatternToMenu());
		}
		else if(tempName=="Voices"){
			StartCoroutine(MoveVoicesToMenu());
		}
		else if(tempName=="AutoTumbler"){
			StartCoroutine(MoveAutoTumblerToMenu());
		}
		else if(tempName=="Tumbler"){
			StartCoroutine(MoveTumblerToMenu());
		}
		else if(tempName=="Volume"){
			StartCoroutine(MoveVolumeToMenu());	
		}										//End Optimize 


		/*--
		yield return new WaitForSeconds (0.40f);	
		for (int x=0; x<MenuButtonList.Count; x++) {
			MenuButtonList[x].SetActive(true);
			yield return null;		
			tempMenuButtonScript = MenuButtonList[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}
		--*/
		

	}
	IEnumerator MovePatternToMenu(){
		for(int x=0; x<MenuButtonListPattern.Count; x++){
			MenuButtonListPattern[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListPattern[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}

		BingoCard.SetActive (true); // set BingoCard Active
		BingoClose.SetActive (true); // set BingoCard Active
		BingoReset.SetActive (true); // set BingoCard Active

		//// Update display when opening menu
		MM_Script.AutoTumblerSpeed();
		MM_Script.VolumeSubTask ();
	
	}

	IEnumerator MoveVoicesToMenu(){
		for(int x=0; x<MenuButtonListVoices.Count; x++){
			MenuButtonListVoices[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListVoices[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}
	}

	IEnumerator MoveAutoTumblerToMenu(){
		for(int x=0; x<MenuButtonListAutoTumbler.Count; x++){
			MenuButtonListAutoTumbler[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListAutoTumbler[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}
	}

	IEnumerator MoveTumblerToMenu(){
		for(int x=0; x<MenuButtonListTumbler.Count; x++){
			MenuButtonListTumbler[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListTumbler[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}
	}

	IEnumerator MoveVolumeToMenu(){
		for(int x=0; x<MenuButtonListVolume.Count; x++){
			MenuButtonListVolume[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListVolume[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}
	}

	//---------------------------------MOVE TO GAME---------------------------------------//

	IEnumerator MoveRecallNumbersToGame(){
		for(int x=0; x<RecallNumberObjectList.Count; x++){
			
			RecallNumberObjectList[x].GetComponent<RecallNumbersScript>().movingToGame=true;
			//tempMenuButtonScript = MenuButtonListPattern[x].GetComponent<MenuButton> ();
			//tempMenuButtonScript.moving = true;
			yield return new WaitForSeconds(0.2f);
		}
		
	}

	IEnumerator MoveSmallHexToGame(){
		yield return new WaitForSeconds (0.8f);
		hexList [1] [7].GetComponent<Hex> ().ChangeLengthOfHex(0.5f);
		hexList [3] [7].GetComponent<Hex> ().ChangeLengthOfHex(0.5f);
		
		for(int y=0; y<5 ;y++){
			
			for(int x=0; x<15; x++){
				tempHex = hexList[y][x].GetComponent<Hex>();
				tempHex.rotateTempNum=0;
				tempHex.rotating = true; 
				tempHex.movingBackGame = true; 
				if(tempHex.GetComponent<Hex>().hasChild){
					tempHex.transform.GetChild(0).gameObject.SetActive(true);
				}


				
				if(y==4 && x<14){
					//hexList[y][x].GetComponent<Hex>().movingBackGame = true; 
					
					
				}
			}
			yield return new WaitForSeconds(0.03f);
		}
		yield return new WaitForSeconds(0.4f);
		//isMenuMoving = false;
	}
	
	IEnumerator MoveBigHexToGame(){
		yield return new WaitForSeconds (0.20f);	
		for(int x=0; x<5; x++){
			tempHexBig = hexListBig[x].GetComponent<HexBig>();
			tempHexBig.rotateTempNum=90;
			tempHexBig.rotatingToGame = true; 
			tempHexBig.movingToGame = true; 
			yield return new WaitForSeconds(0.03f);				
		}
		
		
		//isMenuMoving = false;

	}

	IEnumerator MoveMenuButtonToGame(){
		StartCoroutine (MoveVolumeToGame());
		yield return null;
		StartCoroutine (MoveTumblerToGame());
		yield return null;
		StartCoroutine (MoveAutoTumblerToGame());
		yield return null;
		StartCoroutine (MoveVoicesToGame());
		yield return null;
		StartCoroutine (MovePatternToGame());
	}

	IEnumerator MoveVolumeToGame(){ 
		for(int x=MenuButtonListVolume.Count-1; x>-1; x--){
			//MenuButtonListVolume[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListVolume[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.movingToGame = true;
		}
	}

	IEnumerator MoveTumblerToGame(){ 
		for(int x=MenuButtonListTumbler.Count-1; x>-1; x--){
			//MenuButtonListTumbler[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListTumbler[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.movingToGame = true;
		}
	}

	IEnumerator MoveAutoTumblerToGame(){ 
		for(int x=MenuButtonListAutoTumbler.Count-1; x>-1; x--){
			//MenuButtonListAutoTumbler[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListAutoTumbler[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.movingToGame = true;
		}
	}
	IEnumerator MoveVoicesToGame(){ 
		for(int x=MenuButtonListVoices.Count-1; x>-1; x--){
			//MenuButtonListVoices[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListVoices[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.movingToGame = true;
		}
	}

	IEnumerator MovePatternToGame(){ 
		for(int x=MenuButtonListPattern.Count-1; x>-1; x--){
			//MenuButtonListPattern[x].SetActive(true);
			yield return null;
			tempMenuButtonScript = MenuButtonListPattern[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.movingToGame = true;
		}

		BingoCard.SetActive (false); // set BingoCard Active
		BingoClose.SetActive (false); // set BingoCard Active
		BingoReset.SetActive (false); // set BingoCard Active
	
	}



	public void DisplayRecallNumbers(int tempValue){
		for(int x=2; x>-1; x--){
			if(RecallNumberObjectList[x].GetComponent<RecallNumbersScript>().isOccupied){
				//RecallNumberObjectList[x+1].GetComponent<SpriteRenderer>().sprite = RecallNumberObjectList[x].GetComponent<SpriteRenderer>().sprite;
				//RecallNumberObjectList[x+1].GetComponentInChildren<SpriteRenderer>().sprite = RecallNumberObjectList[x].GetComponentInChildren<SpriteRenderer>().sprite;
				RecallNumberObjectList[x+1].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = RecallNumberObjectList[x].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
				RecallNumberObjectList[x+1].GetComponent<RecallNumbersScript>().value = RecallNumberObjectList[x].GetComponent<RecallNumbersScript>().value;
				RecallNumberObjectList[x+1].GetComponent<RecallNumbersScript>().isOccupied=true;
			}
		}

		RecallNumberObjectList [0].GetComponent<RecallNumbersScript> ().value = tempValue;
		RecallNumberObjectList[0].GetComponent<RecallNumbersScript>().isOccupied=true;
		RecallNumberObjectList [0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Textures/BallSprites/Ball"+tempValue);
	}



	/*--
	IEnumerator MoveMenuButtonToMenu(){ 
		yield return new WaitForSeconds (0.40f);	
		for (int x=0; x<MenuButtonList.Count; x++) {
			MenuButtonList[x].SetActive(true);
			yield return null;		
			tempMenuButtonScript = MenuButtonList[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.moving = true;
		}

		
	}

	IEnumerator MoveMenuButtonToGame(){ 
		//yield return new WaitForSeconds (0.40f);	
		//for (int x=0; x<MenuButtonList.Count; x++) {
		for (int x=MenuButtonList.Count-1; x>-1; x--) {


			tempMenuButtonScript = MenuButtonList[x].GetComponent<MenuButton> ();
			tempMenuButtonScript.movingToGame = true;
			//MenuButtonList[x].SetActive(false);
			yield return null;		
		}
		
		
	}
	--*/

	public void ResetGame(){
		Debug.Log("Reset");

		BallManager tempBM = GameObject.FindGameObjectWithTag ("BallManager").GetComponent<BallManager>();
		BingoCardBigScript = GameObject.FindGameObjectWithTag ("BingoCardBig").GetComponent<BingoCardBig> ();
		//GameObject[] Balls = GameObject.FindGameObjectsWithTag();
		//Debug.Log (Balls.Length);
		/*
		 foreach(GameObject ball in Balls){
			Destroy (ball.gameObject);
			//Destroy (ball);
			Debug.Log ("Kill a Ball");
		}
		 */

		tempBM.ClearBalls ();
		tempBM.RePopulateNumberList ();

		for(int x=0; x<RecallNumberObjectList.Count; x++){

			RecallNumberObjectList [x].GetComponent<RecallNumbersScript> ().value = 0;
			RecallNumberObjectList[x].GetComponent<RecallNumbersScript>().isOccupied=false;
			RecallNumberObjectList [x].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
		}

		BingoCardBigScript.ResetBingoCards ();

	}









}// END of Class

