using UnityEngine;
using System.Collections;

public class MenuTitle : MonoBehaviour {


	public GameObject GM;
	public GM GM_script;
	public SpriteRenderer sprite;
	public Sprite sprite1;
	public Sprite sprite2;
	// Use this for initialization
	void Start () {

	
	}

	void Awake(){



	}

	public void LoadAssets(){
		GM = GameObject.FindGameObjectWithTag ("GameManager");
		GM_script = GM.GetComponent<GM> ();
		sprite = this.GetComponent<SpriteRenderer> ();
		sprite1 = Resources.Load<Sprite> ("MenuButtons/menulittlev2");
		sprite2 = Resources.Load<Sprite> ("MenuButtons/menulittle");
	}
	
	// Update is called once per frame
	void Update () {


			
	}

	void OnMouseOver() {
		if (Input.GetButtonDown("Fire1")){
			sprite.sprite = sprite2;
		}
		
		if (Input.GetButtonUp("Fire1")){
			sprite.sprite = sprite1;
			GM_script.GoToMenu ();
		}


	}

	void OnMouseExit(){
		sprite.sprite = sprite1;



	}
}
