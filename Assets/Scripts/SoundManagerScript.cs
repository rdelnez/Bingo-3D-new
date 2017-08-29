using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {

	//public AudioSource BGMusic_Player;
	public AudioSource FX_Player;
	public AudioSource BG_Player;

	public int setNum;
	public string fileName;
	public float lowPitchRange;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange;            //The highest a sound effect will be randomly pitched.
	public int tempBGRandomNum;
	public AudioClip tempAudioClip;

	// Use this for initialization
	void Start () {
		setNum = 1;
		fileName = "jo";
		lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
		highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


		tempBGRandomNum = (int)Random.Range(1, 5);
		ChangeBGMusic("Background" + tempBGRandomNum);
		//		ChangeBGMusic ("bingobackground"+ tempBGRandomNum);
	}
	public void Play_SFX (int ballNum)
	{
		//FX_Player.clip = Resources.Load<AudioClip> ("Voices/Set"+ setNum + "/" + fileName + ballNum);
		//FX_Player.Play ();
		FX_Player.PlayOneShot (Resources.Load<AudioClip> ("Voices/Set" + setNum + "/" + fileName + ballNum));
	}

	public void PlayOther_SFX (string tempStringName)
	{
		//FX_Player.clip = Resources.Load<AudioClip> ("Voices/Set"+ setNum + "/" + fileName + ballNum);
		//FX_Player.Play ();
		FX_Player.PlayOneShot (Resources.Load<AudioClip> ("SFX/" + tempStringName));
	}


	public void ChangeBGMusic(string tempBGMusic){

		//AudioSource.PlayClipAtPoint(footSteps1, transform.position, volume);


		BG_Player.clip = Resources.Load<AudioClip> ("Voices/BGM/"+tempBGMusic);
		BG_Player.loop = true;
		BG_Player.Play();

		//tempAudioClip = Resources.Load<AudioClip>("Voices/BGM/" + tempBGMusic) as AudioClip;
		
		//FX_Player.PlayClipAtPoint(tempAudioClip, transform.position, 0.3f);
		

	}
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseVolume() {
		//if (FX_Player.volume < 100) {
		//FX_Player.volume = System.Math.Round((double)(FX_Player.volume + 0.05f), 2);
		FX_Player.volume += 0.05f;
		//}
	}

	public void DecreaseVolume() {
		//FX_Player.volume = System.Math.Round((double)(FX_Player.volume - 0.05f), 2);
		FX_Player.volume -= 0.05f;
	}
}
