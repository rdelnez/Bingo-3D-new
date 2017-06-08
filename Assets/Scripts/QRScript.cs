using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BarcodeScanner;
using BarcodeScanner.Scanner;

public class QRScript : MonoBehaviour {
    Scanner BarcodeScanner;

    // Use this for initialization
    void Start () {
        BarcodeScanner = new Scanner();
    }

    // Update is called once per frame
    void Update () {
        BarcodeScanner.Update();
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
}

