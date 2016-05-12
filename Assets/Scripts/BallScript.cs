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

		/*if (rb != null && gameObject.tag == "ThrownBall")
			SetParticulesRotation ();*/
	}

	void FixedUpdate ()
	{
		if(rb != null)
			Debug.Log(rb.velocity.magnitude);

		if(rb != null && rb.velocity.magnitude < speedLimitToFall)
		{
			rb.AddForce(new Vector3 (0, -gravityWhenNoVelocity, 0), ForceMode.Acceleration);
		}

	}

	void SetParticulesRotation ()
	{
		Quaternion rotation = Quaternion.LookRotation (-rb.velocity.normalized, Vector3.up);
		//rotation.eulerAngles = new Vector3 (0, 0, rotation.eulerAngles.z);
		ballParticules.localRotation = rotation;

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

		if(rb != null && rb.velocity.magnitude > speedLimitToFall)
			StartCoroutine (ParticulesImpact (collision.contacts[0].point));
	}

	IEnumerator ParticulesImpact (Vector3 pos)
	{
		GameObject particuleClone = Instantiate(ballImpactParticules, pos, ballImpactParticules.transform.rotation) as GameObject;

		yield return new WaitForSeconds(ballImpactParticules.GetComponent<ParticleSystem>().duration);

		Destroy (particuleClone);
	}
}
