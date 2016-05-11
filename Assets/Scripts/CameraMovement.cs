using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public Transform centerTarget;

	[Header ("Camera Bounds Position")]
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	[Header ("Zoom")]
	public float cameraLerp;
	public float cameraZNewPosition;

	[Header ("Camera Bounds Position")]
	public float xDistanceFromCamera;
	public float yDistanceFromCamera;
	public float distanceCameraGap;

	private GameObject[] players;

	private Vector3 centerTargetPos = new Vector3 (0, 0, 0);

	private float cameraZPos;

	private int zoom;

	public int[] playersZoom;

	// Use this for initialization
	void Start () 
	{
		players = GameObject.FindGameObjectsWithTag("Player");

		cameraZPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindCenterPoint ();
	}

	void FixedUpdate ()
	{
		SetZoom ();
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
			centerTarget.position = centerTargetPos;
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

			centerTarget.position = centerTargetPos;
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

			centerTarget.position = centerTargetPos;
		}


		Debug.Log(xBoundMin);
		Debug.Log(xBoundMax);
		Debug.Log(yBoundMin);
		Debug.Log(yBoundMax);
	}

	void SetCameraPosition ()
	{
		Vector3 cameraPos = new Vector3 ();

		if(centerTargetPos.x > xMax)
			cameraPos.x = xMax;
		else
			cameraPos.x = centerTargetPos.x;

		if(centerTargetPos.x < xMin)
			cameraPos.x = xMin;
		else
			cameraPos.x = centerTargetPos.x;

		if(centerTargetPos.y > yMax)
			cameraPos.y = yMax;
		else
			cameraPos.y = centerTargetPos.y;

		if(centerTargetPos.y < yMin)
			cameraPos.y = yMin;
		else
			cameraPos.y = centerTargetPos.y;


		cameraPos.z = transform.position.z;
		transform.position = cameraPos;
	}

	void SetZoom ()
	{
		playersZoom = new int[players.Length];

		for(int i = 0; i < players.Length; i++)
		{
			if(players[i].transform.position.x > -xDistanceFromCamera && players[i].transform.position.x < -xDistanceFromCamera + distanceCameraGap
				|| players[i].transform.position.x < xDistanceFromCamera && players[i].transform.position.x > xDistanceFromCamera - distanceCameraGap)
				playersZoom[i] = 0;

			else if(players[i].transform.position.x > -xDistanceFromCamera && players[i].transform.position.x < xDistanceFromCamera)
				playersZoom[i] = 1;

			else if(players[i].transform.position.x < -xDistanceFromCamera || players[i].transform.position.x > xDistanceFromCamera)
				playersZoom[i] = -1;


			/*if(players[i].transform.position.y < -yDistanceFromCamera)
				playersZoom[i] = -1;

			else if(players[i].transform.position.y > yDistanceFromCamera)
				playersZoom[i] = -1;

			else if(players[i].transform.position.y > -yDistanceFromCamera && players[i].transform.position.y < yDistanceFromCamera)
				playersZoom[i] = 0;*/
		}

		switch(playersZoom.Length)
		{
		case 2:
			if(playersZoom[0] == -1 || playersZoom[1] == -1)
				ZoomOut ();
			else if(playersZoom[0] == 1 || playersZoom[1] == 1)
				ZoomIn ();
			break;
		case 3:
			if(playersZoom[0] == -1 || playersZoom[1] == -1 || playersZoom[2] == -1)
				ZoomOut ();
			else if(playersZoom[0] == 1 || playersZoom[1] == 1 || playersZoom[2] == 1)
				ZoomIn ();
			break;
		case 4:
			if(playersZoom[0] == -1 || playersZoom[1] == -1 || playersZoom[2] == -1 || playersZoom[3] == -1)
				ZoomOut ();
			else if(playersZoom[0] == 1 || playersZoom[1] == 1 || playersZoom[2] == 1 || playersZoom[3] == 1)
				ZoomIn ();
			break;
		}

	}

	void ZoomIn ()
	{
		Vector3 v3 = transform.position;
		v3.z = Mathf.Lerp(v3.z, v3.z + cameraZNewPosition, cameraLerp);

		transform.position = v3;
	}

	void ZoomOut ()
	{
		Vector3 v3 = transform.position;
		v3.z = Mathf.Lerp(v3.z, v3.z - cameraZNewPosition, cameraLerp);

		transform.position = v3;
	}
}
