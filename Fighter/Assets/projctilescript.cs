using UnityEngine;
using System.Collections;

public class projctilescript : MonoBehaviour {

	public Vector3 speed;
	public GameObject dieExplode;
	public bool boomerang;
	public bool control;
	protected GameObject owner;
	// Use this for initialization
	void Start () {
	}

	public void SetSpeed(Vector2 s) {
		speed = s;
	}
	public void SetPlayer(GameObject player) {
		owner = player;
	}

	// Update is called once per frame
	void Update () {
		//speed = 10;
		//rigidbody2D.velocity = speed;
		transform.position = transform.position + new Vector3 (speed.x* Time.deltaTime ,speed.y,0);
		if (InitGame.OutOfBounds (transform.position)) 
						Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && other.gameObject != owner)
		{
			Debug.Log ("player");
			other.gameObject.rigidbody2D.AddForce (new Vector2(2000 * speed.x,0));
			Instantiate (dieExplode);
			dieExplode.transform.position = other.transform.position;
			Destroy (gameObject);
			other.gameObject.GetComponent<PlayerMoveScript>().takeDamage(10,10,0);
			owner.gameObject.GetComponent<PlayerMoveScript>().takeDamage(0,0,10);

		}
	}
}
