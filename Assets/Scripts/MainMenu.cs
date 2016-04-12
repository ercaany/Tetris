using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void startGame(){
		Application.LoadLevel ("Game");
	}

	public void exitGame(){
		Application.Quit ();
	}
}
