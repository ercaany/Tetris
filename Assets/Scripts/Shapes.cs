using UnityEngine;
using System.Collections;

public class Shapes : MonoBehaviour {
	float lastFall = 0f;
	public float timeToFall = 2f;


	// Use this for initialization
	void Start () {
		if(!isValidGridPos()){
			PlayerPrefs.SetInt ("LastScore", ScoreManager.score);
			Destroy (gameObject);
			Application.LoadLevel ("GameOver");
		}
	}



	// Update is called once per frame
	void Update () {
		
		// Move Left
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			//change the position
			transform.position += new Vector3 (-1, 0, 0);

			//check if it is valid
			if (isValidGridPos ())
				updateGrid ();
			else
				transform.position += new Vector3 (1, 0, 0);
		}

		// Move Right
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//change the position
			transform.position += new Vector3 (1, 0, 0);

			//check if it is valid
			if (isValidGridPos ())
				updateGrid ();
			else
				transform.position += new Vector3 (-1, 0, 0);
		}

		//Rotate
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			transform.Rotate (0, 0, -90);
			//check if it is valid
			if (isValidGridPos ())
				updateGrid ();
			else
				transform.Rotate (0, 0, 90);
		}
		//Move Down
		else if (Input.GetKeyDown (KeyCode.DownArrow) || Time.time - lastFall >= timeToFall) {
			
			//change the position
			transform.position += new Vector3(0, -1, 0);

			//check if it's valid
			if (isValidGridPos ()) {
				updateGrid ();
			} else {
				transform.position += new Vector3(0, 1, 0);

				//clear the filled rows
				Grid.deleteFullRows();

				//spawn next shapes
				FindObjectOfType<Spawner>().SpawnNext();

				enabled = false;
			}
			lastFall = Time.time;
		}
	}

	bool isValidGridPos() {        
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2(child.position);

			// is it inside border ?
			if (!Grid.insideBorder(v))
				return false;

			// Block in grid cell and not part of same shape?
			if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
				return false;
		}
		return true;
	}

	void updateGrid() {
		// Remove old children from grid
		for (int y = 0; y < Grid.height; ++y)
			for (int x = 0; x < Grid.width; ++x)
				if (Grid.grid[x, y] != null)
				if (Grid.grid[x, y].parent == transform)
					Grid.grid[x, y] = null;

		// Add new children to grid
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2(child.position);
			Grid.grid[(int)v.x, (int)v.y] = child;
		}        
	}
}
