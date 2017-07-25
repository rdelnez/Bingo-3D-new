using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCardScript : MonoBehaviour {


	public SpriteRenderer srWinnerFirstNum;
	public SpriteRenderer srWinnerSecondNum;
	public SpriteRenderer srCardFirstNum;
	public SpriteRenderer srCardSecondNum;
	public Vector3 endPos;
	public int moveSpeed;

	private void Awake()
	{
		endPos = this.transform.localPosition + new Vector3(0, 10, 0);
		moveSpeed = 1;
	}

	private void Update()
	{

		transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, Time.deltaTime * moveSpeed);
		if(this.transform.localPosition == endPos) {

			Destroy(this.gameObject);

		}

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




}
