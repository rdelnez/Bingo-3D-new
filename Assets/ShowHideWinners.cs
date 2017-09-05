using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideWinners : MonoBehaviour
{
    bool isTrue;

    // Use this for initialization
    void Start()
    {
        isTrue = false;

    }

    void OnMouseOver()
    {

		Debug.Log("Button Clicked");
        isTrue = true;
        if (Input.GetButtonDown("Fire1") && isTrue)
        {
            isTrue = true;
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameButtons/show_winners_NOTpressed");
        }

       if (Input.GetButtonUp("Fire1") && !isTrue)
        {
            isTrue = false;
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameButtons/hide_winners_NOTpressed");
        }

    }

}



