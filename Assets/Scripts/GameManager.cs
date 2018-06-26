using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using UnityEditor;

public class GameManager : MonoBehaviour {
	public float turnDelay = 0f;
	private List<Enemy> enemies;
	public float levelStartDelay = 2f;
	private Text levelText;
	private GameObject levelImage;
	public bool battle = false;
	private bool doingSetup;
	private bool enemyesMoving;
	// Use this for initialization
	public BoardManager boardScript;
	public static GameManager instance = null;
	private int level = 1;
	public int playerFoodPoints = 100;
	[HideInInspector]public bool playersTurn = true;
	public int lastVisitedQX = 0;
	public int lastVisitedQY = 0;
	public static int questionID = -1;
    public static List<Question> QuestionList = new List<Question>();
    public static int infoID = -1;
	public infoboxcontroller ibc;
	public Canvas canvas;

	void Awake () {
        
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
		canvas = GetComponent<Canvas>();
		enemies = new List<Enemy>();
		DontDestroyOnLoad(gameObject);		
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	private void OnLevelWasLoaded (int index){
		level = level+1;
		InitGame();
	}
	void InitGame(){
		doingSetup = true;
		// levelText.text = "Day "+level;
		// levelImage.SetActive(true);
		Invoke("HideLevelImage",levelStartDelay);
		enemies.Clear();
        LoadQuestions();
		boardScript.SetupScene(level);

	}
	private void HideLevelImage(){
		// levelImage.SetActive(false);
		doingSetup = false;
	}
	public void GameOver(){
		// levelText.text = "After "+level + " days, you starved.";
		// levelImage.SetActive(true);
		// enabled = false;
	}

    public void LoadQuestions()
    {
        string stringAsset = (Resources.Load("questions") as TextAsset).ToString();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(stringAsset));
        string xmlPathPattern = "//Questions/Question";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        foreach (XmlNode node in myNodeList)
        {
            string corr = node.Attributes.GetNamedItem("correct").Value;
            XmlNode question = node.FirstChild;
            XmlNode o1 = question.NextSibling;
            XmlNode o2 = o1.NextSibling;
            XmlNode o3 = o2.NextSibling;
            XmlNode o4 = o3.NextSibling;
            GameManager.QuestionList.Add(new Question()
            {
                question = question.InnerText,
                answer = new List<string>() { o1.InnerText, o2.InnerText, o3.InnerText, o4.InnerText },
                correctAnswer = int.Parse(corr)
            });
        }
    }

	
	// Update is called once per frame
	void Update () {
		if(playersTurn || enemyesMoving || doingSetup){
			return;
		}	
		StartCoroutine(MoveEnemies());

	}
	public void sceneChange(questionTrigger qt){
			Debug.Log("question: "+qt.questionID);
			lastVisitedQX = (int)qt.transform.position.x;	
			lastVisitedQY = (int)qt.transform.position.y;	
			SceneManager.LoadScene("questionScne", LoadSceneMode.Additive);

	}
	public void infoBox(infoTrigger ib){
			// ibc = GetComponent<infoboxcontroller>();
			Debug.Log("infobox: "+ib.infoID);

            Question q = GameManager.QuestionList[0];

        Debug.Log(q);

        EditorUtility.DisplayDialog("Title here", q.answer[3], "Ok");


			//ibc = canvas.GetComponent<infoboxcontroller>();
			//Debug.Log(ibc.contentID);
			// create canvas shizzle..
	}
	public void AddEnemyToList(Enemy script){
		enemies.Add(script);
	}
	public void startBattle(Enemy enemy){
		if(!battle){
			boardScript.startBattleScene();
			battle = true;
			Debug.Log(enemy.stringy);
		}
	}

	IEnumerator MoveEnemies(){
		if(!battle){
			enemyesMoving = true;
			yield return new WaitForSecondsRealtime(0.05f);
			if(enemies.Count == 0){
				yield return new WaitForSecondsRealtime(0.05f);
			}

			for(int i = 0; i < enemies.Count;i++){
				enemies[i].MoveEnemy();
				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			playersTurn = true;
			enemyesMoving = false;
		}

	}
}
