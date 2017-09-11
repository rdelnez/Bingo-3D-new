using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerCardScript : MonoBehaviour {


	public SpriteRenderer srWinnerFirstNum; // left 
	public SpriteRenderer srWinnerSecondNum; // right
	public SpriteRenderer srCardFirstNum; // left
	public SpriteRenderer srCardSecondNum; // right
	public Vector3 endPos; // destroy here
	public Vector3 nextPos; // pass this to spawn next
	public int moveSpeed;
	public QRScript QR_Script;
	public bool pastSpawnPoint; // So it sets true only once.	
	public GM GM_Script;

	//public List<List<List<int>>> winnerList_Stack;


	private void Awake()
	{
		QR_Script = GameObject.FindGameObjectWithTag("QR").GetComponent<QRScript>();
		//GM_Script = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
		endPos = this.transform.localPosition + new Vector3(0, -5, 0);
	//	nextPos = this.transform.localPosition + new Vector3(0, 2, 0);
		moveSpeed = 2;
		//QR_Script.winnerAlive = true;
	}


	void Start()
	{
		//GM_Script = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
		//winnerList_Stack = new List<List<List<int>>>();

		StartCoroutine(MoveGraphics());
		//GM_Script.winnerObjects.Add(this.gameObject);
	}


	public void SetWinnerNumber(int xy, int firstWinnerNum, int secondWinnerNum)
	{
		string inputNumString;
		if (xy > 9)
		{
			inputNumString = xy.ToString();
		}
		else
		{
			inputNumString = "0" + xy;
		}
		
		srWinnerFirstNum.sprite = Resources.Load<Sprite>("QRCode/" + firstWinnerNum);
		srWinnerSecondNum.sprite = Resources.Load<Sprite>("QRCode/" + secondWinnerNum);
		srCardFirstNum.sprite = Resources.Load<Sprite>("QRCode/" + inputNumString[0]);
		srCardSecondNum.sprite = Resources.Load<Sprite>("QRCode/" + inputNumString[1]);


	}



	IEnumerator MoveGraphics() {

		while (true)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, Time.deltaTime * moveSpeed);

			//if (pastSpawnPoint == false)
			//{
				//if (this.transform.localPosition.y >= nextPos.y)
				//{
				//	pastSpawnPoint = true;
				//	QR_Script.winnerAlive = false;
				//}

			//}
			if (this.transform.localPosition == endPos)
			{
				//GM_Script.destroyWinners();
				DestroyWinner();
			}


			yield return new WaitForSeconds(0.03f);
		}
	}


	public void DestroyWinner()
	{
		Destroy(this.gameObject);
	}

}





