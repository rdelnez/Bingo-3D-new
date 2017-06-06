using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManagerScript : MonoBehaviour {

	public Sprite sprite;
	public Sprite sprite1;
	public Image CreditsImage;

	void Start() {
	}

	public void HostGame () {
		//Application.LoadLevel (1);
        SceneManager.LoadScene(1);
	}

	public void Singleplayer () {
		//Application.LoadLevel ("Main Game");
	}

	public void Credits () {
		//Application.LoadLevel ("Main Game");
		CreditsImage.gameObject.SetActive (true);

	}

	public void CloseCredits() {
		//this.CreditsImage.GetComponent<Image> ().enabled = false;
		CreditsImage.gameObject.SetActive (false);
		//this.GetComponent<Image>().enabled = false;
	}

	public void Exit () {
		Application.Quit ();
	}

	void OnMouseOver() {
		if (Input.GetButtonDown ("Fire1")) {
			this.GetComponent<Image>().sprite = sprite1;
		}
		
		if (Input.GetButtonUp ("Fire1")) {
			this.GetComponent<Image>().sprite = sprite;
		} 
	}

	void OnMouseExit(){
		this.GetComponent<Image>().sprite = sprite;
	}
		

}
