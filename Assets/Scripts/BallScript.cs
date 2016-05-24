using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
	public Team team;

	public float gravityWhenNoVelocity;
	public float speedLimitToFall = 8;

	public GameObject ballImpactParticules;

	private Transform ballParticules;
	private Rigidbody rb;


	public bool checkTeam = true;

	void Start ()
	{
		ballParticules = transform.GetChild (0).transform;
		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		if(gameObject.tag == "Ball" || gameObject.tag == "ThrownBall")
		{
			if(rb == null)
				rb = GetComponent<Rigidbody> ();
		}
	}

	void FixedUpdate ()
	{
		/*if(rb != null)
			Debug.Log(rb.velocity.magnitude);*/

		if(rb != null && rb.velocity.magnitude < speedLimitToFall)
		{
			rb.AddForce(new Vector3 (0, -gravityWhenNoVelocity, 0), ForceMode.Acceleration);

			if(team != Team.None && checkTeam)
			{
				//Debug.Log ("None");
				team = Team.None;
			}
		}

	}

	public void CheckTeamVoid ()
	{
		StartCoroutine (CheckTeam ());
	}

	IEnumerator CheckTeam ()
	{
		checkTeam = false;

		yield return new WaitForSeconds (0.1f);

		checkTeam = true;
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			//Debug.Log ("Touched Player");
		}

		if(collision.gameObject.tag != "Player")
		{
			gameObject.tag = "Ball";
		}

		if(rb != null && rb.velocity.magnitude > speedLimitToFall)
			ParticulesImpact (collision.contacts[0].point);
	}

	void ParticulesImpact (Vector3 pos)
	{
		Instantiate(ballImpactParticules, pos, ballImpactParticules.transform.rotation);
	}
}
