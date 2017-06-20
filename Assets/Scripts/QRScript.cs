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
    public Button Scan, Compare;

    //public AudioSource Audio;
    public RawImage Image;
    public String QRCode;
    public Boolean isQR;
    public String Card1, Card2, Card3;
    public List<String> Cards;

    public Boolean isRunning;

    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        //Board = "1:B,1,12,13,14,14-I:,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-";
        Card1 = "1:B,1,12,13,14,14-I:,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-N: ,21,12,12,34,21-";
        Card2 = "Card2-B:1,2,3,4,4-I:23,24,34,56,43-N:23,24,34,56,43-G:23,24,34,56,43-0:23,24,34,56,43";
        Card3 = "Card2-B:13,22,13,14,24-I:33,14,4,6,3-N:13,22,34,46,45-G:53,25,4,6,3-0:2,4,4,6,3";
        Cards.Add(Card1);
        Cards.Add(Card2);
        isRunning = false;
    }

    // Use this for initialization
    void Start () {
        // Create a basic scanner
        BarcodeScanner = new Scanner();
        GM_Script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();

    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private Boolean checkQR(string type)
    {
        Boolean result = false;
        if (type == "QR_CODE") { result = true; }
        return result;
    }

    private void StartScanner()
    {
        BarcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        BarcodeScanner.OnReady += (sender, arg) => {
            // Set Orientation & Texture
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = BarcodeScanner.Camera.GetScale();
            Image.texture = BarcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = Image.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
        };

        // Track status of the scanner
        BarcodeScanner.StatusChanged += (sender, arg) => {
            TextHeader.text = "Status: " + BarcodeScanner.Status;
        };


        BarcodeScanner.Scan((barCodeType, barCodeValue) => {
            BarcodeScanner.Stop();
            if (TextHeader.text.Length > 250)
            {
                TextHeader.text = "";
            }
            TextHeader.text += "Found: " + barCodeType + " / " + barCodeValue + "\n";
            RestartTime += Time.realtimeSinceStartup + 1f;
            isQR = checkQR(barCodeType);
            QRCode = barCodeValue;

            Debug.Log(barCodeType);
            Debug.Log(barCodeValue);

        #if UNITY_ANDROID || UNITY_IOS
		            Handheld.Vibrate();
        #endif
        });
    }

    #region UI Buttons
    public void ClickQRScan()
    {
        if (!isRunning)
        {
            StartScanner();

            // Switch Text
            isRunning = true;
            Scan.GetComponentInChildren<Text>().text = "Stop Scan";
            //Scan.GetComponent<Text>().text = "Stop Scan";
        } else
        {
            StartCoroutine(StopCamera(() => {
            }));
            Scan.GetComponentInChildren<Text>().text = "Start Scan";
            isRunning = false;
        }
    }


    public void ClickCompare()
    {
        if (isQR)
        {
            TextHeader.text = "Does not match"; Debug.Log(TextHeader.text);
            foreach (string card in Cards)
            {
                if (QRCode == card) { TextHeader.text = "Matches"; Debug.Log(TextHeader.text); }
            }
        }
        else
        {
            TextHeader.text = "Not a QR Code"; Debug.Log(TextHeader.text);
        }
    }

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

    public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion

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
}

