using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BarcodeScanner;
using BarcodeScanner.Scanner;
using UnityEngine.SceneManagement;

public class QRScript : MonoBehaviour
{
	private IScanner BarcodeScanner;
	private float RestartTime;

	public Text cardWinner;
	public bool alreadyHasWinner;
	public bool multipleWinner;

	public GM GM_Script;
	public BallManager BM_Script;
	public SoundManagerScript SM_Script;
	public WinnerCardScript WIN_Script;
	public bool winnerAlive;
	public GameObject winnerText;

	public UnityEngine.Object winCardPrefab;
	public GameObject winCardGameObject;
	public Transform winCardPos;


	public Text TextHeader;
	public Button ScanButton, CompareButton;

	//public AudioSource Audio;
	public RawImage CameraDisplay;
	public string QRCode;
	public bool isQR;
	public string Card1, Card2, Card3;
	public List<string> Cards;

	private bool isRunning;

	//Start Compare Variables
	public string scannedCardName;
	public List<int> scannedCardNumber;
	public List<bool> scannedCardPatternCheck;
	public bool isMatch;

	public string tempCardName;
	public bool iteratingCards;
	public List<string> tempCardArray;
	public List<string> cardNameArray;
	public List<string> cardNumberArray;

	public GameObject BingoWin;
	public GameObject BingoLose;

	//End Compare Variables

	//Start Winner Couting
	public int firstWinnerNum = 0;
	public int secondWinnerNum = 0;

	public List<int> CardNum_Stack;
	public List<int> FirstNum_Stack;
	public List<int> SecondNum_Stack;

	public List<List<int>> tempActiveNumberList;
	[SerializeField]
	private bool isComparing;
	//End Winner Counting

	//Start Toggle Winner Spawner
	public Text toggleWinnerText;
	public bool spawnWinners;
    public GameObject winnerButton;

	void Awake()
	{
        spawnWinners = false;
		isComparing = false;
		tempCardArray = new List<string>();
		cardNameArray = new List<string>();
		cardNumberArray = new List<string>();
		GetSavedCards();

		alreadyHasWinner = false;

		firstWinnerNum = 0;
		secondWinnerNum = 0;

		//Screen.autorotateToPortrait = false;
		//Screen.autorotateToPortraitUpsideDown = false;
		//Board = "1:B,1,12,13,14,14-I:,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-";
		Card1 = "1:B,1,12,13,14,14-I:,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-";
		Card2 = "Card2-B:1,2,3,4,4-I:23,24,34,56,43-N:23,24,34,56,43-G:23,24,34,56,43-0:23,24,34,56,43";
		Card3 = "Card2-B:13,22,13,14,24-I:33,14,4,6,3-N:13,22,34,46,45-G:53,25,4,6,3-0:2,4,4,6,3";
		Cards.Add(Card1);
		Cards.Add(Card2);

		isMatch = false;
		scannedCardPatternCheck = new List<bool>();
		//for(int x=0; x<25; x++){
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false); //this is the middle free on the card
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		scannedCardPatternCheck.Add(false);
		//}

		GM_Script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
		BM_Script = GameObject.FindGameObjectWithTag("BallManager").GetComponent<BallManager>();
		SM_Script = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManagerScript>();
		//WIN_Script = GameObject.FindGameObjectWithTag("WinnerDisplay").GetComponent<WinnerCardScript>();
		BarcodeScanner = new Scanner();
		isRunning = false;
		// BarcodeScanner = new Scanner();

