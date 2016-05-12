using UnityEngine;
using System.Collections;
using System;
using Rewired;
using DG.Tweening;
using XInputDotNetPure;

public enum Team
{
	Team1,
	Team2
}

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

public class PlayerScript : MonoBehaviour 
{
	public Action OnDash;
	public Action OnGround;
	public Action OnJump;
	public Action OnStun;
	public Action OnThrow;
	public Action OnFacinRight;
	public Action OnFacingLeft;

	public int playerId = 0; // The Rewired player id of this character
	[HideInInspector]
	public Player player; // The Rewired Player
	public PlayerState playerState;
	public JumpState jumpState;
	public Team team;

	[Header ("On Ground")]
	public float maxGroundSpeed = 3f;
	public LayerMask groundLayer;

	public bool facingLeft;
	private Rigidbody rb;

	[Header ("In Air")]
	public float maxAirSpeed = 2f;
	public float gravityForce;
	public float fastGravityForce;

	[Header ("Jump")]
	public float jumpForce = 0.5f;
	public float doubleJumpForce = 0.5f;

	private bool doubleJumped = false;
	private float distToGround;


	[Header ("Dash")]
	public float dashCooldown = 1;
	public float dashForce = 4.5f;
	public float dashDuration = 0.2f;
	public Ease dashEase;

	private bool canDash = true;


	[Header ("Ball")]
	public bool holdingBall;
	public Transform holdPoint;
	public float catchLerp = 0.5f;
	public float distanceMinToCatch;
	public float throwForceMin;
	public float throwForceMax;
	public float timeToMaxForce;

	private GameObject holdBall;
	private PhysicMaterial physicMat;
	private float mass;
	private float drag;
	private float angularDrag;
	private bool useGravity;
	private bool iskinematic;
	private Vector3 throwDirection;
	private float throwForceTemp;

	private bool charging = false;

	[Header ("Stun")]
	public float stunDuration;

	[Header ("Screen Shake")]
	public Vector2 stunScreenShake;

	private CameraScreenShake screenShake;


	[Header ("Vibration")]
	public Vector3 jumpVibration;
	public Vector3 groundVibration;
	public Vector3 dashVibration;
	public Vector3 stunVibration;
	public Vector3 throwVibration;

	private VibrationManager vibration;

	public bool vibrate = false;
	public Vector2 vibrationTest;
	public float vibrationDuration;

	public bool stunned = false;

	// Use this for initialization
	void Start () 
	{
		player = ReInput.players.GetPlayer(playerId);
		rb = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider> ().bounds.extents.y;
		screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScreenShake>();
		vibration = GameObject.FindGameObjectWithTag("VibrationManager").GetComponent<VibrationManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerState != PlayerState.Dashing)
			IsGrounded ();

		if(!stunned)
			SetFacing();

		throwDirection = new Vector3(player.GetAxis("Aim_Horizontal"), player.GetAxis("Aim_Vertical"), 0);

		if(holdingBall && !stunned)
		{
			if(player.GetButton("Throw") && !charging)
			{
				charging = true;
				//Debug.Log("Force Before " + throwForceTemp);
				//DOTween.Pause("ThrowForce");
				//DOTween.Complete("ThrowForce");
				ThrowForce ();
			}

			else if(player.GetButtonUp("Throw") && charging)
			{
				DOTween.Pause("ThrowForce");
				StartCoroutine(Throw ());
			}
		}

		if(vibrate)
		{
			vibrate = false;

			VibrationDebug ();
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (playerState != PlayerState.Dashing)
		{
			Gravity ();
		}

		if(!stunned)
		{
			if (playerState != PlayerState.Dashing)
			{
				Movement ();
			}

			if(jumpState == JumpState.HasJumped)
				DoubleJump ();

			if(jumpState == JumpState.CanJump)
				Jump ();

			if (canDash && playerState != PlayerState.Dashing && player.GetButtonDown("Dash"))
				StartCoroutine (Dash ());
		}


	}

	void Movement ()
	{
		//Debug.Log (player.GetAxis("Movement_Vertical"));


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
		//rb.AddForce(new Vector3 (0, -gravityForce, 0), ForceMode.Acceleration);

		if(playerState == PlayerState.InAir || playerState == PlayerState.Falling)
		{
			if(player.GetAxis("Movement_Vertical") < 0)
				rb.AddForce(new Vector3 (0, -fastGravityForce, 0), ForceMode.Acceleration);
			else
				rb.AddForce(new Vector3 (0, -gravityForce, 0), ForceMode.Acceleration);
		}
	}

