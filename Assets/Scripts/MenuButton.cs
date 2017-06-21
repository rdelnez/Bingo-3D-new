using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public Vector3 origPos;
	public Vector3 menuPos;
	public float speedMoving;
	public string buttonName;

	public bool isPressed;
	public bool moving;
	public bool movingToGame;
	public bool IsVolumeButton;
	public bool IsDisplay;
	public Sprite spriteBG1;
	public Sprite spriteBG;
	
	/// <Start Button Initilization Variables>
	public GameObject GM;
	public GM GM_script;
	public MenuManagerScript MM_Script;
	public SoundManagerScript SM_Script;
	public Sprite sprite1;
	public Sprite sprite;
	public string menuButtonSprite;
	public string menuButtonSprite1;
	public SpriteRenderer menuButtonSpriteRenderer;

	public SpriteRenderer menuButtonBGSpriteRenderer;

	/// </End button Initilization>

	void Awake(){
		isPressed = false;
	}
	// Use this for initialization
	void Start () {

		//
		moving = false;
		movingToGame = false;
		IsVolumeButton = false;
		speedMoving = 50.0f;
		//GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();
		MM_Script = GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<MenuManagerScript>();
		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();

		//sprite = Resources.Load("Patterns/"+menuButtonSprite) as Sprite;
		//menuButtonSpriteRenderer.sprite = sprite;
	
	}

	public void LoadSprite(){
		if (!IsDisplay) {
			sprite = Resources.Load<Sprite> ("Patterns/" + menuButtonSprite);
			sprite1 = Resources.Load<Sprite> ("Patterns/" + menuButtonSprite1);

			spriteBG = Resources.Load<Sprite> ("Patterns/menuButton");
			//spriteBG1 = Resources.Load<Sprite> ("Patterns/menuButton1");
			spriteBG1 = Resources.Load<Sprite> ("Patterns/yellowhex");
			//spriteBG1 = Resources.Load<Sprite>("Patterns/yellowhex");
			//sprite = Resources.Load("Patterns/crosspattern") as Sprite;

			/////
			menuButtonSpriteRenderer = transform.GetChild (0).GetComponent<SpriteRenderer> ();
			menuButtonBGSpriteRenderer = this.GetComponent<SpriteRenderer> ();
			
			
			transform.GetChild (0).transform.localScale = new Vector3 (0.58f, 0.58f, 0.58f);
			//menuButtonSpriteRenderer = GetComponent<SpriteRenderer> ();
			menuButtonSpriteRenderer.sprite = sprite;
			//////
			/// 
		} else {
			//sprite = Resources.Load<Sprite> ("Patterns/" + menuButtonSprite);
			spriteBG = Resources.Load<Sprite> ("Patterns/whitehex");

		}

	
		//transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

	}
	
	// Update is called once per frame
	void Update () {

		StartMoving ();
		StartMovingToGame ();
	
	}

	void StartMoving(){
		if (moving)
		{
			//menuTitlePos = new Vector3(transform.localPosition.x, menuPos.y, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, menuPos) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, menuPos, Time.deltaTime*speedMoving);
                //transform.localEulerAngles += new Vector3(1,0,0);
                MM_Script.QRButton.gameObject.SetActive(true);
            }
			else {
				transform.localPosition = menuPos;
				moving = false;
            }
		}
		
	}

	void StartMovingToGame(){
		if (movingToGame)
		{
			//menuTitlePos = new Vector3(transform.localPosition.x, menuPos.y, transform.localPosition.z);
			if (Vector3.Distance(transform.localPosition, origPos) > 0.1f)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, origPos, Time.deltaTime*speedMoving);
                //transform.localEulerAngles += new Vector3(1,0,0);
                
            }
			else {
				transform.localPosition = origPos;
				movingToGame = false;
				this.gameObject.SetActive (false);
                MM_Script.QRButton.gameObject.SetActive(false);
			}
		}
		
	}

	///Start Buttons

	void OnMouseOver() {
		if(!isPressed && !IsDisplay)
		{
			if (Input.GetButtonDown ("Fire1")) {
				VolumeButton ();
				PressButton();
				MM_Script.tempButtonName = buttonName;
				SM_Script.PlayOther_SFX("menubuttonsounds");
				MM_Script.CallFunction ();

			}
		
			if (Input.GetButtonUp ("Fire1")) {
				menuButtonSpriteRenderer.sprite = sprite;
				menuButtonBGSpriteRenderer.sprite = spriteBG;

			//	if(IsVolumeButton){
			//		DePressButton ();
			//		isPressed=false;
			//	}
				

			}
		}
		
	}
	
	void OnMouseExit(){
		//if (!isPressed || buttonName == "volume1") {
		//if (buttonName == "volume1") {
		//if (!isPressed && buttonName != "Volume") {
	//	if(IsVolumeButton){
	//		DePressButton ();
	//		isPressed=false;
	//	}

		if (!isPressed && !IsDisplay) {
			DePressButton ();
			//menuButtonSpriteRenderer.sprite = sprite;
			//menuButtonBGSpriteRenderer.sprite = spriteBG;
			//}

			//if (buttonName == "volume1") {

		} else {
			//PressButton();
		}
	}

	public void DePressButton(){
		menuButtonSpriteRenderer.sprite = sprite;
		menuButtonBGSpriteRenderer.sprite = spriteBG;

	}

	public void PressButton(){
		if (!IsVolumeButton) {
			isPressed = true;
		}
		menuButtonSpriteRenderer.sprite = sprite1;
		menuButtonBGSpriteRenderer.sprite = spriteBG1;
	}

	private void VolumeButton() {
		//bool result = false;
		if (buttonName == "Volume1" || buttonName == "Volume2" || buttonName == "AutoTumbler4" || buttonName == "AutoTumbler5") {
			IsVolumeButton = true;
		}

	}

	private void CheckIfDisplaySmallHex(){
		if(buttonName == "AutoTumbler3" || buttonName == "Volume3"){
			IsDisplay = true;
		}
	}

	///End Buttons 
}
