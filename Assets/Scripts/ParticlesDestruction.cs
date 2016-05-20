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
		yield return new WaitForSeconds (GetComponent<ParticleSystem> ().duration);
		Destroy (gameObject);
	}
}
