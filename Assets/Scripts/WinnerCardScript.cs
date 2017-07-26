﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Awake()
	{
		QR_Script = GameObject.FindGameObjectWithTag("QR").GetComponent<QRScript>();
		endPos = this.transform.localPosition + new Vector3(0, 5, 0);
		nextPos = this.transform.localPosition + new Vector3(0, 2, 0);
		moveSpeed = 2;
		QR_Script.winnerAlive = true;
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

	private void Update()
	{
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, Time.deltaTime * moveSpeed);

		if (pastSpawnPoint == false)
		{
			if (this.transform.localPosition.y >= nextPos.y)
			{
				pastSpawnPoint = true;
				QR_Script.winnerAlive = false;
			}

		}
		if (this.transform.localPosition == endPos)
		{	
			Destroy(this.gameObject);
		}

	}

	}





