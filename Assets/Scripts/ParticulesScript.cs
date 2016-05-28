using UnityEngine;
using System.Collections;

public class ParticulesScript : MonoBehaviour 
{
	[Header ("Particules")]
	public GameObject stunParticlues;
	public GameObject dashParticles;
	public GameObject hitParticlesTeam1;
	public GameObject hitParticlesTeam2;
	public GameObject onGroundParticles;

	private PlayerScript playerScript;

	private GameObject stunParticlesClones;

	private GameObject dashParticlesClones;

	// Use this for initialization
	void Start () 
	{
		playerScript = GetComponent<PlayerScript> ();
		playerScript.OnGround += Ground;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerScript.stunned == true && stunParticlesClones == null)
			Stun ();

		if(playerScript.dashState == DashState.Dashing && dashParticlesClones == null)
			Dash ();

	}

	void Ground ()
	{
		Vector3 pos = transform.position;
		pos.y -= 0.8f;
		Instantiate (onGroundParticles, pos, onGroundParticles.transform.rotation);
	}

	void Stun ()
	{
		Vector3 pos = transform.position;
		pos.y += 1;
		stunParticlesClones = Instantiate (stunParticlues, pos, stunParticlues.transform.rotation) as GameObject;
		stunParticlesClones.transform.parent = transform;
	}

	void Dash ()
	{
		Vector3 pos = transform.position;
		pos.y += 0.3f;
		dashParticlesClones = Instantiate (dashParticles, pos, dashParticles.transform.rotation) as GameObject;
		dashParticlesClones.transform.parent = transform;

		Vector3 rot = dashParticlesClones.transform.localRotation.eulerAngles;
		rot.y = playerScript.facingLeft ? 90 : -90;
		dashParticlesClones.transform.localRotation = Quaternion.Euler(rot);
	}
}
