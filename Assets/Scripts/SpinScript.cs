using UnityEngine;
using System.Collections;

public class SpinScript : MonoBehaviour {


	public GameObject GM;
	public GM GM_script;

	public GameObject BM;
	public BallManager BM_script;

	public Sprite sprite;
	public Sprite sprite1;
	public bool isSpinning;

	public string ButtonSpriteString;
	public string ButtonSpriteString1;
	public SpriteRenderer ButtonSpriteRenderer;
	// Use this for initialization
	void Start () {
		//LoadSprite ();

		isSpinning = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>

	public void LoadSprite(){
		GM = GameObject.FindGameObjectWithTag ("GameManager");
		GM_script = GM.GetComponent<GM> ();

		BM = GameObject.FindGameObjectWithTag ("BallManager");
		BM_script = BM.GetComponent<BallManager> ();

		sprite = Resources.Load<Sprite>("GameButtons/"+ButtonSpriteString);
		sprite1 = Resources.Load<Sprite>("GameButtons/"+ButtonSpriteString1);

		ButtonSpriteRenderer = this.GetComponent<SpriteRenderer>();

		ButtonSpriteRenderer.sprite = sprite;

	}

	/// </summary>


	void OnMouseOver() {
		if (Input.GetButtonDown("Fire1")){
			ButtonSpriteRenderer.sprite = sprite1;

		}
		
		if (Input.GetButtonUp("Fire1")){


			ButtonSpriteRenderer.sprite = sprite;

			if (!GM_script.BingoTumbler.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("Tumbling") && !isSpinning) {
				isSpinning = true;
				GM_script.SpinTumbler();

				StartCoroutine(InstantiateBall());
				Debug.Log ("Spin");
			}
			
		}
		
		
	}
	
	void OnMouseExit(){
		ButtonSpriteRenderer.sprite = sprite;

		
		
		
	}

	IEnumerator InstantiateBall(){
		yield return new WaitForSeconds(0.1f);
		while(isSpinning){

			yield return new WaitForSeconds(0.3f);

			if (!GM_script.BingoTumbler.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("Tumbling")) {
				isSpinning = false;
			}

		}
		
		BM_script.InstantiateBallFromTumbler();

	}

}
