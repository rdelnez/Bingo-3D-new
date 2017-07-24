using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public string cardString;
    public string cardName;
    public List<int> cardNumbers;

	// Use this for initialization
	void Start () {
        cardName = "";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initialiseCard(string card)
    {
        cardString = card;
        parseNumbers(cardString);
        displayNumbers();
    }

    private void displayNumbers()
    {
        string spriteName, objectName;
        int numSuffix;
        GameObject square;
        Vector2 origSize;

        for (int i = 0; i < cardNumbers.Count; i++) // Chose to keep this as a for loop instead of using a do while loop
        //for (int i = 0; i < 1; i++) // Chose to keep this as a for loop instead of using a do while loop
        {
            spriteName = "Ball";
            objectName = "";
            numSuffix = 0;
            if (i >= 0 && i < 5) { objectName += "B";  } // letter B
            if (i >= 5 && i < 10) { objectName += "I"; numSuffix -= 5; } // letter I
            if (i >= 10 && i < 15) { objectName += "N"; numSuffix -= 10; } // letter N
            if (i >= 15 && i < 20) { objectName += "G"; numSuffix -= 15; } // letter G
            if (i >= 20 && i < 25) { objectName += "O"; numSuffix -= 20; } // letter O 


            objectName += (i + 1 + numSuffix);
            //Debug.Log(square);
            spriteName += cardNumbers[i];
            square = this.transform.Find(objectName).gameObject;
            //square = GameObject.Find(objectName);
            Debug.Log(square);
            //origSize = square.GetComponent<SpriteRenderer>().size;
            square.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/BallSprites/"+ spriteName);
            square.transform.localScale = new Vector3(0.75f, 0.75f, 1);
            //square.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Ball3DTextures/" + spriteName);
            
            //square.gameObject.GetComponent<SpriteRenderer>().size = new Vector2(0.1f, 0.1f);
            //GetComponentInParent
        }
    }

    private void parseNumbers(string stream)
    {
        int x = 0;
        string tempNum = "";

        while (true)
        {

            if (stream[x] != ':')
            {
                cardName += stream[x];
                x++;
            }
            else
            {
                break;
            }
        }

        x++;

        for (int y = x; y < stream.Length; y++)
        {
            if (cardString[y] != ',')
            {
                tempNum += cardString[y];
            }
            else
            {
                cardNumbers.Add(int.Parse(tempNum));
                //Debug.Log(scannedCardNumber);
                tempNum = "";
            }
        }
    }
}



/*
            scannedCardName = "";
            int x = 0;
            while (true) {

                if (QRCode[x] != ':')
                {
                    scannedCardName += QRCode[x];
					if (x>9) {
						DisplayWrongQR();
						return;
					}
                    x++;
                }
                else {
                    break;
                }
            }


            scannedCardNumber.Clear();
            x++;
            string tempNum = "";
            for (int y = x; y < QRCode.Length; y++) {
                if (QRCode[y] != ',')
                {
                    tempNum += QRCode[y];
                }
                else
                {
                    scannedCardNumber.Add(int.Parse(tempNum));
                    Debug.Log(scannedCardNumber);
                    tempNum = "";
                }
            }



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

 */
