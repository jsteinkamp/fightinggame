using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour {

	public GameObject player1;
	public GameObject player1Prefab;
	public GameObject player2;
	public GameObject player2Prefab;
	public GameObject explosion;
	public GameObject stupidWallPrefab;

	public GameObject UI;
	public GameObject UIprefab;
	public static float LEFTBOUND = -40f;
	public static float RIGHTBOUND = 40f;
	public static float  UPBOUND = 40f;
	public static float DOWNBOUND = -20f;
	public static float BRICKSIZE = .7f;


	public static bool OutOfBounds(Vector2 position){
		if (position.x < LEFTBOUND || position.x > RIGHTBOUND || position.y < DOWNBOUND || position.y > UPBOUND)
						return true;
		return false;
	}

	// Use this for initialization
	void Start () {
		UI = (GameObject) Instantiate(UIprefab);
		player1 = (GameObject) Instantiate (player1Prefab);
		player1.transform.position = new Vector2 (-3, 10);
		player1.GetComponent<PlayerMoveScript> ().SetPlayerNum (1);

		player2 = (GameObject) Instantiate (player2Prefab);
		player2.transform.position = new Vector2 (3, 10);
		player2.GetComponent<PlayerMoveScript> ().SetPlayerNum (2);

		UI.GetComponent<UIScript> ().AddPlayers (player1, player2);

		// build stpid stage
		/*for (int i = -20; i < 21; i++)
		{
			GameObject wall = (GameObject) Instantiate (stupidWallPrefab);
			wall.transform.position = new Vector2(i * BRICKSIZE, BRICKSIZE * -20f);
			wall = (GameObject) Instantiate (stupidWallPrefab);
			wall.transform.position = new Vector2(i * BRICKSIZE, BRICKSIZE * 20f);
			if (Mathf.Abs (i) <= 19)
			{
				wall = (GameObject) Instantiate (stupidWallPrefab);
				wall.transform.position = new Vector2(-BRICKSIZE * -20f, i * BRICKSIZE);
				wall = (GameObject) Instantiate (stupidWallPrefab);
				wall.transform.position = new Vector2(-BRICKSIZE * 20f, i * BRICKSIZE);
			}

		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
