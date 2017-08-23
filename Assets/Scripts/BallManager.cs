using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{


	public List<List<GameObject>> tempSmallHexList;
	public Object ball3DprefabsLoaded;
	public GameObject Ball3DPrefabs;
	public GameObject hexTempObject;

	public int tempRandNum;
	public int ballsMoving;
	public Texture testTexture;
	public GameObject GM_Object;
	public GM GM_Script;
	public QRScript QR_Script;

	public List<int> poolNumberList;
	public List<int> poolActiveNumberList;
	public List<GameObject> activeBallList;
	public List<int> dropBallList;

	// Use this for initialization
	void Start()
	{
		GM_Object = GameObject.FindGameObjectWithTag("GameManager");
		GM_Script = GM_Object.GetComponent<GM>();

		tempSmallHexList = GM_Script.hexList;


		ball3DprefabsLoaded = Resources.Load("Prefabs/Ball3D");
		//BingoCard = Instantiate(Resources.Load("Prefabs/BingoCard"), new Vector3(19.52f, -4.83f, 0), Quaternion.identity) as GameObject;

		poolNumberList = new List<int>();
		poolActiveNumberList = new List<int>();
		dropBallList = new List<int>(); // Should not be more than 1 number at a time
		activeBallList = new List<GameObject>();

		RePopulateNumberList();

		ballsMoving = 0;
	}

	// Update is called once per frame
	void Update()
	{


	}

	public void RePopulateNumberList()
	{
		poolNumberList.Clear();
		poolActiveNumberList.Clear();
		for (int x = 1; x < 76; x++)
		{
			poolNumberList.Add(x);
		}

	}

	public int GetRandomNum()
	{
		return (int)Random.Range(0, poolNumberList.Count-60);
	}

	public void InstantiateBallFromTumbler()
	{
		//ballsMoving++;
		tempRandNum = GetRandomNum();
		testTexture = Resources.Load("Textures/Ball3DTextures/Ball" + poolNumberList[tempRandNum]) as Texture;

		Ball3DPrefabs = Instantiate(ball3DprefabsLoaded, new Vector3(-10.6f, 9.19f, -2), Quaternion.identity) as GameObject;    //START Instatiate from tumbler
		Ball3DPrefabs.transform.localEulerAngles = new Vector3(-90, 0, 0);
		//Ball3DPrefabs.transform.eulerAngles = new Vector3 (-90, 0, 0);														//END Instatiate from tumbler
		Ball3DPrefabs.GetComponent<Ball3D>().value = poolNumberList[tempRandNum];
		Ball3DPrefabs.GetComponent<Renderer>().material.SetTexture("_MainTex", testTexture);

		activeBallList.Add(Ball3DPrefabs);

		poolActiveNumberList.Add(poolNumberList[tempRandNum]);
		poolNumberList.RemoveAt(tempRandNum);

		QR_Script.CompareStoredCardsMain(poolActiveNumberList);
		// Debug.Log ("Balls Moving when button is pressed: " + ballsMoving);
	}

	public void DisplayGameNumbers(GameObject tempBall)
	{
		//tempSmallHexList.transform.localPosition;

		hexTempObject = GameObject.FindGameObjectWithTag("Hex" + tempBall.GetComponent<Ball3D>().value);
		hexTempObject.GetComponent<Hex>().hasChild = true;
		tempBall.GetComponent<Rigidbody>().isKinematic = true;
		//tempBall.transform.localPosition = hexTempObject.transform.localPosition;
		tempBall.transform.SetParent(hexTempObject.transform);
		tempBall.transform.localPosition = new Vector3(0, 0, -1.5f);
		tempBall.transform.localEulerAngles = new Vector3(-90, 0, 0);
		tempBall.transform.localScale = Vector3.one * 0.83f;

	}

	public void DisplayRecallNumbers()
	{

	}

	public bool NoBallsMoving()
	{
		bool result = false;
		if (ballsMoving == 0)
		{
			result = true;
		}
		return result;
	}

	public void BallStoppedMoving()
	{
		ballsMoving--;
	}

	public void ClearBalls()
	{

		/*foreach (GameObject ball in activeBallList) {

		}*/

		for (int x = 0; x < activeBallList.Count; x++)
		{
			activeBallList[x].transform.parent.GetComponent<Hex>().hasChild = false;
			activeBallList[x].transform.SetParent(null);
			activeBallList[x].GetComponent<Ball3D>().DestroyObject();

		}

		activeBallList.Clear();
	}

	public bool NoMoreNumbers()
	{
		bool result = false;
		if (poolNumberList.Count <= 0)
		{
			result = true;
		}
		// Debug.Log ("Balls Moving after collision: " + ballsMoving);
		return result;
	}


}
