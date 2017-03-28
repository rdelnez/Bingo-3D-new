using UnityEngine;
using System.Collections;

public class SpinScript : MonoBehaviour {


	public GameObject GM;
	public GM GM_script;
	public SoundManagerScript SM_Script;

	public GameObject BM;
	public BallManager BM_script;

	public Sprite sprite;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;

	public bool isTumbling;
	public bool AutoTumblerEnabled;
	public bool AutoTumblerRunning;

	public string ButtonSpriteString;
	public string ButtonSpriteString1;
	public string ButtonSpriteString2;
	public string ButtonSpriteString3;
	public SpriteRenderer ButtonSpriteRenderer;

	public float AnimationSpeed;
	public float AnimationLength; // Not used
	public Animator TumblerAnimator;

	// Use this for initialization
	void Start () {
		
		isTumbling = false;
		AutoTumblerEnabled = false;
		AutoTumblerRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>

	public void LoadSprite(){
		GM = GameObject.FindGameObjectWithTag ("GameManager");
		GM_script = GM.GetComponent<GM> ();
		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();

		BM = GameObject.FindGameObjectWithTag ("BallManager");
		BM_script = BM.GetComponent<BallManager> ();

		sprite = Resources.Load<Sprite>("GameButtons/"+ButtonSpriteString);
		sprite1 = Resources.Load<Sprite>("GameButtons/"+ButtonSpriteString1);
		sprite2 = Resources.Load<Sprite>("GameButtons/"+ButtonSpriteString2);
		sprite3 = Resources.Load<Sprite>("GameButtons/"+ButtonSpriteString3);

		ButtonSpriteRenderer = this.GetComponent<SpriteRenderer>();
		ButtonSpriteRenderer.sprite = sprite;

		TumblerAnimator = GM_script.BingoTumbler.GetComponent<Animator> ();
		AnimationSpeed = TumblerAnimator.GetComponent<Animator> ().speed;
		
	}

	public void IncreaseSpeed() {
		if (AnimationSpeed < 2.5f) { // Displayed as 99 on Menu
			TumblerAnimator.speed = AnimationSpeed + 0.25f;
			AnimationSpeed = GM_script.BingoTumbler.GetComponent<Animator> ().speed;
			//Debug.Log ("Playback Speed: " + AnimationSpeed);
		}

	}

	public void DecreaseSpeed() {
		if (AnimationSpeed > 0.25f) { // Displayed as 10 on Menu
			TumblerAnimator.speed = AnimationSpeed - 0.25f;
			AnimationSpeed = GM_script.BingoTumbler.GetComponent<Animator> ().speed;
			//Debug.Log ("Playback Speed: " + AnimationSpeed);
		}
	}
	/// </summary>


	void OnMouseOver() {
		if (Input.GetButtonDown("Fire1")){
			if (AutoTumblerEnabled) {
				SwitchImage();
			} else {
				ButtonSpriteRenderer.sprite = sprite1;
			}
		}
		
		if (Input.GetButtonUp("Fire1")){
			//Debug.Log (AnimationLength);

			if (AutoTumblerEnabled) {
				if (AutoTumblerRunning){
					AutoTumblerRunning = false;
					SwitchImage();
				} else {
					AutoTumblerRunning = true;
					SwitchImage();
					Debug.Log ("AutoTumbler Starting");

					StartCoroutine(AutoTumbler());
				}
			} 
			else if (!TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling") && !isTumbling && !AutoTumblerEnabled) {
				ButtonSpriteRenderer.sprite = sprite;
				if (!BM_script.NoMoreNumbers ()) {
					//GM_script.SpinTumbler();
					//SM_Script.PlayOther_SFX("tumbler");
					StartCoroutine(InstantiateBall());
				}
				// Debug.Log ("Spin");
			}

		}
		
	}
	
	void OnMouseExit(){
		if (!AutoTumblerEnabled) {
			ButtonSpriteRenderer.sprite = sprite;
		}
	}
	public void SwitchImage() { // Used only when AutoTumbler is running, sprite1 is not used
		if (AutoTumblerEnabled) {
			if (!AutoTumblerRunning) {
				ButtonSpriteRenderer.sprite = sprite2;
			} else { 
				ButtonSpriteRenderer.sprite = sprite3;
			}
		} else {
			ButtonSpriteRenderer.sprite = sprite;
		}
	}

	IEnumerator InstantiateBall(){
		BM_script.ballsMoving++;
		GM_script.SpinTumbler();
		SM_Script.PlayOther_SFX("tumbler");
		isTumbling = true;
		GM_script.menuLock = true;
		//yield return new WaitForSeconds(0.01f);
		while(isTumbling){
			yield return new WaitForSeconds(0.1f);

			if (!TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling")) {
				isTumbling = false;
			}
		}

		BM_script.InstantiateBallFromTumbler ();

	}

	IEnumerator AutoTumbler(){
		while (AutoTumblerRunning) {
			if (!TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling") && !isTumbling) {
				if (!BM_script.NoMoreNumbers ()) {
					StartCoroutine(InstantiateBall());
				}

				else {
					AutoTumblerRunning = false;
					SwitchImage();
				}
			}
			yield return new WaitForSeconds(1.0f);
		}
	}



}
