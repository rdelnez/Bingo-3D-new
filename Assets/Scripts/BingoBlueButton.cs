using UnityEngine;
using System.Collections;

public class BingoBlueButton : MonoBehaviour {

	public bool isPressed;
	public Sprite sprite;
	public SoundManagerScript SM_Script;
	//public GameObject button;
	public SpriteRenderer ButtonSpriteRenderer;
	public BingoCardBig bingoCardBigScript;
	public int buttonIndex;
	public GM GM_Script;
	public bool interactable;


	// Use this for initialization
	void Start () {
		isPressed = false;
		//interactable = true;
		//button = this;
		ButtonSpriteRenderer = this.GetComponent<SpriteRenderer> ();
		bingoCardBigScript = GameObject.FindGameObjectWithTag("BingoCardBig").GetComponent<BingoCardBig>();
		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();
		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseOver() {
		//	if(!isPressed)
		//	{

		if(!interactable){
			return;
		}

		if (Input.GetButtonUp ("Fire1") && bingoCardBigScript.isCustomPattern) {
			if (!isPressed) {
				ButtonSpriteRenderer.enabled = true;
				isPressed = true;
				SM_Script.PlayOther_SFX("bubblepop");
				GM_Script.blueIsActiveList[buttonIndex] = true;

				//				menuButtonSpriteRenderer.sprite = sprite;
				//				menuButtonBGSpriteRenderer.sprite = spriteBG;	
			} else {
				ButtonSpriteRenderer.enabled = false;
				isPressed = false;
				SM_Script.PlayOther_SFX("bubblepop");
				GM_Script.blueIsActiveList[buttonIndex] = false;
			}	
			//	}
		
		}
	}
}
