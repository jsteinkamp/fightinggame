using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIScript : MonoBehaviour {

	public GameObject player;
	PlayerMoveScript playerScript;
	public GameObject player2;
	PlayerMoveScript playerScript2;
	public static float BAR_HEIGHT = 249f;
	public static float BAR_WIDTH = 311f;

	public void Start(){
		}

	public void AddPlayers(GameObject newPlayer, GameObject np2) {
		player = newPlayer;
		playerScript = player.GetComponent<PlayerMoveScript> ();

		player2 = np2;
		playerScript2 = player2.GetComponent<PlayerMoveScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (player != null)
		{
			float health = playerScript.health;
			float maxhealth= playerScript.maxHealth;
			float MP= playerScript.MP;
			float maxMP= playerScript.maxMP;
			float butthurt = playerScript.butthurt;
			float maxbutt = playerScript.maxButthurt;
			float health2 = playerScript2.health;
			float maxhealth2= playerScript2.maxHealth;
			float MP2= playerScript2.MP;
			float maxMP2= playerScript2.maxMP;
			float butthurt2 = playerScript2.butthurt;
			float maxbutt2 = playerScript2.maxButthurt;
			foreach (Transform child in transform)
			{
				if (child.name == "Butthurt Bar")
				{
					child.GetComponent<RectTransform>().sizeDelta = new Vector2(butthurt / maxbutt * BAR_WIDTH, BAR_HEIGHT);
				}
				if (child.name == "Health Bar")
				{
					child.GetComponent<RectTransform>().sizeDelta = new Vector2(health / maxhealth * BAR_WIDTH,BAR_HEIGHT);
				}
				if (child.name == "Magic Bar")
				{
					child.GetComponent<RectTransform>().sizeDelta = new Vector2(MP / maxMP * BAR_WIDTH, BAR_HEIGHT);
				}
				if (child.name == "Butthurt Bar2")
				{
					child.GetComponent<RectTransform>().sizeDelta = new Vector2(butthurt2 / maxbutt2 * BAR_WIDTH, BAR_HEIGHT);
				}
				if (child.name == "Health Bar2")
				{
					child.GetComponent<RectTransform>().sizeDelta = new Vector2(health2 / maxhealth2 * BAR_WIDTH,BAR_HEIGHT);
				}
				if (child.name == "Magic Bar2")
				{
					child.GetComponent<RectTransform>().sizeDelta = new Vector2(MP2 / maxMP2 * BAR_WIDTH, BAR_HEIGHT);
				}
			}

		}	
	}
}
