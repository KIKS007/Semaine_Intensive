using UnityEngine;
using System.Collections;
using DG.Tweening;
using Rewired;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour 
{
	public GameObject cursor;

	private int playerId = 0;
	public PlayerState playerState;

	private Player player;
	private PlayerScript playerScript;

	private Quaternion rotationOffset;
	private bool opaque = false;

	// Use this for initialization
	void Start () 
	{
		playerScript = GetComponent<PlayerScript>();
		playerId = playerScript.playerId;
		rotationOffset = cursor.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerState = playerScript.playerState;
		player = playerScript.player;

		FollowPlayer ();
		SetRotation ();
		SetOpacity ();
	}

	void SetOpacity ()
	{
		if(playerState == PlayerState.Aiming && !opaque)
		{
			opaque = true;
			cursor.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.25f);
		}

		else if(playerState != PlayerState.Aiming && opaque)
		{
			opaque = false;
			cursor.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.25f);
		}

	}

	void FollowPlayer ()
	{
		cursor.transform.position = transform.position;
	}

	void SetRotation ()
	{
		Vector3 direction;

		if(playerScript.throwDirection.magnitude != 0)
		{
			direction = playerScript.throwDirection.normalized;

			var rotation = Quaternion.AngleAxis(-Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg, Vector3.back);
			cursor.transform.rotation = rotation;
		}
	}
}
