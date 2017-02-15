using UnityEngine;
using System.Collections;

public class BingoBlueButton : MonoBehaviour {

	public bool isPressed;
	public Sprite sprite;
	//public GameObject button;
	public SpriteRenderer ButtonSpriteRenderer;


	// Use this for initialization
	void Start () {
		isPressed = false;
		//button = this;
		ButtonSpriteRenderer = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseOver() {
		//	if(!isPressed)
		//	{
		if (Input.GetButtonDown ("Fire1")) {

			//				menuButtonSpriteRenderer.sprite = sprite1;
			//				menuButtonBGSpriteRenderer.sprite = spriteBG1;
			//				MM_Script.tempButtonName = buttonName;
			//				MM_Script.CallFunction ();
			
		}
		
		if (Input.GetButtonUp ("Fire1")) {
			if (!isPressed) {
				ButtonSpriteRenderer.enabled = false;
				isPressed = true;
				//				menuButtonSpriteRenderer.sprite = sprite;
				//				menuButtonBGSpriteRenderer.sprite = spriteBG;	
			} else {
				ButtonSpriteRenderer.enabled = true;
				isPressed = false;
			}	
			//	}
		
		}
	}
}
