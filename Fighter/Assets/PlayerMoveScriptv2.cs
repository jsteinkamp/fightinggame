using UnityEngine;
using System.Collections;

public class PlayerMoveScriptv2 : MonoBehaviour {

	public int playerNum;
	

	protected float maxSpeed;
	public float maxButthurt;
	public float maxHealth;
	public float maxMP;
	public float health;
	public float butthurt;
	public float MP;

	public GameObject brickFab;

	protected int rightface = -1;

	// velocity given from outside (think DI)...attenuates
	protected Vector2 outsideVel;

	protected Animator anim;
	public float JUMP_ABILITY;
	public int jumps_used;
	public int max_jumps;

	public int recent_reverse;
	public static int RECENT_REVERSE_MAXFRAMES = 5;

	public int jump_delay;
	public int JDelayMax;

	public static float GROUND_CUTOFF = .005f;
	public static float WALL_CUTOFF = .05f;
	public bool grounded;

	public bool touchingWall = false;
	public bool stuckToWall = false;
	public int lastTimeOnWall = 0;

	public static int WALLJUMPFRAMES = 10;
	public static int WALL_MULTIPLE_TIMES = 10;


	int wallTimer = 0;

	int maxWallfails = 1;
	int wallfails = 0;

	// Use this for initialization
	void Start () {

		// character varying parameters, can be overwritten
		anim = this.GetComponent<Animator> ();
		maxButthurt = 100f;
		maxHealth = 100f;
		maxMP = 100f;
		health = 100f;
		maxSpeed = 17;
		max_jumps = 1;
		JUMP_ABILITY = 45;
		recent_reverse = 10;
		JDelayMax = 6;

		outsideVel = new Vector2 (0, 0);
	}

	public void SetPlayerNum(int num) {
		playerNum = num;
	}

	public RaycastHit2D raycastCollide(char side, float distance)
	{
		if (side == 'd')
			return Physics2D.Raycast (new Vector2 (transform.position.x, renderer.bounds.min.y), -Vector2.up, distance);
		else if (side == 'u')
			return Physics2D.Raycast (new Vector2(transform.position.x, renderer.bounds.max.y), Vector2.up, distance);
		else if (side == 'r')
			return Physics2D.Raycast (new Vector2(renderer.bounds.max.x, transform.position.y), Vector2.right, distance);
		else if (side == 'l')
			return Physics2D.Raycast (new Vector2(renderer.bounds.min.x, transform.position.y), -Vector2.right, distance);
		Debug.Log ("invalid direction input");
		return new RaycastHit2D();
	}
	

