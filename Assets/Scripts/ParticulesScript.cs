using UnityEngine;
using System.Collections;

public class ParticulesScript : MonoBehaviour 
{
	[Header ("Particules")]
	public GameObject stunParticlues;
	public GameObject dashParticles;
	public GameObject hitParticlesTeam1;
	public GameObject hitParticlesTeam2;

	private PlayerScript playerScript;

	private GameObject stunParticlesClones;

	private GameObject dashParticlesClones;

	// Use this for initialization
	void Start () 
	{
		playerScript = GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerScript.stunned == true && stunParticlesClones == null)
			StartCoroutine (Stun ());

		if(playerScript.dashState == DashState.Dashing && dashParticlesClones == null)
			StartCoroutine (Dash ());

	}

	IEnumerator Stun ()
	{
		Vector3 pos = transform.position;
		pos.y += 1;
		stunParticlesClones = Instantiate (stunParticlues, pos, stunParticlues.transform.rotation) as GameObject;
		stunParticlesClones.transform.parent = transform;

		yield return new WaitForSeconds (stunParticlesClones.GetComponent<ParticleSystem>().duration);

		Destroy (stunParticlesClones);
	}

	IEnumerator Dash ()
	{
		Vector3 pos = transform.position;
		pos.y += 0.3f;
		dashParticlesClones = Instantiate (dashParticles, pos, dashParticles.transform.rotation) as GameObject;
		dashParticlesClones.transform.parent = transform;

		Vector3 rot = dashParticlesClones.transform.localRotation.eulerAngles;
		rot.y = playerScript.facingLeft ? 90 : -90;
		dashParticlesClones.transform.localRotation = Quaternion.Euler(rot);

		yield return new WaitForSeconds (dashParticlesClones.GetComponent<ParticleSystem>().duration);

		Destroy (dashParticlesClones);
	}
}
