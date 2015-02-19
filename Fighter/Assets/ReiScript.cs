using UnityEngine;
using System.Collections;

public class ReiScript : PlayerMoveScript {


	public GameObject bowFab;
	public GameObject bow;
	int bowcount;
	// Use this for initialization
	void Start () {
		JUMP_ABILITY = 15f;
		anim = this.GetComponent<Animator> ();
		maxButthurt = 100f;
		maxHealth = 90f;
		maxMP = 100f;
		health = 100f;
		maxSpeed = 12;

	}

	protected override void NeutralA(){
		if (!bow){
			bow = (GameObject)Instantiate (bowFab);
			if (rightface == 1)
				bow.GetComponent<BoomerangScript>().SetSpeed (new Vector2(15,0));
			else
				bow.GetComponent<BoomerangScript>().SetSpeed (new Vector2(-15,0));
			bow.GetComponent<BoomerangScript>().SetPlayer (gameObject);
			bow.GetComponent<BoomerangScript>().SetMax (40);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