	// Update is called once per frame
	void FixedUpdate () {


		//////// TIMERS AND COUNTERS ////////

		// did we recently reverse
		recent_reverse++;
		if (recent_reverse > RECENT_REVERSE_MAXFRAMES)
			recent_reverse = RECENT_REVERSE_MAXFRAMES;

		// decrease jump delay
		jump_delay--;
		if (jump_delay < 0)
			jump_delay = 0;

		// time clinging to wall
		wallTimer--;
		if (wallTimer < 0)
			wallTimer = 0;

		// time since left wall BY WALL JUMPING
		lastTimeOnWall++;
		if (lastTimeOnWall > WALL_MULTIPLE_TIMES)
			lastTimeOnWall = WALL_MULTIPLE_TIMES;

		outsideVel = outsideVel / 1.2f;

		/////// ENVIRONMENTAL CHECKS ///////////

		//Debug.Log (transform.position.y - renderer.bounds.extents.y);
		// if landed
		RaycastHit2D ray = raycastCollide ('d', GROUND_CUTOFF);
		grounded = !(ray.collider == null);

		// if near to the wall
		ray = raycastCollide ('r',WALL_CUTOFF);
		if (!ray.collider)
		{
			ray = raycastCollide ('l',WALL_CUTOFF);
		}
		bool oldtouchingWall = touchingWall;
		touchingWall = !(ray.collider == null);

		if (grounded && jump_delay == 0)
			//Debug.Log (ray.collider.gameObject);
			jumps_used = 0;
		if (grounded)
			wallfails = 0;

		// if in midair, check wall collisions
		if (!grounded)
		{
			if (touchingWall)
			{

				if (!stuckToWall && wallfails < maxWallfails && lastTimeOnWall >= WALL_MULTIPLE_TIMES)
				{
					//stick to wall if you can
					wallTimer = WALLJUMPFRAMES;
					stuckToWall = true;
					rigidbody2D.gravityScale = 0;
					rigidbody2D.velocity = new Vector2(0,0);

				}
				if (stuckToWall && wallTimer == 0)
				{
					// fall off the wall due to lack of time
					stuckToWall = false;
					wallfails++;
					rigidbody2D.gravityScale = 8;
				}
			}
			if (!touchingWall && oldtouchingWall)
			{	
				// you moved off the wall
				wallfails++;
				stuckToWall = false;
				rigidbody2D.gravityScale = 8;
			}


		}

		///////////// CONTROLLER INPUT ////////////////////

		// joystick axes
		float move;
		float vmove;
		if (playerNum == 1)
			move = Input.GetAxis ("Horizontal");
		else
			move = Input.GetAxis ("Horizontal2");
		if (playerNum == 1)
			vmove = Input.GetAxis ("Vertical");
		else
			vmove = Input.GetAxis ("Vertical2");

		// horizontal movement - prevent weird physics collisions
		if (move < 0)
			ray = raycastCollide ('l', WALL_CUTOFF);
		else
			ray = raycastCollide ('r', WALL_CUTOFF);

		// ok to move
		if (ray.collider == null)
		{
			rigidbody2D.velocity = new Vector2 (outsideVel.x + move * maxSpeed, outsideVel.y + rigidbody2D.velocity.y);
			anim.SetFloat ("speed", Mathf.Abs(move * maxSpeed));
			if (move > 0 && rightface == -1 || move < 0 && rightface == 1)
				Flip ();
		}
		else
		{
			// stop player
			//rigidbody2D.velocity = new Vector2(outsideVel.x,outsideVel.y);
			//anim.SetFloat ("speed", 0);

		}

		// jump
		if (playerNum == 1){

			if (Input.GetButtonDown("Y1"))
			{
				if (jumps_used < max_jumps && jump_delay == 0)
				{
					rigidbody2D.AddForce(new Vector2(0,1000));
					jumps_used++;
					jump_delay = JDelayMax;
				}
				else if (stuckToWall)
				{
					// WALL KICK
					Debug.Log("YO");
					Flip ();
					lastTimeOnWall = 0;
					outsideVel = new Vector2 (40, 10);
					rigidbody2D.AddForce (new Vector2(1000, 1000));
					rigidbody2D.gravityScale = 8;
					stuckToWall = false;
					wallTimer = 0;
				}
			}
			if (Input.GetButtonDown ("A1"))
			{	
				if (Mathf.Abs (move) < .2f && Mathf.Abs (vmove) < .2f)
				{
					if (grounded)
						NeutralA ();
					else
						NAir();
				}
				else if (Mathf.Abs (vmove) > Mathf.Abs (move))
				{
					if (vmove > 0) 
					{
						if (grounded)
							UpA();
						else
							UAir();
					}
					else
					{
						if (grounded)
							DownA();
						else 
							DAir();
					}
				}
				else
				{
					if (recent_reverse >= RECENT_REVERSE_MAXFRAMES) 
					{
						if (grounded)
							ForwardA();
						else
							FAir();
					}
					else
					{
						if (grounded)
							BackA();
						else
							BAir();
					}
				}

			}
		}
		if (playerNum == 2){
			
			if (Input.GetButtonDown("Y2"))
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, JUMP_ABILITY);
			if (Input.GetButtonDown ("A2"))
				NeutralA ();
		}

		// ring out
		if (InitGame.OutOfBounds (transform.position)) {
				transform.position = new Vector2 (0, 6);
				takeDamage (40,0,0);
			Debug.Log (Camera.current.GetComponent<Animator>());
			//Camera.current.transform.position = Camera.current.transform.position + new Vector3(1,0,0);
			Camera.current.GetComponent<Animator>().SetFloat ("trig",2);
		}
	}

	void Flip() {
		rightface = -1 * rightface;
		anim.SetFloat ("dir", 1 - anim.GetFloat ("dir"));
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		recent_reverse = 0;
		transform.localScale = scale;
	}

	protected virtual void NeutralA(){
		Debug.Log ("Neutral");
		if (rightface == 1)
			EmitProjectile(brickFab,new Vector2(.01f,0),0, new Vector2(15,0));
		else
			EmitProjectile(brickFab,new Vector2(-.01f,0),0, new Vector2(-15,0));
	}
	
	protected virtual void UpA(){
		Debug.Log ("UpA");
	}
	
	protected virtual void DownA(){
		Debug.Log ("DownA");
	}
	
	protected virtual void BackA(){
		Debug.Log ("Back");
	}
	
	protected virtual void ForwardA(){
		Debug.Log ("RightA");
	}

	protected virtual void UAir(){
		Debug.Log ("UAir");
	}
	
	protected virtual void DAir(){
		Debug.Log ("DAir");
	}
	
	protected virtual void BAir(){
		Debug.Log ("BAir");
	}
	
	protected virtual void FAir(){
		Debug.Log ("FAir");
	}
	
	protected virtual void NAir(){
		Debug.Log ("NAir");
	}

	void EmitProjectile(GameObject proj, Vector2 emitFrom, float angle, Vector2 speed) {
		GameObject newbullet = (GameObject) Instantiate (proj, new Vector3 (transform.position.x + emitFrom.x, transform.position.y + emitFrom.y, 0), new Quaternion (0, 0, angle,0));
		newbullet.GetComponent<projctilescript>().SetSpeed (speed);
		newbullet.GetComponent<projctilescript>().SetPlayer (gameObject);
	}

	public void takeDamage(float h, float b, float m)
	{
		health -= h;
		butthurt += b;
		MP += m;
		if (butthurt > maxButthurt)
			butthurt = maxButthurt;
		if (MP > maxMP)
			MP = maxMP;
	}

}
