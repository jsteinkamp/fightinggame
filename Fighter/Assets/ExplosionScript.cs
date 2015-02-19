using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		if (i > 24)
			Destroy (gameObject);
	}
}