	void IsGrounded ()
	{
		if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f, groundLayer))
		{

			if(playerState != PlayerState.OnGround)
			{
				playerState = PlayerState.OnGround;
				jumpState = JumpState.CanJump;

				vibration.VibrateBothMotors(playerId, groundVibration.x, groundVibration.z, groundVibration.y, groundVibration.z);
			}

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
		if(player.GetButtonDown("Jump"))
		{
			rb.velocity = new Vector3 (rb.velocity.x, jumpForce, rb.velocity.z);
			jumpState = JumpState.HasJumped;

			if(OnJump != null)
				OnJump ();
		}
	}

	void DoubleJump ()
	{
		if(player.GetButtonDown("Jump"))
		{
			rb.velocity = new Vector3 (rb.velocity.x, doubleJumpForce, rb.velocity.z);
			jumpState = JumpState.HasDoubleJumped;

			OnJump ();
		}
	}

	IEnumerator Dash ()
	{
		float dashForceTemp = dashForce;

		canDash = false;
		playerState = PlayerState.Dashing;

		vibration.VibrateBothMotors(playerId, dashVibration.x, dashVibration.z, dashVibration.y, dashVibration.z);

		dashForceTemp = facingLeft ? -dashForce : dashForce;

		DOTween.To (() => dashForceTemp, x => dashForceTemp = x, 0, dashDuration).SetEase (dashEase).OnUpdate(
			()=> rb.velocity = new Vector3(dashForceTemp, rb.velocity.y, rb.velocity.z));

		yield return new WaitForSeconds (dashDuration - 0.05f);

		IsGrounded ();

		yield return new WaitForSeconds (dashCooldown);

		canDash = true;
	}

	IEnumerator Catch ()
	{
		holdingBall = true;
		holdBall.tag = "HoldBall";
		holdBall.layer = 10;

		holdBall.transform.DOLocalRotate (Vector3.zero, 0.1f);

		AddAndRemoveRigibody (false);

		while(Vector3.Distance(holdBall.transform.position, holdPoint.transform.position) > distanceMinToCatch)
		{
			holdBall.transform.position = Vector3.Lerp(holdBall.transform.position, holdPoint.transform.position, catchLerp);
			yield return null;

			if(holdBall == null)
				break;
		}

		if(holdBall != null)
			holdBall.transform.SetParent (transform);		

		yield return null;
	}

	void ThrowForce ()
	{
		//Debug.Log(DOTween.PlayingTweens ());
			
		throwForceTemp = throwForceMin;
		//Debug.Log("Force After " + throwForceTemp);
		DOTween.To(() => throwForceTemp, x => throwForceTemp = x, throwForceMax, timeToMaxForce).SetId("ThrowForce");
		holdBall.GetComponent<Renderer>().material.DOColor(Color.red, timeToMaxForce).SetId("ThrowForce");
	}

	IEnumerator Throw ()
	{
		holdBall.transform.SetParent (GameObject.FindGameObjectWithTag("BallsParent").transform);
		holdBall.tag = "ThrownBall";

		vibration.VibrateBothMotors(playerId, throwVibration.x, throwVibration.z, throwVibration.y, throwVibration.z);

		AddAndRemoveRigibody (true);

		if(throwDirection.magnitude == 0)
		{
			Vector3 direction = facingLeft ? -Vector3.right : Vector3.right;
			direction.y = 0.2f;

			throwDirection = direction;
		}
			
		holdBall.GetComponent<Rigidbody> ().AddForce (throwDirection * throwForceTemp, ForceMode.VelocityChange);
		//Debug.Log(throwForceTemp);

		holdBall.GetComponent<Renderer>().material.color = Color.white;

		GameObject holdBallTemp = holdBall;
		holdBall = null;

		holdingBall = false;
		charging = false;

		yield return new WaitForSeconds(0.1f);

		//holdBallTemp.GetComponent<Collider>().enabled = true;

		if(holdBallTemp != null)
			holdBallTemp.layer = 0;
	}

	IEnumerator Release ()
	{
		holdBall.transform.SetParent (GameObject.FindGameObjectWithTag("BallsParent").transform);

		AddAndRemoveRigibody (true);

		//holdBall.GetComponent<Rigidbody> ().AddForce (throwDirection * throwForceTemp, ForceMode.VelocityChange);
		//Debug.Log(throwForceTemp);

		holdBall.GetComponent<Renderer>().material.color = Color.white;

		GameObject holdBallTemp = holdBall;
		holdBall = null;

		holdingBall = false;
		charging = false;

		yield return new WaitForSeconds(0.1f);

		holdBallTemp.tag = "Ball";
		//holdBallTemp.GetComponent<Collider>().enabled = true;
		holdBallTemp.layer = 0;
	}

	public void StunVoid ()
	{
		Debug.Log("Stun");

		if(holdingBall)
			StartCoroutine (Release ());
		
		StartCoroutine (Stun ());
	}

	IEnumerator Stun ()
	{
		if(!stunned)
		{
			if(OnStun != null)
				OnStun ();

			stunned = true;

			screenShake.CameraShaking(0.5f, 0.8f);
			vibration.VibrateBothMotors(playerId, stunVibration.x, stunVibration.z, stunVibration.y, stunVibration.z);

			yield return new WaitForSeconds (stunDuration);

			stunned = false;

			IsGrounded ();
		}
	}

	void VibrationDebug ()
	{
		vibration.VibrateBothMotors(0, vibrationTest.x, vibrationDuration, vibrationTest.y, vibrationDuration);
		vibration.VibrateBothMotors(1, vibrationTest.x, vibrationDuration, vibrationTest.y, vibrationDuration);
	}

	void AddAndRemoveRigibody (bool add)
	{
		if(!add)
		{
			mass = holdBall.GetComponent<Rigidbody> ().mass;
			drag = holdBall.GetComponent<Rigidbody> ().drag;
			angularDrag = holdBall.GetComponent<Rigidbody> ().angularDrag;
			useGravity = holdBall.GetComponent<Rigidbody> ().useGravity;
			iskinematic = holdBall.GetComponent<Rigidbody> ().isKinematic;
			physicMat = holdBall.GetComponent<Collider> ().material;

			Destroy (holdBall.GetComponent<Rigidbody> ());
			//holdBall.GetComponent<Collider>().enabled = false;
		}
		else
		{
			holdBall.AddComponent<Rigidbody> ();

			holdBall.GetComponent<Rigidbody> ().mass = mass;
			holdBall.GetComponent<Rigidbody> ().drag = drag;
			holdBall.GetComponent<Rigidbody> ().angularDrag = angularDrag;
			holdBall.GetComponent<Rigidbody> ().useGravity = useGravity;
			holdBall.GetComponent<Rigidbody> ().isKinematic = iskinematic;
			holdBall.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ;
			holdBall.GetComponent<Collider> ().material = physicMat;
		}
	}

	void SetFacing()
	{
		if (player.GetAxis("Movement_Horizontal") < 0 && facingLeft == false)
		{
			facingLeft = true;

			if(OnFacingLeft != null)
				OnFacingLeft ();
		}
		else if (player.GetAxis("Movement_Horizontal") > 0 && facingLeft == true)
		{
			facingLeft = false;
			OnFacinRight ();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(playerState == PlayerState.Dashing && collision.gameObject.tag == "Player")
		{
			if(collision.gameObject.GetComponent<PlayerScript>())
				collision.gameObject.GetComponent<PlayerScript>().StunVoid();
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(!stunned)
		{

			if(other.tag == "ThrownBall" && other.GetComponent<BallScript>().team != team)
			{
				StunVoid ();
			}

			else if(other.tag == "Ball" && !holdingBall)
			{
				Vector3 direction = other.transform.position - transform.position;
				RaycastHit objectHit;

				if(Physics.Raycast (transform.position, direction, out objectHit, 10) && objectHit.collider.tag != "Ground" && objectHit.collider.tag != "Player")
				{
					holdingBall = true;
					holdBall = other.gameObject;
					holdBall.GetComponent<BallScript>().team = team;
					StartCoroutine (Catch ());
				}
			}



			else if(other.tag == "ThrownBall" && !holdingBall && other.GetComponent<BallScript>().team == team)
			{
				Vector3 direction = other.transform.position - transform.position;
				RaycastHit objectHit;

				if(Physics.Raycast (transform.position, direction, out objectHit, 10) && objectHit.collider.tag != "Ground" && objectHit.collider.tag != "Player")
				{
					holdingBall = true;
					holdBall = other.gameObject;
					holdBall.GetComponent<BallScript>().team = team;
					StartCoroutine (Catch ());
				}
			}
		}

	}

	void OnDisable ()
	{
		switch(playerId)
		{
		case 0:
			GamePad.SetVibration (PlayerIndex.One, 0, 0);
			break;
		case 1:
			GamePad.SetVibration (PlayerIndex.Two, 0, 0);
			break;
		case 2:
			GamePad.SetVibration (PlayerIndex.Three, 0, 0);
			break;
		case 3:
			GamePad.SetVibration (PlayerIndex.Four, 0, 0);
			break;
		}
	}

	void OnDestroy ()
	{
		switch(playerId)
		{
		case 0:
			GamePad.SetVibration (PlayerIndex.One, 0, 0);
			break;
		case 1:
			GamePad.SetVibration (PlayerIndex.Two, 0, 0);
			break;
		case 2:
			GamePad.SetVibration (PlayerIndex.Three, 0, 0);
			break;
		case 3:
			GamePad.SetVibration (PlayerIndex.Four, 0, 0);
			break;
		}
	}
}
