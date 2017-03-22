using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayManagerScript : MonoBehaviour {
	
	public int tempNum;
	public char tempChar;

	public string scoreString;


	public List<Sprite> listNumImage;
	public Sprite num0;
	public Sprite num1;
	public Sprite num2;
	public Sprite num3;
	public Sprite num4;
	public Sprite num5;
	public Sprite num6;
	public Sprite num7;
	public Sprite num8;
	public Sprite num9;
	
	// Use this for initialization
	void Start () {

		listNumImage = new List<Sprite> ();
		listNumImage.Add (num0);
		listNumImage.Add (num1);
		listNumImage.Add (num2);
		listNumImage.Add (num3);
		listNumImage.Add (num4);
		listNumImage.Add (num5);
		listNumImage.Add (num6);
		listNumImage.Add (num7);
		listNumImage.Add (num8);
		listNumImage.Add (num9);

	//	DisplayImageNum (GameObject.FindGameObjectWithTag("VolumeDisplay").transform.GetChild(0).gameObject, GameObject.FindGameObjectWithTag("VolumeDisplay").transform.GetChild(1).gameObject, 99);

	}
	
	// Update is called once per frame
	void Update () {

	}
	

	public void DisplayImageNum(GameObject num2, GameObject num1, int tempChangeValue){

		scoreString = tempChangeValue.ToString ();
		
		//Debug.Log (scoreString.Length);

		if(scoreString.Length>2){
			scoreString = "99";
		}
		
		if (scoreString.Length == 1) {
			tempChar = scoreString[0];
			tempNum = (int)char.GetNumericValue(tempChar);
			num1.GetComponent<SpriteRenderer> ().sprite = listNumImage [tempNum];
			num2.GetComponent<SpriteRenderer> ().sprite = listNumImage [0];
			
		} else {
			
			tempChar = scoreString[1];
			tempNum = (int)char.GetNumericValue(tempChar);
			num1.GetComponent<SpriteRenderer> ().sprite = listNumImage[tempNum];
			//scoreNum2.GetComponent<Image> ().sprite = listNumImage[scoreString[0]];
			
			tempChar = scoreString[0];
			tempNum = (int)char.GetNumericValue(tempChar);
			num2.GetComponent<SpriteRenderer> ().sprite = listNumImage[tempNum];

		}

	}

}
