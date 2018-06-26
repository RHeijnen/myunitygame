using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultScript : MonoBehaviour {
    private Answer CurrentAnswer;
    public Text ProvidedAnswer;
    public Text Answer1Text;
    public Text Answer2Text;
    public Text Answer3Text;
    public Text Toelichting;
    // Use this for initialization
    void Start () {
		if(GameManager.Answers.Count == 0)
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
        CurrentAnswer = GameManager.Answers[0];
        Answer1Text.text = CurrentAnswer.Question.answer[0];
        Answer2Text.text = CurrentAnswer.Question.answer[1];
        Answer3Text.text = CurrentAnswer.Question.answer[2];
        Toelichting.text = CurrentAnswer.Question.explaination;
        ProvidedAnswer.text = "You answered this question with option " + CurrentAnswer.GivenAnswer.ToString() + ". \n The correct result was " + CurrentAnswer.Question.correctAnswer + ".";
        GameManager.Answers.RemoveAt(0);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp("space"))
        {
            SceneManager.LoadScene("resultScene", LoadSceneMode.Single);
        }
	}
}
