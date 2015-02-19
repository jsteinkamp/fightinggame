using UnityEngine;
using System.Collections;

public class BoomerangScript: projctilescript {

	// boomerang heading out from player?
	bool outMode;
	float maxDist;
	float travDist;
	Vector3 oldPos;

	// Use this for initialization
	void Start () {
		outMode = true;
		travDist = 0;
		oldPos = transform.position;
	}
	
	public void SetMax(float distance) {
		maxDist = distance;
	}
	
	// Update is called once per frame
	void Update () {
		//speed = 10;
		if (outMode) {
						rigidbody2D.velocity = speed;
						travDist += Vector3.Distance (oldPos, transform.position);
						Debug.Log (travDist);
						oldPos = transform.position;
				} else
						rigidbody2D.velocity = Mathf.Max (1f,(float) (5f / Vector3.Magnitude (speed))) * (owner.transform.position - transform.position);

		//transform.position = transform.position + new Vector3 (speed.x* Time.deltaTime ,speed.y,0);
		if (InitGame.OutOfBounds (transform.position) || travDist > maxDist) 
						outMode = false;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && other.gameObject != owner)
		{
			Debug.Log ("player");
			other.gameObject.rigidbody2D.AddForce (new Vector2(2000 * speed.x,0));
			Destroy (gameObject);
			other.gameObject.GetComponent<PlayerMoveScript>().takeDamage(10,10,0);
			owner.gameObject.GetComponent<PlayerMoveScript>().takeDamage(0,0,10);
			
		}
	}
}
