using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GoalScript : MonoBehaviour 
{
	public Team team;
	public GameObject butParticles;

	[Header("Vibration")]
	public Vector3 goalVibrationForce;

	[Header("Screen Shake")]
	public float screenShakeForce = 1;
	public float screenShakeDuration = 1;

	[Header("Lights")]
	public Material[] lights = new Material[0];
	public float lightingDuration = 0.5f;
	public float lightingGap = 0.8f;
	public float bloomValue;
	public float rgbSpliValue;

	private MatchManager matchManager;

	private CameraScreenShake screenShake;
	private VibrationManager vibration;
	private Color originalColor;
	private float originalBloom;

	void Start ()
	{
		matchManager = GameObject.FindGameObjectWithTag ("MatchManager").GetComponent<MatchManager> ();
		screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScreenShake>();
		vibration = GameObject.FindGameObjectWithTag("VibrationManager").GetComponent<VibrationManager>();
		originalColor = lights[0].GetColor("_Color2");
		originalBloom = lights[0].GetFloat("_Bloom");
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "ThrownBall" && other.GetComponent<BallScript>().team != Team.None || other.tag == "Ball" && other.GetComponent<BallScript>().team != Team.None )
		{
			if(team == Team.Team1)
			{
				InstantiateBallParticles (other.transform.position);

				matchManager.PointToTeam2 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();
			}

			if(team == Team.Team2)
			{
				InstantiateBallParticles (other.transform.position);

				matchManager.PointToTeam1 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();
	
			}

			StartCoroutine (WaitAndDestroy ());

			StartCoroutine (GoalLights ());
			screenShake.CameraShaking(screenShakeDuration, screenShakeForce);
			GoalVibration ();

			if(matchManager.switchGoals)
				matchManager.SwitchGoals (gameObject);
		}
	}

	void InstantiateBallParticles (Vector3 pos)
	{
		Instantiate (butParticles, pos, butParticles.transform.rotation);
	}

	void GoalVibration ()
	{
		vibration.VibrateBothMotors (0, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z, 0.5f, 0.5f);
		vibration.VibrateBothMotors (1, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z, 0.5f, 0.5f);
		vibration.VibrateBothMotors (2, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z, 0.5f, 0.5f);
		vibration.VibrateBothMotors (3, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z, 0.5f, 0.5f);
	}

	IEnumerator GoalLights ()
	{
		Color tempColor = GetComponent<Renderer> ().material.GetColor ("_Color2");

		for(int i = 0; i < lights.Length; i++)
		{
			lights[i].DOColor(tempColor, "_Color2", lightingDuration);
			lights[i].DOFloat(bloomValue, "_Bloom", lightingDuration);
		}

		yield return new WaitForSeconds (lightingDuration);

		for(int i = 0; i < lights.Length; i++)
		{
			lights[i].DOColor(originalColor, "_Color2", lightingDuration);
			lights[i].DOFloat(originalBloom, "_Bloom", lightingDuration);
		}



		yield return new WaitForSeconds (lightingGap);

		for(int i = 0; i < lights.Length; i++)
		{
			lights[i].DOColor(tempColor, "_Color2", lightingDuration);
			lights[i].DOFloat(bloomValue, "_Bloom", lightingDuration);
		}

		yield return new WaitForSeconds (lightingDuration);

		for(int i = 0; i < lights.Length; i++)
		{
			lights[i].DOColor(originalColor, "_Color2", lightingDuration);
			lights[i].DOFloat(originalBloom, "_Bloom", lightingDuration);
		}
	}

	void DestroyBall (GameObject ball)
	{
		Destroy (ball);
	}

	IEnumerator WaitAndDestroy ()
	{
		Destroy (gameObject.GetComponent<Renderer>());
		Destroy (gameObject.GetComponent<Collider>());
		Destroy (transform.GetChild (0).gameObject);
		Destroy (transform.GetChild (1).gameObject);

		yield return new WaitForSeconds (5);

		Destroy (gameObject);
	}
}
