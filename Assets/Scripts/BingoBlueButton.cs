using UnityEngine;
using System.Collections;

public class BingoBlueButton : MonoBehaviour {

	public bool isPressed;
	public Sprite sprite;
	//public GameObject button;
	public SpriteRenderer ButtonSpriteRenderer;
	public BingoCardBig bingoCardBigScript;
	public int buttonIndex;
	public GM GM_Script;


	// Use this for initialization
	void Start () {
		isPressed = false;
		//button = this;
		ButtonSpriteRenderer = this.GetComponent<SpriteRenderer> ();
		bingoCardBigScript = GameObject.FindGameObjectWithTag("BingoCardBig").GetComponent<BingoCardBig>();
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseOver() {
		//	if(!isPressed)
		//	{
			
		if (Input.GetButtonUp ("Fire1") && bingoCardBigScript.isCustomPattern) {
			if (!isPressed) {
				ButtonSpriteRenderer.enabled = true;
				isPressed = true;
				GM_Script.blueIsActiveList[buttonIndex] = true;

				//				menuButtonSpriteRenderer.sprite = sprite;
				//				menuButtonBGSpriteRenderer.sprite = spriteBG;	
			} else {
				ButtonSpriteRenderer.enabled = false;
				isPressed = false;

				GM_Script.blueIsActiveList[buttonIndex] = false;
			}	
			//	}
		
		}
	}
}
