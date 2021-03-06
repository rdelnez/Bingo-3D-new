﻿using UnityEngine;
using System.Collections;

public class MainMenuButtonScript : MonoBehaviour {

	public GameObject GM;
	public GM GM_script;
	public SoundManagerScript SM_Script;
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
		sprite1 = Resources.Load<Sprite> ("MainMenuButtons/gotomainmenu1");
		sprite2 = Resources.Load<Sprite> ("MainMenuButtons/gotomainmenu2");
		SM_Script = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void OnMouseOver() {
		if (Input.GetButtonDown("Fire1")){
			sprite.sprite = sprite2;
			SM_Script.PlayOther_SFX("menuopenclose");
		}
		
		if (Input.GetButtonUp("Fire1") && !GM_script.menuLock) {
			sprite.sprite = sprite1;
			GM_script.GotoMainMenu();
		}
		
	}
	
	void OnMouseExit(){
		sprite.sprite = sprite1;
		
	}
}
