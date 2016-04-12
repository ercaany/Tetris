using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastScore : MonoBehaviour {

	public static int score;

	// Use this for initialization
	void Awake () {
		score = PlayerPrefs.GetInt ("LastScore");
	}

	// Update is called once per frame
	void Update () {
		Text text = this.GetComponent<Text>();
		text.text = "Score: " + score;

	}
}