		CardNum_Stack = new List<int>();
		FirstNum_Stack = new List<int>();
		SecondNum_Stack = new List<int>();
	}

	// Use this for initialization
	void Start()
	{
		//BarcodeScanner.Camera.Play();
		initialiseCamera();
		firstWinnerNum = 0;
		secondWinnerNum = 0;

		//RestartTime = Time.realtimeSinceStartup;

		tempActiveNumberList = new List<List<int>>();
		StartCoroutine(CompareStoredCards());
		StartCoroutine(SpawningWinners());

	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("Barcode exists " + (BarcodeScanner != null));
		if (BarcodeScanner != null)
		{
			BarcodeScanner.Update();
		}

		// Check if the Scanner need to be started or restarted
		//Debug.Log("Scanning " + isRunning);
		// if ((RestartTime != 0 && RestartTime < Time.realtimeSinceStartup) && isRunning)
		if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup && isRunning)
		{
			StartScanner();
			RestartTime = 0;
		}
	}



	private bool checkQR(string type)
	{
		bool result = false;
		if (type == "QR_CODE")
		{
			result = true;
		}
		return result;
	}

	private void saveType(string type)
	{
		isQR = checkQR(type);
	}
	private void saveValue(string value)
	{
		QRCode = value;
	}


	private void StartScanner()
	{
		//initialiseCamera();
		BarcodeScanner.StatusChanged += (sender, arg) =>
		{
			TextHeader.text = "Status: " + BarcodeScanner.Status;
		};

		BarcodeScanner.Scan((barCodeType, barCodeValue) =>
		{
			BarcodeScanner.Stop();
			/* if (TextHeader.text.Length > 250)
            {
                TextHeader.text = "";
            } */
			TextHeader.text += "Found: " + barCodeType + " / " + barCodeValue + "\n";
			RestartTime += Time.realtimeSinceStartup + 5f;
			saveType(barCodeType);
			saveValue(barCodeValue);
			//Debug.Log("QR Code? " + isQR);
			//if (isQR) { QRCode = barCodeValue;  }
			//QRCode = barCodeValue;
			Debug.Log(barCodeType);
			Debug.Log(barCodeValue);
			Debug.Log("Saved Check in Scanner(): " + isQR);
			Debug.Log("Saved Value in Scanner(): " + QRCode);


			ClickCompare();
#if UNITY_ANDROID || UNITY_IOS
		    Handheld.Vibrate();
#endif
		});
	}

	private void initialiseCamera()
	{
		BarcodeScanner.Camera.Play();
		Debug.Log("Camera Started");
		BarcodeScanner.OnReady += (sender, arg) =>
		{
			// Set Orientation & Texture
			CameraDisplay.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
			CameraDisplay.transform.localScale = BarcodeScanner.Camera.GetScale();
			CameraDisplay.texture = BarcodeScanner.Camera.Texture;

			// Keep Image Aspect Ratio
			var rect = CameraDisplay.GetComponent<RectTransform>();
			var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
			rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);

			RestartTime = Time.realtimeSinceStartup;

		};

		BarcodeScanner.StatusChanged += (sender, arg) =>
		{
			TextHeader.text = "Status: " + BarcodeScanner.Status;
		};
	}

	//Stops Scanning and Stops Camera
	public void stopScanning()
	{
		//isRunning = false;
		//if (isRunning)
		// {
		// ClickQRScan();
		//}
		isRunning = false;
		TextHeader.text = "Status: " + BarcodeScanner.Status;
		ScanButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("QRCode/Scan");
		BarcodeScanner.Stop();
		BarcodeScanner.Camera.Stop();
	}

	public void startScanning()
	{
		isRunning = true;
		ScanButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("QRCode/Scanning");
		initialiseCamera();
		StartScanner();
	}

	#region UI Buttons
	public void ClickQRScan()
	{
		if (BarcodeScanner == null)
		{
			TextHeader.text = "No valid camera";
			return;
		}
		//if (BarcodeScanner != null)
		//{
		//if (BarcodeScanner.Status)

		// Track status of the scanner
		//if (!(BarcodeScanner.Status == ScannerStatus.Running))
		if (!isRunning)
		{
			//if (!BarcodeScanner.Camera.IsPlaying()) { initialiseCamera(); }
			startScanning();
			// Switch Text
			//Scan.GetComponent<Text>().text = "Stop Scan";
		}
		else
		{
			/* StartCoroutine(StopCamera(() => {
            })); */
			stopScanning();

			Debug.Log("Stopped Scan");
			//Debug.Log("Camera Stopped");
			//ScanButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("QRCode/Scan");
			//isRunning = false;
		}

		Debug.Log("isRunning " + isRunning);
		Debug.Log("Saved Check in QRScanButton(): " + isQR);
		Debug.Log("Saved Value in QRScanButton(): " + QRCode);
		//}
		//else { BarcodeScanner = new Scanner(); }
	}

	public void DisplayWrongQR()
	{
		TextHeader.text = "Invalid Bingo QRCode";
		SM_Script.PlayOther_SFX("wrong");
	}





	public void ClickCompare()
	{
		if (this.isQR)
		{
			scannedCardName = "";
			int x = 0;
			while (true)
			{

				if (QRCode[x] != ':')
				{
					scannedCardName += QRCode[x];
					if (x > 9)
					{
						DisplayWrongQR();
						return;
					}
					x++;
				}
				else
				{
					break;
				}
			}


			scannedCardNumber.Clear();
			x++;
			string tempNum = "";
			for (int y = x; y < QRCode.Length; y++)
			{
				if (QRCode[y] != ',')
				{
					tempNum += QRCode[y];
				}
				else
				{
					scannedCardNumber.Add(int.Parse(tempNum));
					Debug.Log(scannedCardNumber);
					tempNum = "";
				}
			}

			for (int c = 0; c < scannedCardPatternCheck.Count; c++)
			{
				scannedCardPatternCheck[c] = false;
			}

			for (int z = 0; z < BM_Script.poolActiveNumberList.Count; z++)
			{

				//BM_Script.poolActiveNumberList[z];
				for (int a = 0; a < scannedCardPatternCheck.Count; a++)
				{
					if (BM_Script.poolActiveNumberList[z] == scannedCardNumber[a])
					{
						scannedCardPatternCheck[a] = true;
					}

				}

			}

			isMatch = true;
			int tempAnimatedPatternCheckNum = 0;

			if (GM_Script.activePatternName == "Pattern1")
			{
				if (scannedCardPatternCheck[0] && scannedCardPatternCheck[5] && scannedCardPatternCheck[10] && scannedCardPatternCheck[15] && scannedCardPatternCheck[20])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[1] && scannedCardPatternCheck[6] && scannedCardPatternCheck[11] && scannedCardPatternCheck[16] && scannedCardPatternCheck[21])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[2] && scannedCardPatternCheck[7] && scannedCardPatternCheck[17] && scannedCardPatternCheck[22])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[3] && scannedCardPatternCheck[8] && scannedCardPatternCheck[13] && scannedCardPatternCheck[18] && scannedCardPatternCheck[23])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[4] && scannedCardPatternCheck[9] && scannedCardPatternCheck[14] && scannedCardPatternCheck[19] && scannedCardPatternCheck[24])
				{
					tempAnimatedPatternCheckNum++;
				}

				if (tempAnimatedPatternCheckNum < 2)
				{
					isMatch = false;
				}
			}
			else if (GM_Script.activePatternName == "Pattern2")
			{
				if (scannedCardPatternCheck[0] && scannedCardPatternCheck[1] && scannedCardPatternCheck[2] && scannedCardPatternCheck[3] && scannedCardPatternCheck[4])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[5] && scannedCardPatternCheck[6] && scannedCardPatternCheck[7] && scannedCardPatternCheck[8] && scannedCardPatternCheck[9])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[10] && scannedCardPatternCheck[11] && scannedCardPatternCheck[13] && scannedCardPatternCheck[14])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[15] && scannedCardPatternCheck[16] && scannedCardPatternCheck[17] && scannedCardPatternCheck[18] && scannedCardPatternCheck[19])
				{
					tempAnimatedPatternCheckNum++;
				}
				if (scannedCardPatternCheck[20] && scannedCardPatternCheck[21] && scannedCardPatternCheck[22] && scannedCardPatternCheck[23] && scannedCardPatternCheck[24])
				{
					tempAnimatedPatternCheckNum++;
				}

				if (tempAnimatedPatternCheckNum < 2)
				{
					isMatch = false;
				}
			}
			else
			{
				for (int b = 0; b < scannedCardPatternCheck.Count; b++)
				{
					if (GM_Script.blueIsActiveList[b])
					{
						if (scannedCardPatternCheck[b])
						{
						}
						else
						{

							isMatch = false;
							break;
						}
					}

				}
			}

			// if (isMatch) { { TextHeader.text = "Bingo!!!"; Debug.Log("Match"); } }
			// else { { TextHeader.text = "Not Bingo"; Debug.Log("not a match"); } }


			if (isMatch)
			{

				BingoWin.gameObject.SetActive(true);
				Invoke("RemoveDisplayBingoWin", 3.5f);
				SM_Script.PlayOther_SFX("beep");


			}
			else
			{

				BingoLose.gameObject.SetActive(true);
				Invoke("RemoveDisplayBingoLose", 3.5f);
				SM_Script.PlayOther_SFX("wrong");
			}



			/*
             if (isMatch) { TextHeader.text = " "+scannedCardName + " is Bingo"; Debug.Log("Match"); }
			else { TextHeader.text = " "+scannedCardName + " is not Bingo"; Debug.Log("not a match"); }
             * */


			//string array ends but does not finish with a comma
			//scannedCardNumber.Add(int.Parse(tempNum));
			//end of crazy code because somebody will always ask what it is doing

			//QRcode
			/*
            foreach (string card in Cards)
            {
				
				if (QRCode == card) {
					TextHeader.text = "Matches";
				}
				else {
					TextHeader.text = "Does not match";
				}
				
            }
			*/
			Debug.Log(TextHeader.text);
		}
		else
		{
			TextHeader.text = "Not a QR Code"; Debug.Log(TextHeader.text);
			SM_Script.PlayOther_SFX("wrong");
		}
	}

	public void RemoveDisplayBingoWin()
	{

		BingoWin.gameObject.SetActive(false);
	}

	public void RemoveDisplayBingoLose()
	{

		BingoLose.gameObject.SetActive(false);
	}

	public IEnumerator StopCamera(Action callback)
	{
		// Stop Scanning
		CameraDisplay = null;
		BarcodeScanner.Destroy();
		BarcodeScanner = null;

		// Wait a bit
		yield return new WaitForSeconds(0.1f);

		callback.Invoke();
	}

	#endregion

	public void GetSavedCards()
	{
		tempCardArray.Clear();
		cardNameArray.Clear();
		cardNumberArray.Clear();

		iteratingCards = true;
		int tempIndex = 1;
		while (iteratingCards)
		{
			tempCardName = "TempCard" + tempIndex;
			if (PlayerPrefs.HasKey(tempCardName))
			{
				tempCardArray.Add(tempCardName);
				cardNameArray.Add(PlayerPrefs.GetString(tempCardName));
				cardNumberArray.Add(PlayerPrefs.GetString(PlayerPrefs.GetString(tempCardName)));

				tempIndex++;
			}
			else
			{
				iteratingCards = false;
			}
		}

	}



	public void CompareStoredCardsMain(List<int> tempActNum)
	{


		tempActiveNumberList.Add(new List<int>(tempActNum));

		//Debug.Log(tempActiveNumberList[0]);
		for (int y = 0; y < tempActiveNumberList.Count; y++)
		{
			Debug.Log("List No. " + y);
			for (int x = 0; x < tempActiveNumberList[y].Count; x++)
			{
				Debug.Log(tempActiveNumberList[y][x]);
			}
		}
		//		if(!isComparing)
		//		{
		//			isComparing = true;
		//			StartCoroutine(CompareStoredCards());
		//		}
	}
	public void toggleSpawnWinners() {

		if (spawnWinners == true)
		{

			spawnWinners = false;
            winnerButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameButtons/hide_winners_NOTpressed");

        }
		else {

			spawnWinners = true;
            winnerButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameButtons/show_winners_NOTpressed");
            //winnerButton.GetComponent<Button>().spriteState.pressedSprite = Resources.Load<Sprite>("GameButtons/show_winners_NOTpressed");

        }

	}

	IEnumerator CompareStoredCards()
	{
		while (true)
		{
			if (tempActiveNumberList.Count > 0)
			{

				for (int xy = 0; xy < cardNumberArray.Count; xy++)
				{
					scannedCardNumber.Clear();

					string tempNum = "";
					for (int y = 0; y < cardNumberArray[xy].Length; y++)
					{
						if (cardNumberArray[xy][y] != ',')
						{
							tempNum += cardNumberArray[xy][y];
						}
						else
						{
							scannedCardNumber.Add(int.Parse(tempNum));
							//Debug.Log(scannedCardNumber);
							tempNum = "";
						}
					}



					for (int c = 0; c < scannedCardPatternCheck.Count; c++)
					{
						scannedCardPatternCheck[c] = false;
					}

					for (int z = 0; z < tempActiveNumberList[0].Count; z++)
					{

						//BM_Script.poolActiveNumberList[z];
						for (int a = 0; a < scannedCardPatternCheck.Count; a++)
						{
							if (tempActiveNumberList[0][z] == scannedCardNumber[a])
							{
								scannedCardPatternCheck[a] = true;
							}

						}

					}


					//--------------------Start Comparing Cards----------------------------//
					isMatch = true;
					int tempAnimatedPatternCheckNum = 0;

					if (GM_Script.activePatternName == "Pattern1")
					{
						if (scannedCardPatternCheck[0] && scannedCardPatternCheck[5] && scannedCardPatternCheck[10] && scannedCardPatternCheck[15] && scannedCardPatternCheck[20])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[1] && scannedCardPatternCheck[6] && scannedCardPatternCheck[11] && scannedCardPatternCheck[16] && scannedCardPatternCheck[21])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[2] && scannedCardPatternCheck[7] && scannedCardPatternCheck[17] && scannedCardPatternCheck[22])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[3] && scannedCardPatternCheck[8] && scannedCardPatternCheck[13] && scannedCardPatternCheck[18] && scannedCardPatternCheck[23])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[4] && scannedCardPatternCheck[9] && scannedCardPatternCheck[14] && scannedCardPatternCheck[19] && scannedCardPatternCheck[24])
						{
							tempAnimatedPatternCheckNum++;
						}

						if (tempAnimatedPatternCheckNum < 2)
						{
							isMatch = false;
						}
					}
					else if (GM_Script.activePatternName == "Pattern2")
					{
						if (scannedCardPatternCheck[0] && scannedCardPatternCheck[1] && scannedCardPatternCheck[2] && scannedCardPatternCheck[3] && scannedCardPatternCheck[4])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[5] && scannedCardPatternCheck[6] && scannedCardPatternCheck[7] && scannedCardPatternCheck[8] && scannedCardPatternCheck[9])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[10] && scannedCardPatternCheck[11] && scannedCardPatternCheck[13] && scannedCardPatternCheck[14])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[15] && scannedCardPatternCheck[16] && scannedCardPatternCheck[17] && scannedCardPatternCheck[18] && scannedCardPatternCheck[19])
						{
							tempAnimatedPatternCheckNum++;
						}
						if (scannedCardPatternCheck[20] && scannedCardPatternCheck[21] && scannedCardPatternCheck[22] && scannedCardPatternCheck[23] && scannedCardPatternCheck[24])
						{
							tempAnimatedPatternCheckNum++;
						}

						if (tempAnimatedPatternCheckNum < 2)
						{
							isMatch = false;
						}
					}
					else
					{
						for (int b = 0; b < scannedCardPatternCheck.Count; b++)
						{
							if (GM_Script.blueIsActiveList[b])
							{
								if (scannedCardPatternCheck[b])
								{
								}
								else
								{

									isMatch = false;
									break;
								}
							}

						}
					}

					if (isMatch)
					{

						if (!alreadyHasWinner)
						{
							alreadyHasWinner = true;
							if (secondWinnerNum >= 9)
							{
								firstWinnerNum++;
								secondWinnerNum = 0;
							}
							else
							{
								secondWinnerNum++;
							}
						}
						else
						{



						}
						string[] cardNameSplit = cardNameArray[xy].Split(' ');

						Debug.Log(cardNameArray[xy] + "is a match and has been removed from the card array. Winner Number " + firstWinnerNum + secondWinnerNum);
						if (int.Parse(cardNameSplit[1]) > 9)
						{
							cardWinner.text += cardNameArray[xy] + " is Winner " + firstWinnerNum + secondWinnerNum + "\n";
						}
						else {
							
							cardWinner.text += cardNameSplit[0] + " 0" + cardNameSplit[1] + " is Winner " + firstWinnerNum + secondWinnerNum + "\n";
						}
						

						cardNameArray.RemoveAt(xy);
						cardNumberArray.RemoveAt(xy);
						tempCardArray.RemoveAt(xy);

						xy--;

						//yield return new WaitUntil(() => winnerAlive == false);
						//winnerAlive = true;

						//winCardGameObject = Instantiate(winCardPrefab, winCardPos.localPosition, Quaternion.identity) as GameObject;
						//winCardGameObject.GetComponent<WinnerCardScript>().SetWinnerNumber(int.Parse(cardNameSplit[1]), firstWinnerNum, secondWinnerNum);

						CardNum_Stack.Add(int.Parse(cardNameSplit[1]));
						FirstNum_Stack.Add(firstWinnerNum);
						SecondNum_Stack.Add(secondWinnerNum);


					}



					//--------------------End Comparing Cards----------------------------//
					yield return new WaitForSeconds(0.03f);

				}

				tempActiveNumberList.RemoveAt(0);
				alreadyHasWinner = false;

			}//END of if loop

			yield return null;
		}//E.N.D.
	}






	IEnumerator SpawningWinners() {		//this is for Displaying the winner on the Bingo Board
		while (true) {

			if (CardNum_Stack.Count > 0 && spawnWinners == true) {

				yield return new WaitForSeconds(2.0f);
				winCardGameObject = Instantiate(winCardPrefab, winCardPos.localPosition, Quaternion.identity) as GameObject;
				winCardGameObject.GetComponent<WinnerCardScript>().SetWinnerNumber(CardNum_Stack[0], FirstNum_Stack[0], SecondNum_Stack[0]);

				CardNum_Stack.RemoveAt(0);
				FirstNum_Stack.RemoveAt(0);
				SecondNum_Stack.RemoveAt(0);


			}
			

			yield return null;
		}

		
	}


}


