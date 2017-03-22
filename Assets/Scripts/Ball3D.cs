using UnityEngine;
using System.Collections;

public class Ball3D : MonoBehaviour {

	public int value;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag ("RecallNumberPlacement")){
			
			//Vector3.Distance(transform.eulerAngles, new Vector3(90,270,0)) > 5.0f
			
			
			//Debug.Log ("ball got Hit ontrigger");
			//Destroy (this.gameObject);
			
		}
	}

	public void BecomeHexChild(Transform parentHex)
	{
		this.transform.parent = parentHex;
		this.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.05f);
	}

	public void DestroyObject(){
		Destroy (this.gameObject);
	}
}
