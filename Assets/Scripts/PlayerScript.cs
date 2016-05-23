using UnityEngine;
using System.Collections;
using System;
using Rewired;
using DG.Tweening;
using XInputDotNetPure;

public enum Team
{
	Team1,
	Team2,
	None
}

public enum PlayerState
{
	OnGround,
	InAir,
	Falling
}

public enum JumpState
{
	CanJump,
	HasJumped,
	HasDoubleJumped
}

public enum DashState
{
	CanDash,
	Dashing,
	CoolDown
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
	public DashState dashState = DashState.CanDash;
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
	public GameObject throwParticules;

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
	public GameObject stunParticulesTeam1;
	public GameObject stunParticulesTeam2;


	[Header ("Screen Shake")]
	public Vector2 stunScreenShake;

	private CameraScreenShake screenShake;


	[Header ("Vibration")]
	public Vector3 jumpVibration;
	public Vector3 groundVibration;
	public Vector3 dashVibration;
	public Vector3 stunVibration;
	public Vector3 throwVibration;
	public Vector3 destroyVibration;

	private VibrationManager vibration;

	public bool vibrate = false;
	public Vector2 vibrationTest;
	public float vibrationDuration;

	public bool stunned = false;
	public bool passingThrough = false;

	public GameObject lastHoldBall;

	private MatchManager matchManager;

	// Use this for initialization
	void Start () 
	{
		player = ReInput.players.GetPlayer(playerId);
		rb = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider> ().bounds.extents.y;
		screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScreenShake>();
		vibration = GameObject.FindGameObjectWithTag("VibrationManager").GetComponent<VibrationManager>();
		matchManager = GameObject.FindGameObjectWithTag ("MatchManager").GetComponent<MatchManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!passingThrough)
			IsGrounded ();

		CanPassThrough ();
	
		if(!stunned)
			SetFacing();

		throwDirection = new Vector3(player.GetAxis("Aim_Horizontal"), player.GetAxis("Aim_Vertical"), 0);

