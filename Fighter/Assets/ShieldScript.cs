using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	public int i = 0;
	int dieWhen = 999;
	GameObject myPlayer;
	// Use this for initialization
	void Start () {
	}

	public void AddPlayer (GameObject p, int lag) {
		myPlayer = p;
		dieWhen = lag;
	}
	
	// Update is called once per frame
	void Update () {
		if (myPlayer != null)
			transform.position = myPlayer.transform.position;
		i++;
		if (i > dieWhen)
			Destroy (gameObject);
	}
}