//poolNumberList.RemoveAt (tempRandNum);

/*
// Create a basic scanner
BarcodeScanner = new Scanner();

// Start playing the camera
BarcodeScanner.Camera.Play();

// Event when for the camera is ready to scan
BarcodeScanner.OnReady += (sender, arg) => {

    // Bind the Camera texture to any RawImage in your scene
    Image.texture = BarcodeScanner.Camera.Texture;

    // Start Scanning
    BarcodeScanner.Scan((barCodeType, barCodeValue) => {

        // This callback is call when something is scanned
        Debug.Log("Found: " + barCodeType + " / " + barCodeValue);
    });
};

...

void Update()
{
    // The barcode scanner has to be updated manually
    BarcodeScanner.Update();
}
*/
/*public void ClickStart()
{
    if (BarcodeScanner == null)
    {
        TextHeader.text = "No valid camera - Click Start";
        return;
    }

    TextHeader.text = "Status: " + BarcodeScanner.Status;

    // Start Scanning
    BarcodeScanner.Scan((barCodeType, barCodeValue) => {
        BarcodeScanner.Stop();
        TextHeader.text = "Found: " + barCodeType + " / " + barCodeValue;
        isQR = checkQR(barCodeType);
        QRCode = barCodeValue;

        Debug.Log(barCodeType);
        Debug.Log(barCodeValue);

        // Feedback
        //Audio.Play();

        #if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
        #endif
    });
}

public void ClickStop()
{
    if (BarcodeScanner == null)
    {
        TextHeader.text = "No valid camera - Click Stop";
        return;
    }

    // Stop Scanning
    BarcodeScanner.Stop();
} */

/*public IEnumerator Scan()
{

}*/

/* if (BarcodeScanner == null)
{
return;
}
BarcodeScanner.Update();

// Check if the Scanner need to be started or restarted
if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup)
{
StartScanner();
RestartTime = 0;
} */
