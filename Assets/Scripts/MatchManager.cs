using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour 
{
	[Header ("Points")]
	public int pointsToWin;

	public int team1points;
	public int team2points;
	public Text team1Score;
	public Text team2Score;

	private string team1ScoreText;
	private string team2ScoreText;

	[Header ("Balls")]
	public bool switchGoals;
	public GameObject ballPrefab;
	public float sphereRadius;
	public float minXRandomPos;
	public float maxXRandomPos;
	public float minYRandomPos;
	public float maxYRandomPos;
	public GameObject ballCreationParticles;
	public Material[] ballMaterials = new Material[3];

	[Header ("Switching Goals")]
	public GameObject[] changingGoals = new GameObject[12];
	public Material team1Color;
	public Material team2Color;

	// Use this for initialization
	void Start () 
	{
		team1points = 0;
		team2points = 0;

		team1ScoreText = team1Score.text;
		team2ScoreText = team2Score.text;

		if (switchGoals)
			SetFirstGoals ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTexts ();

		if(team1points >= pointsToWin)
		{
			GameEnded (1);
		}

		if(team2points >= pointsToWin)
		{
			GameEnded (2);
		}
	}

	void UpdateTexts ()
	{
		team1Score.text = team1ScoreText + team1points;

		team2Score.text = team2ScoreText + team2points;
	}

	public void PointToTeam1 (int howManyPoints)
	{
		team1points += howManyPoints;
	}

	public void PointToTeam2 (int howManyPoints)
	{
		team2points += howManyPoints;
	}

	public void InstantiateBall ()
	{
		Vector3 randomPos = new Vector3 ();

		do
			randomPos = new Vector3 (Random.Range (minXRandomPos, maxXRandomPos), Random.Range (minYRandomPos, maxYRandomPos), 0);
		while(Physics.CheckSphere (randomPos, sphereRadius, 0, QueryTriggerInteraction.Collide));

		GameObject ballClone = Instantiate (ballPrefab, randomPos, ballPrefab.transform.rotation) as GameObject;
		ballClone.GetComponent<MeshRenderer>().material = ballMaterials [Random.Range(0, ballMaterials.Length)];

		StartCoroutine (BallParticulesCreation (randomPos));
	}

	IEnumerator BallParticulesCreation (Vector3 pos)
	{
		GameObject particulesClone = Instantiate (ballCreationParticles, pos, ballCreationParticles.transform.rotation) as GameObject;

		yield return new WaitForSeconds (particulesClone.GetComponent<ParticleSystem>().duration);

		Destroy (particulesClone);
	}

	void SetFirstGoals ()
	{
		for(int i = 0; i < changingGoals.Length; i++)
		{
			changingGoals [i].SetActive (false);
		}

		GameObject goal1 = changingGoals [Random.Range (0, changingGoals.Length)];
		GameObject goal2;

		do
			goal2 = changingGoals [Random.Range (0, changingGoals.Length)];

		while (goal2 == goal1);

		goal1.GetComponent<GoalScript> ().team = Team.Team1;
		goal1.GetComponent<MeshRenderer> ().material = team1Color;

		goal2.GetComponent<GoalScript> ().team = Team.Team2;
		goal2.GetComponent<MeshRenderer> ().material = team2Color;

		goal1.SetActive (true);
		goal2.SetActive (true);
	}

	public void SwitchGoals (GameObject whichGoal)
	{
		Team team = whichGoal.GetComponent<GoalScript> ().team;
		GameObject newGoal;

		do
			newGoal = changingGoals [Random.Range (0, changingGoals.Length)];
		
		while (newGoal.activeSelf == true);

		whichGoal.SetActive (false);

		newGoal.GetComponent<GoalScript> ().team = team;

		if (team == Team.Team1)
			newGoal.GetComponent<MeshRenderer> ().material = team1Color;
		else
			newGoal.GetComponent<MeshRenderer> ().material = team2Color;

		newGoal.SetActive (true);
	}

	void GameEnded (int whichTeam)
	{
		
	}
}
