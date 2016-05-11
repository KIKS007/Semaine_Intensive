using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
	public Team team;

	public float gravityForce;

	void FixedUpdate ()
	{
		if(GetComponent<Rigidbody>() != null)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravityForce, 0), ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag != "Player")
			gameObject.tag = "Ball";

		if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerScript>().team != team)
		{
			collision.gameObject.GetComponent<PlayerScript>().StunVoid ();
		}
	}
}
