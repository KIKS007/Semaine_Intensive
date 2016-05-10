using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
	public Team team;

	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag != "Player")
			gameObject.tag = "Ball";
	}
}
