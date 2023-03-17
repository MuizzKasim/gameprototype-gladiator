using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody2D playerRigidBody2D;
	private float movePlayerVector;
	private bool facingRight;
	public static bool canMove = true;
	public bool isInteracting = false;
	public float speed = 4.0f;
	private GameObject playerSprite;
	private Animator anim;
	public Transform player;
	private Vector3 playerCurrentPosition;
	public Vector3 playerOriginalPosition;
	public bool moving = false;

	public AudioSource walk0;
	private AudioSource currentSound;

	void Awake()
	{
		// Setting up references.
		playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		playerSprite = transform.Find("PlayerSprite").gameObject;
		anim = (Animator)playerSprite.GetComponent(typeof(Animator));
		player = GameObject.Find ("Player").transform;
		playerOriginalPosition = player.position;

		if (GameState.LastScenePositions.ContainsKey("Town")) {
			playerCurrentPosition = GameState.GetLastScenePosition("Town");
		} else {
			playerCurrentPosition = playerOriginalPosition;
		}
		currentSound = walk0;
		player.position = playerCurrentPosition;
	}

	// Update is called once per frame
	void Update()
	{
		// Cache the horizontal input.
		if (canMove && !isInteracting) {
			movePlayerVector = Input.GetAxis ("Horizontal");

			anim.SetFloat ("speed", Mathf.Abs (movePlayerVector));
			playerRigidBody2D.velocity = new Vector2 (movePlayerVector * speed, playerRigidBody2D.velocity.y);

			if (playerRigidBody2D.velocity.x == 0)
				moving = false;
			else
				moving = true;

			if (movePlayerVector < 0 && !facingRight) {
				Flip ();
			} else if (movePlayerVector > 0 && facingRight) {
				Flip ();
			}
		} else {
			moving = false;
			anim.SetFloat ("speed", 0f);
			playerRigidBody2D.velocity = new Vector2 (0f,0f);
		}

		if (!currentSound.isPlaying && moving && anim.GetFloat("speed")>= 0.05f) {
			
			float randomiser2 = Random.Range (0.3f, 0.4f);
			currentSound.volume = randomiser2;

			currentSound.PlayDelayed (0.5f);
		}
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = playerSprite.transform.localScale;
		theScale.x *= -1;
		playerSprite.transform.localScale = theScale;
	}
}
