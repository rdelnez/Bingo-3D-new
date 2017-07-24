using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCardScript : MonoBehaviour {


	public SpriteRenderer srWinnerFirstNum;
	public SpriteRenderer srWinnerSecondNum;
	public SpriteRenderer srCardFirstNum;
	public SpriteRenderer srCardSecondNum;


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
