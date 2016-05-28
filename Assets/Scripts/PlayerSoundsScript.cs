using UnityEngine;
using System.Collections;
using Rewired;
using DarkTonic.MasterAudio;

public class PlayerSoundsScript : MonoBehaviour 
{
	public PlayerScript playerScript;

	private int playerId = 0;
	public PlayerState playerState;
	private Player player;
	private DashState dashState;

	// Use this for initialization
	void Start () 
	{
		playerId = playerScript.playerId;

		//playerScript.OnJump += Jump;
		playerScript.OnStun += Stun;
		//playerScript.OnGround += Ground;
		playerScript.OnThrow += Throw;
		playerScript.OnDash += Dash;

	}

	// Update is called once per frame
	void Update () 
	{
		playerState = playerScript.playerState;
		player = playerScript.player;
		dashState = playerScript.dashState;
	}

	void Stun ()
	{
		Debug.Log ("Stun");
		MasterAudio.PlaySound3DFollowTransformAndForget ("HIT_PERSO", transform);
	}

	void Throw ()
	{
		MasterAudio.PlaySound3DFollowTransformAndForget ("THROW", transform);
	}

	void Dash ()
	{
		MasterAudio.PlaySound3DFollowTransformAndForget ("DASH", transform);

	}
}
