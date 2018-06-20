using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoboxcontroller : MonoBehaviour {

	// Use this for initialization
	public int contentID;

    public Text textContent;

	void Start () {
		textContent.text = ""+contentID;
		Debug.Log("infobox: "+contentID);

	}
	
	// Update is called once per frame
	// void Update () {
	// 	textContent.text = ""+contentID;
	// }
}
