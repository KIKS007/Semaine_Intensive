using UnityEngine;
using System.Collections;
using Rewired;

public class AnimationScript : MonoBehaviour 
{
	public Animator anim;

	public PlayerScript playerScript;

	private int playerId = 0;
	private PlayerState playerState;
	private Player player;

	// Use this for initialization
	void Start () 
	{
		playerId = playerScript.playerId;

		playerScript.OnFacingLeft = FaceLeft ;
		playerScript.OnFacinRight = FaceRight ;
		playerScript.OnJump = Jump;
		playerScript.OnStun = Jump;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerState = playerScript.playerState;
		player = playerScript.player;


		if(playerState == PlayerState.Dashing)
		{
			//Debug.Log("Dash");

			anim.Play("dash");
		}

		if(playerState == PlayerState.OnGround)
		{
			if(player.GetAxis("Movement_Horizontal") == 0)
			{
				anim.Play("iddle");
			}

			else if(player.GetAxis("Movement_Horizontal") != 0)
			{
				anim.Play("marche");
			}	

		}
			
	}

	void Stun ()
	{
		anim.Play ("degats");
	}

	void Jump ()
	{
		anim.Play ("saut");
	}

	void FaceRight ()
	{
		transform.Rotate(new Vector3(0, 180, 0));
	}

	void FaceLeft ()
	{
		transform.Rotate(new Vector3(0, 180, 0));
	}
}
