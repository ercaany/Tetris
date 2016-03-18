using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	//shapes
	public GameObject[] shapes;


	// Use this for initialization
	void Start () {
		SpawnNext ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnNext(){
		int i = Random.Range (0, shapes.Length);
		Instantiate (shapes [i], transform.position, Quaternion.identity);
	}
}
