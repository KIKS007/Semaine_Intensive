using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour 
{

	public Team team;

	private MatchManager matchManager;

	void Start ()
	{
		matchManager = GameObject.FindGameObjectWithTag ("MatchManager").GetComponent<MatchManager> ();
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
			}

			if(team == Team.Team2)
			{
				matchManager.PointToTeam2 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();
			}
		}
	}

	void DestroyBall (GameObject ball)
	{
		Destroy (ball);
	}
}
