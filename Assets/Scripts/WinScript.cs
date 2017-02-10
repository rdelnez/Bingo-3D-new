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
	// Use this for initialization
	void Start () {
	
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
			
			
			//GM_script.SpinTumbler();
			Debug.Log ("Win");
			
		}
		
		
	}
	
	void OnMouseExit(){
		ButtonSpriteRenderer.sprite = sprite;
		
		
		
		
	}
}
