using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	public static int score;

	// Use this for initialization
	void Awake () {
		if (PlayerPrefs.HasKey ("TetrisHighScore")) {
			score = PlayerPrefs.GetInt ("TetrisHighScore");
		}
		PlayerPrefs.SetInt ("TetrisHighScore", score);
	}

	// Update is called once per frame
	void Update () {
		Text text = this.GetComponent<Text>();
		text.text = "High Score: " + score;

		if(score > PlayerPrefs.GetInt("TetrisHighScore")){
			PlayerPrefs.SetInt ("TetrisHighScore", score);	
		}

	}
}