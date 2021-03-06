﻿using UnityEngine;
using System.Collections;

public class BingoClose : MonoBehaviour {


	public GameObject GM;
	public GM GM_script;
	public SoundManagerScript SM_Script;
	public SpriteRenderer sprite;
	public Sprite sprite1;
	public Sprite sprite2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadAssets(){
		GM = GameObject.FindGameObjectWithTag ("GameManager");
		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();
		GM_script = GM.GetComponent<GM> ();
		sprite = this.GetComponent<SpriteRenderer> ();
		sprite1 = Resources.Load<Sprite> ("MenuButtons/BingoCloseUnpressed");
		sprite2 = Resources.Load<Sprite> ("MenuButtons/BingoClose");
	}

	void OnMouseOver() {
		if (Input.GetButtonDown("Fire1")){
			sprite.sprite = sprite2;
			SM_Script.PlayOther_SFX("menuopenclose");
		}
		
		if (Input.GetButtonUp("Fire1")){
			sprite.sprite = sprite1;
			GM_script.GoToGame ();
		}
		
		
	}
	
	void OnMouseExit(){
		sprite.sprite = sprite1;
		
		
		
	}
}
