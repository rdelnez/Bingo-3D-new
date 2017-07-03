using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BarcodeScanner;
using BarcodeScanner.Scanner;
using UnityEngine.SceneManagement;

public class QRScript : MonoBehaviour {
    private IScanner BarcodeScanner;
    private float RestartTime;

    public GM GM_Script;
    public Text TextHeader;
    public Button ScanButton, CompareButton;

    //public AudioSource Audio;
    public RawImage CameraDisplay;
    public string QRCode;
    public bool isQR;
    public string Card1, Card2, Card3;
    public List<string> Cards;

    private bool isRunning;

    void Awake()
    {
        //Screen.autorotateToPortrait = false;
        //Screen.autorotateToPortraitUpsideDown = false;
        //Board = "1:B,1,12,13,14,14-I:,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-";
        Card1 = "1:B,1,12,13,14,14-I:,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-";
        Card2 = "Card2-B:1,2,3,4,4-I:23,24,34,56,43-N:23,24,34,56,43-G:23,24,34,56,43-0:23,24,34,56,43";
        Card3 = "Card2-B:13,22,13,14,24-I:33,14,4,6,3-N:13,22,34,46,45-G:53,25,4,6,3-0:2,4,4,6,3";
        Cards.Add(Card1);
        Cards.Add(Card2);
        

        GM_Script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
        BarcodeScanner = new Scanner();
        isRunning = false;
        // BarcodeScanner = new Scanner();
    }

    // Use this for initialization
    void Start () {
        //BarcodeScanner.Camera.Play();
        initialiseCamera();
        
        //RestartTime = Time.realtimeSinceStartup;
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
        if (type == "QR_CODE") { result = true; }
        return result;
    }

    private void saveType(string type) { isQR = checkQR(type); }
    private void saveValue(string value) { QRCode = value; }
    

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
            isRunning = true;
            ScanButton.GetComponentInChildren<Text>().text = "Stop Scan";
			initialiseCamera();
			StartScanner();
            // Switch Text
            
            //Scan.GetComponent<Text>().text = "Stop Scan";
        }
        else
        {
            /* StartCoroutine(StopCamera(() => {
            })); */
            BarcodeScanner.Stop();
            isRunning = false;
            Debug.Log("Stopped Scan");
            //Debug.Log("Camera Stopped");
            BarcodeScanner.Camera.Stop();
            ScanButton.GetComponentInChildren<Text>().text = "Start Scan";
        }

        Debug.Log("isRunning " + isRunning);
        Debug.Log("Saved Check in QRScanButton(): " + isQR);
        Debug.Log("Saved Value in QRScanButton(): " + QRCode);
        //}
        //else { BarcodeScanner = new Scanner(); }
    }

    public void ClickCompare()
    {
        if (this.isQR)
        {
            TextHeader.text = "Does not match";
            foreach (string card in Cards)
            {
                if (QRCode == card) { TextHeader.text = "Matches";  }
            }
			Debug.Log(TextHeader.text);
		}
        else
        {
            TextHeader.text = "Not a QR Code"; Debug.Log(TextHeader.text);
        }
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

}

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
