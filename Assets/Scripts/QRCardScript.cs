using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BarcodeScanner;
using BarcodeScanner.Scanner;
using UnityEngine.SceneManagement;

public class QRCardScript : MonoBehaviour {

	private IScanner BarcodeScanner;
	private float RestartTime;

	
	public SoundManagerScript SM_Script;
	public Text TextHeader;
	public Button ScanButton, CompareButton;

	//public AudioSource Audio;
	public RawImage CameraDisplay;
	public string QRCode;
	public bool isQR;
	public string Card1, Card2, Card3;
	public List<string> Cards;

	[SerializeField]
	private bool isRunning;

	//Start Compare Variables
	public string scannedCardName;
	public string scannedCardNumberString;
	public List<int> scannedCardNumber;
	public List<bool> scannedCardPatternCheck;
	public bool isMatch;

	public GameObject CardScanner;

	public GameObject BingoWin;
	public GameObject BingoLose;

	//End Compare Variables

	void Awake()
	{
		BarcodeScanner = new Scanner();
		isRunning = false;
		
	}

	// Use this for initialization
	void Start()
	{
		
		initialiseCamera();

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
		BarcodeScanner.StatusChanged += (sender, arg) => {
			TextHeader.text = "Status: " + BarcodeScanner.Status;
		};

		BarcodeScanner.Scan((barCodeType, barCodeValue) => {
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


			//ClickCompare(); //This is to compare winning card. Change it to add to userprefs
			CheckCardExist();


#if UNITY_ANDROID || UNITY_IOS
		    Handheld.Vibrate();
#endif
		});
	}

	private void initialiseCamera()
	{
		BarcodeScanner.Camera.Play();
		Debug.Log("Camera Started");
		BarcodeScanner.OnReady += (sender, arg) => {
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

		BarcodeScanner.StatusChanged += (sender, arg) => {
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

	private void startScanning()
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

	public void QRButtonTask()
	{

		if (!isRunning)
		{
			isRunning = true;
			startScanning();
			CardScanner.SetActive(true);
		}
		else
		{
			isRunning = false;
			stopScanning();
			CardScanner.SetActive(false);
		}

		// SceneManager.LoadScene("QR Scene");
	}


	public void CheckCardExist() {

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
			scannedCardNumberString = "";
			x++;
			string tempNum = "";
			for (int y = x; y < QRCode.Length; y++)
			{
				if (QRCode[y] != ',')
				{
					tempNum += QRCode[y];
					scannedCardNumberString += QRCode[y];
				}
				else
				{
					scannedCardNumber.Add(int.Parse(tempNum));
					Debug.Log(scannedCardNumber);
					Debug.Log(scannedCardNumberString);
					scannedCardNumberString += QRCode[y];
					tempNum = "";
				}
			}

		}



		
		//Start saving Cards
			if (PlayerPrefs.HasKey(scannedCardName))
			{
				Debug.Log(scannedCardName + " already saved. Scan Another.");

			}
			else {

				PlayerPrefs.SetString(scannedCardName, scannedCardNumberString);
				Debug.Log(scannedCardName + " does not exist and was saved");
		
			}

		//End saving Cards


	}

}


/*--
 
		if(!PlayerPrefs.HasKey("First Initialization"))
		{
			PlayerPrefs.SetInt("First Initialization",1);

			PlayerPrefs.SetString("EE_Top1_Name_Easy", "AAAA");
			PlayerPrefs.SetString("EE_Top2_Name_Easy", "BBBB");
			PlayerPrefs.SetString("EE_Top3_Name_Easy", "CCCC");

			PlayerPrefs.SetInt("EE_Top1_Score_Easy", 000);
			PlayerPrefs.SetInt("EE_Top2_Score_Easy", 000);
			PlayerPrefs.SetInt("EE_Top3_Score_Easy", 000);

			PlayerPrefs.SetString("EE_Top1_Name_Advance", "McC1");
			PlayerPrefs.SetString("EE_Top2_Name_Advance", "McC2");
			PlayerPrefs.SetString("EE_Top3_Name_Advance", "McC3");
			
			PlayerPrefs.SetInt("EE_Top1_Score_Advance", 000);
			PlayerPrefs.SetInt("EE_Top2_Score_Advance", 000);
			PlayerPrefs.SetInt("EE_Top3_Score_Advance", 000);

			PlayerPrefs.SetString("EE_Top1_Name_Expert", "CLN1");
			PlayerPrefs.SetString("EE_Top2_Name_Expert", "CLN2");
			PlayerPrefs.SetString("EE_Top3_Name_Expert", "CLN3");
			
			PlayerPrefs.SetInt("EE_Top1_Score_Expert", 000);
			PlayerPrefs.SetInt("EE_Top2_Score_Expert", 000);
			PlayerPrefs.SetInt("EE_Top3_Score_Expert", 000);
		}
  
 --*/

