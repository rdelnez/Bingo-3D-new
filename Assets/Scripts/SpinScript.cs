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

	public bool isSpinning;
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
		//LoadSprite ();

		isSpinning = false;
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
		//AnimationLength = TumblerAnimator.GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0).Length;

		//Debug.Log (AnimationSpeed);
		//TumblerAnimator.runtimeAnimatorController;
		//GM_script.SpinTumbler();

		/* for(int i = 0; i < TumblerAnimator.runtimeAnimatorController.animationClips.Length; i++)                 //For all animations
		{
			if(TumblerAnimator.runtimeAnimatorController.name == "Tumbling")        //If it has the same name as your clip
			{
				AnimationLength = TumblerAnimator.runtimeAnimatorController.animationClips[i].length;
			}
		} */


		/* if(TumblerAnimator != null) {
			UnityEditor.Animations.AnimatorController AnimatorController = TumblerAnimator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
			UnityEditor.Animations.AnimatorStateMachine StateMachine = AnimatorController.GetStateEffectiveBehaviours;
			
			for(int i = 0; i < StateMachine.states; i++) {
				UnityEditor.Animations.AnimatorState state = StateMachine.states[i];
				if(state.uniqueName == "Tumbling") {
					AnimationClip clip = state.GetMotion() as AnimationClip;
					if(clip != null) {
						AnimationLength = clip.length;
					}
				}
			}
			Debug.Log("Animation:"+AnimationLength); 
		} */
		/*
		Animator anim = obj.GetComponent<Animator>();
		 if(anim != null) {
		     UnityEditorInternal.AnimatorController ac = anim.runtimeAnimatorController as UnityEditorInternal.AnimatorController;
		     UnityEditorInternal.StateMachine sm = ac.GetLayer(0).stateMachine;
		     
		     for(int i = 0; i < sm.stateCount; i++) {
		         UnityEditorInternal.State state = sm.GetState(i);
		         if(state.uniqueName == track) {
		             AnimationClip clip = state.GetMotion() as AnimationClip;
		             if(clip != null) {
		                 length = clip.length;
		             }
		         }
		     }
		     Debug.Log("Animation:"+track+":"+length);
		 }
		 */

		Debug.Log (AnimationLength);


	}

	public void IncreaseSpeed() {
		if (AnimationSpeed < 2.5f) {
			TumblerAnimator.speed = AnimationSpeed + 0.25f;
			AnimationSpeed = GM_script.BingoTumbler.GetComponent<Animator> ().speed;
			Debug.Log (AnimationSpeed);
		}

	}

	public void DecreaseSpeed() {
		if (AnimationSpeed > 0.25f) {
			TumblerAnimator.speed = AnimationSpeed - 0.25f;
			AnimationSpeed = GM_script.BingoTumbler.GetComponent<Animator> ().speed;
			Debug.Log (AnimationSpeed);
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
					GM_script.menuLock=true;
					AutoTumblerRunning = true;
					SwitchImage();
					Debug.Log ("AutoTumbler");
					StartCoroutine(AutoTumbler());
				}
			} 

			else if (!TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling") && !isSpinning && !AutoTumblerEnabled) {
				ButtonSpriteRenderer.sprite = sprite;
				GM_script.menuLock=true;
				isSpinning = true;
				GM_script.SpinTumbler();
				SM_Script.PlayOther_SFX("tumbler");
				StartCoroutine(InstantiateBall());
				Debug.Log ("Spin");
			}
			
		}
		
		
	}
	
	void OnMouseExit(){
		if (!AutoTumblerEnabled) {
			ButtonSpriteRenderer.sprite = sprite;
		}
	}

	public void SwitchImage() {
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
		//yield return new WaitForSeconds(0.01f);
		while(isSpinning){

			yield return new WaitForSeconds(0.5f);

			if (!TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling")) {
				isSpinning = false;
			}
		}
		
		BM_script.InstantiateBallFromTumbler();

	}

	IEnumerator AutoTumbler(){
		//TumblerAnimator.GetCurrentAnimationClipState(0)
		//TumblerAnimator.GetCurrentAnimationClipState(0).
		//TumblerAnimator.GetCurrentAnimatorClipInfo(0).Length;
		// !TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling")
		while (AutoTumblerRunning) {
			//if (!isSpinning) {
			if (!TumblerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tumbling") && !isSpinning) {
				isSpinning = true;
				GM_script.SpinTumbler();
				SM_Script.PlayOther_SFX("tumbler");
				StartCoroutine(InstantiateBall());
				//AnimationLength = TumblerAnimator.GetCurrentAnimatorClipInfo(0).Length;
				//AnimationLength = TumblerAnimator.GetCurrentAnimatorStateInfo(0).length;
				//Debug.Log (AnimationLength);
			}
			yield return new WaitForSeconds(1.0f);
			//}

		}
	}



}
