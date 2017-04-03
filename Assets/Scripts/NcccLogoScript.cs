using UnityEngine;
using System.Collections;

public class NcccLogoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (RotateLogo());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator RotateLogo(){
		while(true){
			transform.localEulerAngles += new Vector3(0,2,0);
			yield return new WaitForSeconds(0.03f);
		}

	}
}
