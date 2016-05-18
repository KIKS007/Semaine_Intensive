using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour 
{
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	[Header ("Points")]
	public int pointsToWin;

	public int team1points;
	public int team2points;
	public Text team1Score;
	public Text team2Score;

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
	public Transform goalsParents;
	public GameObject[] changingGoals = new GameObject[12];
	public GameObject team1GoalPrefab;
	public GameObject team2GoalPrefab;
	public Material team1Color;
	public Material team2Color;

	private GameObject team1Goalenabled;
	private GameObject team2Goalenabled;

	public GameObject game;
	public GameObject gameOver;
	public GameObject menu;
	public GameObject textteam1;
	public GameObject textteam2;

	private bool gameEnded = false;

	// Use this for initialization
	void Start () 
	{
		switch(ReInput.controllers.GetControllerCount(ControllerType.Joystick))
		{
		case 1:
			player1.SetActive(true);
			break;
		case 2:
			player1.SetActive(true);
			player2.SetActive(true);
			break;
		case 3:
			player1.SetActive(true);
			player2.SetActive(true);
			player3.SetActive(true);
			break;
		case 4:
			player1.SetActive(true);
			player2.SetActive(true);
			player3.SetActive(true);
			player4.SetActive(true);
			break;
		}

		team1points = 0;
		team2points = 0;

		if (switchGoals)
			SetFirstGoals ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTexts ();

		if(team1points >= pointsToWin && !gameEnded)
		{
			GameEnded (1);
		}

		if(team2points >= pointsToWin && !gameEnded)
		{
			GameEnded (2);
		}
	}

	void UpdateTexts ()
	{
		team1Score.text = team1points.ToString ();

		team2Score.text = team2points.ToString ();
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

		GameObject goal1Temp = Instantiate (team1GoalPrefab, goal1.transform.position, goal1.transform.rotation) as GameObject;
		GameObject goal2Temp = Instantiate (team2GoalPrefab, goal2.transform.position, goal2.transform.rotation) as GameObject;

		goal1Temp.transform.SetParent (goalsParents);
		goal2Temp.transform.SetParent (goalsParents);

		team1Goalenabled = goal1Temp;
		team2Goalenabled = goal2Temp;
	}

	public void SwitchGoals (GameObject whichGoal)
	{
		Team team = whichGoal.GetComponent<GoalScript> ().team;
		GameObject newGoal;

		do
			newGoal = changingGoals [Random.Range (0, changingGoals.Length)];
		
		while (newGoal.transform.position == team1Goalenabled.transform.position || newGoal.transform.position == team2Goalenabled.transform.position);


		GameObject goalTemp;

		if (team == Team.Team1)
			team1Goalenabled = goalTemp = Instantiate (team1GoalPrefab, newGoal.transform.position, newGoal.transform.rotation) as GameObject;
		
		else
			team2Goalenabled = goalTemp = Instantiate (team2GoalPrefab, newGoal.transform.position, newGoal.transform.rotation) as GameObject;

		goalTemp.transform.SetParent (goalsParents);
	}

	void GameEnded (int whichTeam)
	{
		gameEnded = true;

		game.SetActive(false);
		gameOver.SetActive(true);

		if(whichTeam == 1)
			textteam1.SetActive(true);

		if(whichTeam == 2)
			textteam2.SetActive(true);


		menu.GetComponent<Button>().Select ();
	}

	public void Menu ()
	{
		SceneManager.LoadScene("Menu");
	}

	public void Quit ()
	{
		Application.Quit ();
	}
		
}
