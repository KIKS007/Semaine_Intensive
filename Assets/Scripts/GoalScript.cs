using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GoalScript : MonoBehaviour 
{
	public GameObject butParticles;

	[Header("Vibration")]
	public Vector3 goalVibrationForce;

	[Header("Screen Shake")]
	public float screenShakeForce = 1;
	public float screenShakeDuration = 1;

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
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "ThrownBall" && other.gameObject.GetComponent<BallScript>().team != Team.None || other.gameObject.tag == "Ball" && other.gameObject.GetComponent<BallScript>().team != Team.None )
		{
			if(other.gameObject.GetComponent<BallScript>().team == Team.Team1)
			{
				InstantiateBallParticles (other.transform.position);

				matchManager.PointToTeam1 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();
			}

			if(other.gameObject.GetComponent<BallScript>().team == Team.Team2)
			{
				InstantiateBallParticles (other.transform.position);

				matchManager.PointToTeam2 (1);
				DestroyBall (other.gameObject);
				matchManager.InstantiateBall ();
	
			}
				
			screenShake.CameraShaking(screenShakeDuration, screenShakeForce);
			GoalVibration ();

			if(matchManager.switchGoals)
				matchManager.SwitchGoals (gameObject);
		}

		if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerScript>().holdingBall == true)
		{
			if(other.gameObject.GetComponent<PlayerScript>().team == Team.Team1)
			{
				InstantiateBallParticles (other.transform.position);

				GameObject ball = other.gameObject.GetComponent<PlayerScript>().holdBall;

				other.gameObject.GetComponent<PlayerScript>().ReleaseVoid ();
				matchManager.PointToTeam1 (1);
				DestroyBall (ball);
				matchManager.InstantiateBall ();
			}

			if(other.gameObject.GetComponent<PlayerScript>().team == Team.Team2)
			{
				InstantiateBallParticles (other.transform.position);

				GameObject ball = other.gameObject.GetComponent<PlayerScript>().holdBall;

				other.gameObject.GetComponent<PlayerScript>().ReleaseVoid ();
				matchManager.PointToTeam2 (1);
				DestroyBall (ball);
				matchManager.InstantiateBall ();

			}

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
		vibration.VibrateBothMotors (0, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z);
		vibration.VibrateBothMotors (1, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z);
		vibration.VibrateBothMotors (2, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z);
		vibration.VibrateBothMotors (3, goalVibrationForce.x, goalVibrationForce.z, goalVibrationForce.y, goalVibrationForce.z);
	}

	void DestroyBall (GameObject ball)
	{
		Destroy (ball);
	}
}
