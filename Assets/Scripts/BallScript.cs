using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
	public Team team;

	public float gravityForce;

	private Transform ballParticles;
	private Rigidbody rb;

	void Start ()
	{
		ballParticles = transform.GetChild (0).transform;
		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		if(gameObject.tag == "Ball" || gameObject.tag == "ThrownBall")
		{
			if(rb == null)
				rb = GetComponent<Rigidbody> ();
		}

		/*if (rb != null && gameObject.tag == "ThrownBall")
			SetParticulesRotation ();*/
	}

	void FixedUpdate ()
	{


		if(rb != null)
		{
			rb.AddForce(new Vector3 (0, -gravityForce, 0), ForceMode.Acceleration);
		}

	}

	void SetParticulesRotation ()
	{
		Quaternion rotation = Quaternion.LookRotation (-rb.velocity.normalized, Vector3.up);
		//rotation.eulerAngles = new Vector3 (0, 0, rotation.eulerAngles.z);
		ballParticles.localRotation = rotation;

		Debug.Log (rotation);
	}

	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag != "Player")
			gameObject.tag = "Ball";

		if(gameObject.tag == "ThrownBall" && collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerScript>().team != team)
		{
			collision.gameObject.GetComponent<PlayerScript>().StunVoid ();
		}
	}
}
