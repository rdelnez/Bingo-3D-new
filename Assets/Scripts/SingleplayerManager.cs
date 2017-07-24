using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerManager : MonoBehaviour {

	public GM GM_Script;
	public BallManager BM_Script;

	public int numPlayers;
	public List<string> cards;
    public GameObject cardPrefab;
    public List<GameObject> playerCards;
    public float cardWidth;
    public bool gameStarted;

	// Use this for initialization
	void Awake () {
		GM_Script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
		BM_Script = GameObject.FindGameObjectWithTag("BallManager").GetComponent<BallManager>();

        // Cards from actual bingo to test
        //Card 1:12,4,8,6,7,27,25,24,26,17,35,34,0,43,32,56,52,59,54,53,61,63,65,74,62,
        //Card 2:6,15,7,13,8,20,25,16,17,26,45,36,0,35,33,56,51,58,52,49,63,71,69,74,68,
        //Card 3:11,5,2,3,12,16,21,26,30,18,34,35,0,32,43,57,54,56,55,46,73,64,66,72,63,
        //Card 4:10,15,7,13,8,20,25,16,17,26,45,36,0,35,33,56,51,58,52,49,63,71,69,74,68
        //Card 5:14,5,3,11,1,29,18,19,23,21,36,39,0,37,41,48,58,50,55,57,69,72,64,68,70,
        //cardWidth = 1572;
        cardWidth = cardPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        numPlayers = 5;
        gameStarted = false;

        //cardPrefab.GetComponentInChildren<BingoBlueButton>().enabled = false;
        /* Component[] scripts = cardPrefab.GetComponentsInChildren<BingoBlueButton>();

        foreach (BingoBlueButton script in scripts)
        {
            script.enabled = false;
        } */

        //cardPrefab.
        //cardPrefab.Get

        //playercard.GetComponent<Vector3>().

    }
	/*
	// Update is called once per frame
	void Update () {
		
	}
	*/

	public void initialiseGame()
	{
        if (!gameStarted)
        {
            cards.Clear();
            for (int i = 1; i < numPlayers + 1; i++)
            {
                cards.Add(generateCard(i));
            }

            displayCards();
            gameStarted = true;
        }

        /// Display cards under here?
        /// Could duplicate bingo cards, use ballsprite numbers to represent card numbers for now
        /// Just need to zoom out camera if possible
        /// 
        /// bingoCardMiniPrefabs = Instantiate(Resources.Load("Prefabs/BingoCardMini"), new Vector3(8.8f, -6.77f, 0), Quaternion.identity) as GameObject;
        /// BingoCardBigScript = GameObject.FindGameObjectWithTag ("BingoCardBig").GetComponent<BingoCardBig> ();
        /// public BingoCardBig BingoCardBigScript;
    }

	private string generateCard(int player)
	{
		string tempString = "";
		int numberAmount = 25; // No more than 25 numbers on the board
		Dictionary<int,  int> generatedNumbers = new Dictionary<int, int>();
		int tempNumber = 0;


        /* for (int i = 1; i < 76; i++)
		{
			numberPool.Add(i);
		}*/

        tempString += "Card " + player + ":";

		for (int i = 0; i < numberAmount;) // Chose to keep this as a for loop instead of using a do while loop
		{
            if (i >= 0 && i < 5) { tempNumber = (int)Random.Range(1, 16); } // letter B
            if (i >= 5 && i < 10) { tempNumber = (int)Random.Range(16, 31); } // letter I
            if (i >= 10 && i < 15) { tempNumber = (int)Random.Range(31, 46); } // letter N
            if (i >= 15 && i < 20) { tempNumber = (int)Random.Range(46, 61); } // letter G
            if (i >= 20 && i < 25) { tempNumber = (int)Random.Range(61, 76); } // letter O 

            //tempNumber = (int)Random.Range(1, 76);
            if (!numberExists(generatedNumbers, tempNumber)) {
				if (i == 12) // Element 13 (the middle of the card) should be 0 as a freebie
				{
                    tempString += 0 + ",";
				}
				else
				{
					generatedNumbers.Add(tempNumber, tempNumber);
                    tempString += tempNumber + ",";
				}
				i++;
			}
		}

		Debug.Log(tempString);
		return tempString;
	}

    public void displayCards()
    {
        GameObject tempCard;
        
        //tempCard.transform.parent = this;
        for (int i = 0; i < numPlayers+0; i++)
        {
            tempCard = Instantiate(cardPrefab, new Vector3(-15f + (cardSpace(i)), -7.0f, -2.0f), Quaternion.identity) as GameObject;
            tempCard.GetComponent<AIManager>().initialiseCard(cards[i]);
            playerCards.Add(tempCard);
            //tempCard.transform.parent = GameObject.Find("Singleplayer").transform;
            //cardPrefab.GetComponent<GameObject>().transform.position.x
            // tempCard = Instantiate(cardPrefab, new Vector3(cardPrefab.GetComponent<GameObject>().transform.position.x + 1f + ((cardWidth * 0.4f) * i), cardPrefab.GetComponent<GameObject>().transform.position.y, cardPrefab.GetComponent<GameObject>().transform.position.z), Quaternion.identity);
            //cardPrefab.GetComponent<GameObject>().transform.position.x

            //playerCards.Add(Instantiate(cardPrefab, new Vector3(-15f + (cardSpace(i)), -7.0f, -2.0f), Quaternion.identity) as GameObject);

            //cardPrefab.GetComponent<>
        }

    }

	private bool numberExists(Dictionary<int,int> dictionary, int number)
	{
		return dictionary.ContainsKey(number);
	}

    private float cardSpace (float cardCount)
    {
        return (((cardWidth * 0.4f) * cardCount) * 1.5f);

    }
}
