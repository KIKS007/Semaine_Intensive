using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	[Header ("Camera Bounds Position")]
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float cameraLerp;

	private GameObject[] players;

	private Vector3 centerTargetPos = new Vector3 (0, 0, 0);

	// Use this for initialization
	void Start () 
	{
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindCenterPoint ();
	}

	void FixedUpdate ()
	{
		SetCameraPosition ();
	}

	void FindCenterPoint ()
	{
		centerTargetPos = new Vector3 (0, 0, 0);

		float xBoundMin = 0;
		float xBoundMax = 0;
		float yBoundMin = 0;
		float yBoundMax = 0;

		if(players.Length == 1)
		{
			centerTargetPos = players[0].transform.position;
		}

		if(players.Length == 2)
		{
			xBoundMin = players[0].transform.position.x ;
			xBoundMax = players[0].transform.position.x ;
			yBoundMin = players[0].transform.position.y ;
			yBoundMax = players[0].transform.position.y ;

			if(players[1].transform.position.x < xBoundMin)
				xBoundMin = players[1].transform.position.x;

			if(players[1].transform.position.x > xBoundMax)
				xBoundMax = players[1].transform.position.x;

			if(players[1].transform.position.y < yBoundMin)
				yBoundMin = players[1].transform.position.y;

			if(players[1].transform.position.y > yBoundMax)
				yBoundMax = players[1].transform.position.y;

			centerTargetPos.x = (xBoundMin + xBoundMax) / 2;
			centerTargetPos.y = (yBoundMin + yBoundMax) / 2;
		}

		if(players.Length > 2)
		{
			xBoundMin = players[0].transform.position.x ;
			xBoundMax = players[0].transform.position.x ;
			yBoundMin = players[0].transform.position.y ;
			yBoundMax = players[0].transform.position.y ;

			for(int i = 0; i < players.Length; i++)
			{
				if(players[i].transform.position.x < xBoundMin)
					xBoundMin = players[i].transform.position.x;

				if(players[i].transform.position.x > xBoundMax)
					xBoundMax = players[i].transform.position.x;

				if(players[i].transform.position.y < yBoundMin)
					yBoundMin = players[i].transform.position.y;

				if(players[i].transform.position.y > yBoundMax)
					yBoundMax = players[i].transform.position.y;
			}

			centerTargetPos.x = (xBoundMin + xBoundMax) / players.Length;
			centerTargetPos.y = (yBoundMin + yBoundMax) / players.Length;
		}
	}

	void SetCameraPosition ()
	{
		Vector3 cameraPos = new Vector3 ();

		if(centerTargetPos.x > xMax)
			cameraPos.x = xMax;

		else if(centerTargetPos.x < xMin)
			cameraPos.x = xMin;
		else
			cameraPos.x = centerTargetPos.x;

		if(centerTargetPos.y > yMax)
			cameraPos.y = yMax;

		else if(centerTargetPos.y < yMin)
			cameraPos.y = yMin;
		else
			cameraPos.y = centerTargetPos.y;


		cameraPos.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, cameraPos, cameraLerp);
	}
}
