using UnityEngine;
using System.Collections;
using Rewired;

public class AnimationScript : MonoBehaviour 
{
	public Animator anim;
	public Transform transformToRotate;
	public PlayerScript playerScript;

	private int playerId = 0;
	public PlayerState playerState;
	private Player player;
	private DashState dashState;

	// Use this for initialization
	void Start () 
	{
		playerId = playerScript.playerId;

		//playerScript.OnFacingLeft = FaceLeft ;
		//playerScript.OnFacinRight = FaceRight ;
		playerScript.OnJump = Jump;
		playerScript.OnStun = Jump;
		playerScript.OnGround = Ground;

	}
	
	// Update is called once per frame
	void Update () 
	{
		playerState = playerScript.playerState;
		player = playerScript.player;
		dashState = playerScript.dashState;

		if(dashState == DashState.Dashing)
		{
			//Debug.Log("Dash");

			anim.Play("avant dash");
		}



		if(playerState == PlayerState.OnGround && player.GetAxis("Movement_Horizontal") == 0)
		{
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("atterissage") && !anim.GetCurrentAnimatorStateInfo(0).IsName("iddle"))
				anim.SetTrigger("iddle");
		}
		else if(player.GetAxis("Movement_Horizontal") != 0)
		{
			anim.Play("marche");
		}

		

		/*if(playerState == PlayerState.OnGround)
		{
			if(player.GetAxis("Movement_Horizontal") == 0)
			{
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("atterissage") && !anim.GetCurrentAnimatorStateInfo(0).IsName("iddle"))
					anim.Play("tourne pour iddle");
			}

			else if(player.GetAxis("Movement_Horizontal") != 0)
			{
				anim.Play("marche");
			}	

		}*/

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("iddle"))
		{
			if(playerScript.facingLeft)
			{
				Vector3 rot = transformToRotate.rotation.eulerAngles;
				rot.y = 180;

				transformToRotate.rotation = Quaternion.Euler (rot);
			}
				
			else
			{
				Vector3 rot = transformToRotate.rotation.eulerAngles;
				rot.y = 90;

				transformToRotate.rotation = Quaternion.Euler (rot);
			}
				
		}

		else
		{
			if(playerScript.facingLeft)
			{
				Vector3 rot = transformToRotate.rotation.eulerAngles;
				rot.y = -90;

				transformToRotate.rotation = Quaternion.Euler (rot);
			}
			else
			{
				Vector3 rot = transformToRotate.rotation.eulerAngles;
				rot.y = 90;

				transformToRotate.rotation = Quaternion.Euler (rot);
			}


		}
	}

	void Ground ()
	{
		anim.Play ("atterissage");
	}

	void Stun ()
	{
		anim.Play ("degats");
	}

	void Jump ()
	{
		anim.SetTrigger ("saut");
	}

	void FaceRight ()
	{
		Vector3 rot = transformToRotate.rotation.eulerAngles;
		rot.y = 90;

		transformToRotate.rotation = Quaternion.Euler (rot);
	}

	void FaceLeft ()
	{
		Vector3 rot = transformToRotate.rotation.eulerAngles;
		rot.y = 180;

		transformToRotate.rotation = Quaternion.Euler (rot);
	}
}
