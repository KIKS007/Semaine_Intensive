using UnityEngine;
using System.Collections;
using System;
using Rewired;
using DG.Tweening;

public class Plateform2DMotor : MonoBehaviour 
{
	public enum PlayerState
	{
		OnGround,
		InAir,
		Falling,
		Dashing,
	}

	public enum JumpState
	{
		CanJump,
		HasJumped,
		HasDoubleJumped
	}

	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	public PlayerState playerState;
	public JumpState jumpState;

	[Header ("On Ground")]
	public float maxGroundSpeed = 3f;
	public LayerMask groundLayer;

	[Header ("In Air")]
	public float maxAirSpeed = 2f;
	public float gravityForce;
	public float fastGravityForce;

	[Header ("Jump")]
	public float jumpForce = 0.5f;
	public float doubleJumpForce = 0.5f;

	[Header ("Dash")]
	public float dashCooldown = 1;
	public float dashForce = 4.5f;
	public float dashDuration = 0.2f;
	public Ease dashEase;


	private bool facingLeft;

	private Rigidbody rb;

	private bool doubleJumped = false;

	private bool canDash = true;

	private float distToGround;

	// Use this for initialization
	void Start () 
	{
		player = ReInput.players.GetPlayer(playerId);
		rb = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider> ().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerState != PlayerState.Dashing)
			IsGrounded ();
		
		SetFacing();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (playerState != PlayerState.Dashing)
		{
			Movement ();
			//Gravity ();
		}

		if(jumpState == JumpState.HasJumped)
			DoubleJump ();

		if(jumpState == JumpState.CanJump)
			Jump ();

		if (canDash && playerState != PlayerState.Dashing && player.GetButtonDown("Dash"))
			StartCoroutine (Dash ());
	}

	void Movement ()
	{
		//Debug.Log (player.GetAxis("Movement_Horizontal"));

		Vector3 movement = new Vector3 (player.GetAxis("Movement_Horizontal"), 0, 0);

		if(playerState == PlayerState.OnGround)
		{
			rb.MovePosition(rb.position + movement * maxGroundSpeed * Time.fixedDeltaTime);
		}

		if(playerState == PlayerState.InAir || playerState == PlayerState.Falling)
		{
			rb.MovePosition(rb.position + movement * maxAirSpeed * Time.fixedDeltaTime);
		}
	}

	void Gravity ()
	{
		if(playerState == PlayerState.InAir || playerState == PlayerState.Falling)
		{
			if(player.GetAxis("Movement_Vertical") < 0)
				rb.AddForce(new Vector3 (0, -gravityForce, 0), ForceMode.Acceleration);

			else
				rb.AddForce(new Vector3 (0, -fastGravityForce, 0), ForceMode.Acceleration);
		}
	}

	void IsGrounded ()
	{
		if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
		{
			playerState = PlayerState.OnGround;
			jumpState = JumpState.CanJump;
		}
		else if(rb.velocity.y < 0)
		{
			playerState = PlayerState.Falling;
		}
		else
		{
			playerState = PlayerState.InAir;
		}
	}

	void Jump ()
	{
		if(player.GetButton("Jump"))
		{
			rb.velocity = new Vector3 (rb.velocity.x, jumpForce, rb.velocity.z);
			jumpState = JumpState.HasJumped;
		}
	}

	void DoubleJump ()
	{
		if(player.GetButtonDown("Jump"))
		{
			rb.velocity = new Vector3 (rb.velocity.x, doubleJumpForce, rb.velocity.z);
			jumpState = JumpState.HasDoubleJumped;
		}
	}

	IEnumerator Dash ()
	{
		float dashForceTemp = dashForce;

		canDash = false;
		playerState = PlayerState.Dashing;

		dashForceTemp = facingLeft ? -dashForce : dashForce;

		DOTween.To (() => dashForceTemp, x => dashForceTemp = x, 0, dashDuration).SetEase (dashEase).OnUpdate(
			()=> rb.velocity = new Vector3(dashForceTemp, rb.velocity.y, rb.velocity.z));

		yield return new WaitForSeconds (dashDuration - 0.05f);

		IsGrounded ();

		yield return new WaitForSeconds (dashCooldown);

		canDash = true;
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

	void OnCollisionStay(Collision collision)
	{
		/*if(1 << collision.gameObject.layer == groundLayer.value && playerState != PlayerState.Dashing)
		{
			playerState = PlayerState.OnGround;
			jumpState = JumpState.CanJump;
		}*/
	}

	void OnCollisionExit(Collision collision)
	{
		/*if(1 << collision.gameObject.layer == groundLayer.value)
		{
			playerState = PlayerState.InAir;
		}*/
	}
}
