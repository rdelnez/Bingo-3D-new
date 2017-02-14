using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	public GameObject GM;
	public GM GM_script;
	
	public Sprite sprite;
	public Sprite sprite1;
	
	public string ButtonSpriteString;
	public string ButtonSpriteString1;
	public SpriteRenderer ButtonSpriteRenderer;

	public Object winBingoParticle;
	public bool winParticleAnimating;
	public GameObject winParticleGameObject;
	// Use this for initialization
	void Start () {
		winParticleAnimating = false;
		winParticleGameObject = GameObject.FindGameObjectWithTag ("WinParticle");
		winParticleGameObject.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	
	public void LoadSprite(){
		GM = GameObject.FindGameObjectWithTag ("GameManager");
		GM_script = GM.GetComponent<GM> ();
		
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
			
			
			if(winParticleAnimating == false){

				winParticleAnimating =true;
			//	winParticleGameObject = Instantiate(winBingoParticle, new Vector3(6.42f, -9.5f, -4.9f), Quaternion.identity)as GameObject;
				winParticleGameObject.SetActive (true);
			}else{
				winParticleAnimating =false;
				winParticleGameObject.SetActive (false);
			}

			
		}
		
		
	}
	
	void OnMouseExit(){
		ButtonSpriteRenderer.sprite = sprite;
		
		
		
		
	}

}
