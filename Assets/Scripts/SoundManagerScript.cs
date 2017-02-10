using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {

	//public AudioSource BGMusic_Player;
	public AudioSource FX_Player;

	public int setNum;
	public string fileName;
	public float lowPitchRange;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange;            //The highest a sound effect will be randomly pitched.

	// Use this for initialization
	void Start () {
		setNum = 1;
		fileName = "jo";
		lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
		highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

		ChangeBGMusic ("bingobackground1");
	}
	public void Play_SFX (int ballNum)
	{
		//FX_Player.clip = Resources.Load<AudioClip> ("Voices/Set"+ setNum + "/" + fileName + ballNum);
		//FX_Player.Play ();
		FX_Player.PlayOneShot (Resources.Load<AudioClip> ("Voices/Set" + setNum + "/" + fileName + ballNum));
	}

	public void ChangeBGMusic(string tempBGMusic){
		FX_Player.clip = Resources.Load<AudioClip> ("Voices/BGM/"+tempBGMusic);
		FX_Player.loop = true;
		FX_Player.Play ();

	}
	// Update is called once per frame
	void Update () {
	
	}
}
