using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor;

public class Question
{
    public string question = "";
    public List<string> answer = new List<string>();
    public int correctAnswer = -1;
}


public class questionLoader : MonoBehaviour
{

    public Text questionText;
    public Text anwer1Text;
    public Text anwer2Text;
    public Text anwer3Text;
    public Text anwer4Text;
    private int correctValue = -1;


    public Question q;

    string correctAnswerString;

    // Use this for initialization
    void Start()
    {
        // change content based on this int 
        Debug.Log(GameManager.questionID);
        if (GameManager.QuestionList.Count == 0)
        {
            endGame();
            return;
        }

        q = GameManager.QuestionList[0];
        questionText.text = q.question;
        anwer1Text.text = q.answer[0];
        anwer2Text.text = q.answer[1];
        anwer3Text.text = q.answer[2];
        anwer4Text.text = q.answer[3];
        correctValue = q.correctAnswer;

        correctAnswerString = q.answer[correctValue-1];

        Debug.Log(correctAnswerString);

        GameManager.QuestionList.RemoveAt(0);
    }

	void endGame()
    {


        foreach(var item in GameManager.quizModel) {
            EditorUtility.DisplayDialog("INCORRECT", item.CorrectAnswer, "OK");

            //Debug.Log("INCORRECT: " + item.CorrectAnswer);
        }



        EditorUtility.DisplayDialog("Game over", "You have answered all the questions. Your score was: " + score.value, "OK");
    }

	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyUp("1"))
        {
            Debug.Log(answerCorrect(1));
        }
        else if (Input.GetKeyUp("2"))
        {
            Debug.Log(answerCorrect(2));
        }
        else if (Input.GetKeyUp("3"))
        {
            Debug.Log(answerCorrect(3));
        }
        //else if (Input.GetKeyUp("4"))
        //{
        //    Debug.Log(answerCorrect(4));
        //}
    }

    private bool answerCorrect(int answer)
    {
        //TODO Doe wat er ook moet gebeuren wanneer het antwoord goed is.
        SceneManager.UnloadSceneAsync("questionScne");
        if (answer == correctValue)
        {
            score.value += 1;

        } else {

            // Add to wrong question list

            //Debug.Log("my questionnnnn" + q.question);

            GameManager.quizModel.Add(
                new QuizModel
                {
                    Question = q.question,
                    GivenAnswer = "1",
                    CorrectAnswer = correctAnswerString
                }
            );
        }

        //Debug.Log("FIRST IN LIST: " + quizModel[0].Question);


        EditorUtility.DisplayDialog("Resultaat", "Het juiste antwoord is " + correctValue,  "OK");



        if (GameManager.QuestionList.Count == 0) {}endGame();
        return answer == correctValue;

    }
}