		if(holdingBall && !stunned)
		{
			/*if(player.GetButton("Throw") && !charging)
			{
				charging = true;
				//Debug.Log("Force Before " + throwForceTemp);
				//DOTween.Pause("ThrowForce");
				//DOTween.Complete("ThrowForce");
				ThrowForce ();
			}*/

			if(player.GetButtonDown("Throw"))
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


		if(player.GetButtonDown("Jump") && player.GetAxis("Movement_Vertical") < -0.8f && playerState == PlayerState.OnGround && !passingThrough)
		{
			StartCoroutine (CanPassThrough ());
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (dashState != DashState.Dashing)
		{
			rb.useGravity = true;
			Gravity ();
		}
		else
			rb.useGravity = false;


		if(!stunned)
		{
			if (dashState != DashState.Dashing)
			{
				Movement ();
			}

			if(jumpState == JumpState.HasJumped)
				DoubleJump ();

			if(jumpState == JumpState.CanJump)
				Jump ();

			if (canDash && dashState == DashState.CanDash && player.GetButtonDown("Dash"))
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
		if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f))
		{

			if(playerState != PlayerState.OnGround)
			{
				playerState = PlayerState.OnGround;
				jumpState = JumpState.CanJump;

				if(OnGround != null)
					OnGround ();

				vibration.VibrateBothMotors(playerId, groundVibration.x, groundVibration.z, groundVibration.y, groundVibration.z);
			}

			if (gameObject.layer != 9)
				gameObject.layer = 9;

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

	IEnumerator CanPassThrough ()
	{
		passingThrough = true;

		RaycastHit hitInfo;

		if(Physics.Raycast(transform.position, -Vector3.up, out hitInfo, distToGround + 0.01f) && hitInfo.collider.gameObject.layer != 0)
		{
			playerState = PlayerState.Falling;
			gameObject.layer = 13;


			yield return new WaitForSeconds (0.1f);
		}

		passingThrough = false;

		yield return null;
	}

	void Jump ()
	{
		if(player.GetButtonDown("Jump") && player.GetAxis("Movement_Vertical") > -0.8f)
		{
			rb.velocity = new Vector3 (rb.velocity.x, jumpForce, rb.velocity.z);
			jumpState = JumpState.HasJumped;

			OnJump ();

			gameObject.layer = 12;
		}
	}

	void DoubleJump ()
	{
		if(player.GetButtonDown("Jump") && player.GetAxis("Movement_Vertical") > -0.8f)
		{
			rb.velocity = new Vector3 (rb.velocity.x, doubleJumpForce, rb.velocity.z);
			jumpState = JumpState.HasDoubleJumped;

			if(OnJump != null)
				OnJump ();
		}
	}

	IEnumerator Dash ()
	{
		float dashForceTemp = dashForce;

		canDash = false;
		dashState = DashState.Dashing;

		vibration.VibrateBothMotors(playerId, dashVibration.x, dashVibration.z, dashVibration.y, dashVibration.z);

		dashForceTemp = facingLeft ? -dashForce : dashForce;

		DOTween.To (() => dashForceTemp, x => dashForceTemp = x, 0, dashDuration).SetEase (dashEase).OnUpdate(
			()=> rb.velocity = new Vector3(dashForceTemp, 0, rb.velocity.z));

		yield return new WaitForSeconds (dashDuration - 0.05f);

		dashState = DashState.CoolDown;

		IsGrounded ();

		yield return new WaitForSeconds (dashCooldown);

		canDash = true;

		dashState = DashState.CanDash;
	}

	IEnumerator Catch ()
	{
		lastHoldBall = holdBall;
		Physics.IgnoreCollision (gameObject.GetComponent<Collider> (), lastHoldBall.GetComponent<Collider> ());

		holdingBall = true;
		holdBall.tag = "HoldBall";
		//holdBall.layer = 10;
		holdBall.GetComponent<BallScript> ().team = team;

		holdBall.transform.DOLocalRotate (Vector3.zero, 0.1f);

		AddAndRemoveRigibody (false);

		StartCoroutine (SetBalPosition ());

		if(holdBall != null)
			holdBall.transform.SetParent (transform);		

		if (holdBall != null)
			holdBall.GetComponent<Collider> ().enabled = false;

		yield return null;
	}

	void ThrowForce ()
	{
		throwForceTemp = throwForceMin;
		//Debug.Log("Force After " + throwForceTemp);

		DOTween.To(() => throwForceTemp, x => throwForceTemp = x, throwForceMax, timeToMaxForce).SetId("ThrowForce");
	}

	IEnumerator Throw ()
	{
		holdBall.transform.SetParent (GameObject.FindGameObjectWithTag("BallsParent").transform);
		holdBall.tag = "ThrownBall";
		holdBall.GetComponent<BallScript> ().CheckTeamVoid ();

		if(OnThrow != null)
			OnThrow ();

		ThrowParticules ();
		StopCoroutine (ForgetBall ());
		StartCoroutine (ForgetBall ());

		vibration.VibrateBothMotors(playerId, throwVibration.x, throwVibration.z, throwVibration.y, throwVibration.z);

		AddAndRemoveRigibody (true);

		if(throwDirection.magnitude == 0)
		{
			Vector3 direction = facingLeft ? -Vector3.right : Vector3.right;
			direction.y = 0;

			throwDirection = direction;
		}
			
		holdBall.GetComponent<Rigidbody> ().AddForce (throwDirection * throwForceMax, ForceMode.VelocityChange);

		GameObject holdBallTemp = holdBall;
		holdBall = null;

		holdingBall = false;
		charging = false;

		holdBallTemp.GetComponent<Collider> ().enabled = true;

		yield return null;
	}

	void ThrowParticules ()
	{
		Instantiate (throwParticules, holdPoint.position, throwParticules.transform.rotation);
	}

	IEnumerator ForgetBall ()
	{
		yield return new WaitForSeconds (0.15f);

		if(!holdingBall)
		{
			if(lastHoldBall != null)
				Physics.IgnoreCollision (gameObject.GetComponent<Collider> (), lastHoldBall.GetComponent<Collider> (), false);

			lastHoldBall = null;
		}

	}

	IEnumerator Release ()
	{
		holdBall.transform.SetParent (GameObject.FindGameObjectWithTag("BallsParent").transform);
		holdBall.GetComponent<BallScript> ().CheckTeamVoid ();

		AddAndRemoveRigibody (true);
		StartCoroutine (ForgetBall ());

		GameObject holdBallTemp = holdBall;
		holdBall = null;

		holdingBall = false;
		charging = false;

		holdBallTemp.GetComponent<Collider> ().enabled = true;

		yield return new WaitForSeconds(0.06f);

		holdBallTemp.tag = "Ball";
	}

	public void StunVoid ()
	{
		//Debug.Log("Stun");

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
			vibration.VibrateBothMotors(playerId, stunVibration.x, stunVibration.z, stunVibration.y, stunVibration.z, 0, 1);

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

			if(OnFacinRight != null)
				OnFacinRight ();
		}
	}

	IEnumerator SetBalPosition ()
	{
		while(holdingBall)
		{
			holdBall.transform.position = Vector3.Lerp(holdBall.transform.position, holdPoint.transform.position, catchLerp);
			yield return null;

			if(holdBall == null)
				break;
		}

		yield return null;
	}

	void OnCollisionStay(Collision collision)
	{
		if(dashState == DashState.Dashing && collision.gameObject.tag == "Player")
		{
			if(collision.gameObject.GetComponent<PlayerScript> ().stunned == false)
			{
				collision.gameObject.GetComponent<PlayerScript> ().StunVoid ();
				StunParticules (collision.gameObject.GetComponent<PlayerScript>().team, collision.contacts[0].point);
			}
		}
	}

	void StunParticules (Team whichTeam, Vector3 pos)
	{
		if(whichTeam == Team.Team1)
		{
			Instantiate(stunParticulesTeam1, pos, stunParticulesTeam1.transform.rotation);
		}
		else
		{
			Instantiate(stunParticulesTeam2, pos, stunParticulesTeam2.transform.rotation);
		}
	}

	void OnTriggerStay (Collider other)
	{
		if(!stunned)
		{

			if(other.tag == "ThrownBall" && other.GetComponent<BallScript>().team != Team.None && other.GetComponent<BallScript>().team != team )
			{
				Vector3 direction = other.transform.position - transform.position;
				RaycastHit objectHit;

				if(Physics.Raycast (transform.position, direction, out objectHit, 10) && objectHit.collider.tag != "Plateformes" && objectHit.collider.tag != "Player")
				{
					other.GetComponent<BallScript> ().team = Team.None;

					if(holdingBall)
						StartCoroutine (Release ());

					StopCoroutine (ForgetBall ());
					lastHoldBall = null;

					matchManager.DestroyPlayerVoid (gameObject, team);

					vibration.VibrateBothMotors(playerId, destroyVibration.x, destroyVibration.z, destroyVibration.y, destroyVibration.z);
				}
			}

			else if(other.gameObject != lastHoldBall)
			{
				if(other.tag == "Ball" && !holdingBall)
				{
					Vector3 direction = other.transform.position - transform.position;
					RaycastHit objectHit;

					if(Physics.Raycast (transform.position, direction, out objectHit, 10) && objectHit.collider.tag != "Plateformes" && objectHit.collider.tag != "Player")
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

					if(Physics.Raycast (transform.position, direction, out objectHit, 10) && objectHit.collider.tag != "Plateformes" && objectHit.collider.tag != "Player")
					{
						holdingBall = true;
						holdBall = other.gameObject;
						holdBall.GetComponent<BallScript>().team = team;
						StartCoroutine (Catch ());
					}
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
