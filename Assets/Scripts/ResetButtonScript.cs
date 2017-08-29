using UnityEngine;
using System.Collections;

public class ResetButtonScript : MonoBehaviour {


	public GM GM_Script;
	public SoundManagerScript SM_Script;
	public SpriteRenderer sprite;
	public Sprite sprite1;
	public Sprite sprite2;
	public int tempBGRandomNum;
	// Use this for initialization
	void Start () {

		GM_Script = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GM>();
		SM_Script = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManagerScript>();

		sprite = this.GetComponent<SpriteRenderer> ();
		sprite1 = Resources.Load<Sprite> ("MenuButtons/BingoResetUnpressed");
		sprite2 = Resources.Load<Sprite> ("MenuButtons/BingoReset");

		tempBGRandomNum = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetGame(){

	}


	void OnMouseOver() {
		if (Input.GetButtonDown("Fire1")){
			sprite.sprite = sprite2;
		}
		
		if (Input.GetButtonUp("Fire1")){
			sprite.sprite = sprite1;
			GM_Script.ResetGame();
			//START for changing BG Music
			tempBGRandomNum = (int)Random.Range(1, 5);
			SM_Script.ChangeBGMusic("Background" + tempBGRandomNum);
			//END for changing BG Music
		}


	}
	
	void OnMouseExit(){
		sprite.sprite = sprite1;
		
		
		
	}
}
