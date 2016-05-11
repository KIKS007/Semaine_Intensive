﻿using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour 
{

	public Team team;

	private MatchManager matchManager;

	private CameraScreenShake screenShake;

	void Start ()
	{
		matchManager = GameObject.FindGameObjectWithTag ("MatchManager").GetComponent<MatchManager> ();
		screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScreenShake>();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "ThrownBall" || other.tag == "Ball")
		{
			if(team == Team.Team1)
			{
				matchManager.PointToTeam1 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();

				screenShake.CameraShaking(1, 1);
			}

			if(team == Team.Team2)
			{
				matchManager.PointToTeam2 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();

				screenShake.CameraShaking(1, 1);
			}
		}
	}

	void DestroyBall (GameObject ball)
	{
		Destroy (ball);
	}
}
