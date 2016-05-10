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
	public GameObject ballPrefab;
	public float sphereRadius;
	public float minXRandomPos;
	public float maxXRandomPos;
	public float minYRandomPos;
	public float maxYRandomPos;

	// Use this for initialization
	void Start () 
	{
		team1points = 0;
		team2points = 0;

		team1ScoreText = team1Score.text;
		team2ScoreText = team2Score.text;
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

		Instantiate (ballPrefab, randomPos, ballPrefab.transform.rotation);
	}

	void GameEnded (int whichTeam)
	{
		
	}
}
