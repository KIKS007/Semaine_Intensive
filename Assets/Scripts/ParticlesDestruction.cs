using UnityEngine;
using System.Collections;

public class ParticlesDestruction : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (WaitBeforeDestroy ());
	}
	
	IEnumerator WaitBeforeDestroy ()
	{
		ParticleSystem particles = GetComponent<ParticleSystem> ();

		yield return new WaitUntil (()=> particles.isStopped);

		Destroy (gameObject);
	}
}
