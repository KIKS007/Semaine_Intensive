using UnityEngine;
using System.Collections;
using System;
using Rewired;

public class Plateform2DMotor : MonoBehaviour 
{
	public enum PlayerState
	{
		OnGround,
		Jumped,
		Doublejumped,
		Falling,
		Dashing,
	}

	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	public PlayerState playerState;

	[Header ("On Ground")]
	public float maxGroundSpeed = 3f;
	public float timeToMaxGroundSpeed;
	public LayerMask groundLayer;

	[Header ("In Air")]
	public float maxAirSpeed = 2f;
	public float gravityForce;

	[Header ("Jump")]
	public float jumpForce = 0.5f;
	public float doubleJumpForce = 0.5f;

	[Header ("Dash")]
	public float dashCooldown = 1;
	public float dashDistance = 1;
	public float dashDuration = 0.2f;


	public bool facingLeft;

	private Rigidbody rb;

	public bool doubleJumped = false;

	// Use this for initialization
	void Start () 
	{
		player = ReInput.players.GetPlayer(playerId);
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetFacing();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		Movement ();

		if(playerState == PlayerState.OnGround)
			Jump ();

		if(playerState == PlayerState.Jumped || playerState == PlayerState.Falling)
			DoubleJump ();
	}

	void Movement ()
	{
		Vector3 movement = new Vector3 (player.GetAxis("Movement_Horizontal"), 0, 0);

		if(playerState == PlayerState.OnGround)
		{
			rb.MovePosition(rb.position + movement * maxGroundSpeed * Time.fixedDeltaTime);
		}

		else
		{
			rb.MovePosition(rb.position + movement * maxAirSpeed * Time.fixedDeltaTime);
			rb.AddForce(new Vector3 (0, -gravityForce, 0), ForceMode.Acceleration);
		}
	}

	void Jump ()
	{
		if(player.GetButton("Jump"))
		{
			rb.velocity = new Vector3 (rb.velocity.x, jumpForce, rb.velocity.z);
			playerState = PlayerState.Jumped;
		}
	}

	void DoubleJump ()
	{
		if(player.GetButtonDown("Jump"))
		{
			rb.velocity = new Vector3 (rb.velocity.x, doubleJumpForce, rb.velocity.z);
			playerState = PlayerState.Doublejumped;
		}
	}

	void Dash ()
	{
		
	}

	void SetFacing()
	{
		if (player.GetAxis("Movement_Horizontal") < 0)
		{
			facingLeft = true;
		}
		else if (player.GetAxis("Movement_Horizontal") > 0)
		{
			facingLeft = false;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(1 << collision.gameObject.layer == groundLayer.value)
		{
			playerState = PlayerState.OnGround;
			doubleJumped = false;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if(1 << collision.gameObject.layer == groundLayer.value && playerState != PlayerState.Jumped)
		{
			playerState = PlayerState.Falling;
		}
	}
}
