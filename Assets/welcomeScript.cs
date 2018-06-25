﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class welcomeScript : MonoBehaviour {
    public Text welcomText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("space"))
        {
            welcomText.text = "";
            SceneManager.LoadScene("main", LoadSceneMode.Additive);
        }
	}
}