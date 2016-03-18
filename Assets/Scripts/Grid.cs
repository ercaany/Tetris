using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public static int width = 10;
	public static int height = 20;
	//by making it Transform type we prevent writing xxx.transform.position all the time.
	public static Transform[,] grid = new Transform[width, height];



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// rotations in the game may cause coordinates to be not round anymore so we have to round them all the time, 
	// to work with Grid
	public static Vector2 roundVec2(Vector2 v) {
		return new Vector2(Mathf.Round(v.x),
						   Mathf.Round(v.y));
	}

	//to check if we are in the border or not
	public static bool insideBorder(Vector2 pos) {
		return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0 && (int)pos.y <= 20);
	}

	//to delete selected row
	public static void deleteRow(int y) {
		for (int x = 0; x < width; ++x) {
			Destroy(grid[x, y].gameObject);
			grid[x, y] = null;
		}
	}

	//to make the one row fall down
	public static void decreaseRow(int y) {
		for (int x = 0; x < width; ++x) {
			if (grid[x, y] != null) {
				// Move one row to bottom
				grid[x, y - 1] = grid[x, y];
				grid[x, y] = null;

				// Update Block's position
				grid[x, y - 1].position += new Vector3(0, -1, 0);
			}
		}
	}

	//to make sure every possible row will fall if below that row is null
	public static void decreaseRowsAbove(int y) {
		for (int i = y; i < height; ++i)
			decreaseRow (i);
	}

	//to check if the row is full 
	public static bool isRowFull(int y) {
		for (int x = 0; x < width; ++x)
			if (grid[x, y] == null)
				return false;
		return true;
	}

	//to delete if the row is full
	public static void deleteFullRows() {
		int combo = 0;
		for (int y = 0; y < height; ++y) {
			if (isRowFull(y)) {
				deleteRow(y);
				combo++;
				decreaseRowsAbove(y + 1);
				--y;
			}
		}
		ScoreManager.score += 100 * combo * combo;
		if(ScoreManager.score > HighScore.score){
			HighScore.score = ScoreManager.score;
		}
	}

}
