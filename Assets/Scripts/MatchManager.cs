using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Colorful;
using DarkTonic.MasterAudio;

public class MatchManager : MonoBehaviour 
{
	public Transform[] playersTF = new Transform[4];

	[Header ("Switching Goals")]
	public bool switchGoals;
	public int goalsNumber;
	public Transform goalsParents;
	public GameObject[] allGoals = new GameObject[0];

	private GameObject[] goalsEnabled = new GameObject[0];

	[Header ("Players Name")]
	public float displayTime;
	public float fadeDuration;
	public float lerpText;
	public Text[] playersNameText = new Text[4];
	public Color[] charactersColor = new Color[4];
	public bool following = true;

	[Header ("Player Destruction")]
	public GameObject team1ApparitionParticles;
	public GameObject team2ApparitionParticles;
	public GameObject team1DestructionParticles;
	public GameObject team2DestructionParticles;
	public float timeBeforeCreation;

	private GameObject[] playerSpawnPoints = new GameObject[0];

	[Header ("Points")]
	public int pointsToWin;
	public int team1points;
	public int team2points;
	public Text team1Score;
	public Text team2Score;
	public int fontSize;
	public float punchDuration;
	public float resetDuration;
	public float timeBeforeReset;
	public Ease punchEase;
	public Ease resetEase;

	[Header ("Balls")]
	public GameObject ballPrefab;
	public float sphereRadius;
	public LayerMask sphereLayer;
	public GameObject ballCreationParticles;
	public Material[] ballMaterials = new Material[3];

	private GameObject[] ballSpawnPoints = new GameObject[0];

	public GameObject game;
	public GameObject gameOver;
	public GameObject menu;
	public GameObject textteam1;
	public GameObject textteam2;

	[Header ("Game Over")]
	public Animator[] charactersAnim = new Animator[4];

	private bool gameEnded = false;

	[Header("Lights")]
	public Material[] lights = new Material[0];
	public float lightingDuration = 0.5f;
	public float lightingGap = 0.8f;
	public float bloomValue;
	public float rgbSpliValue;
	public float glitchDuration;

	[Header ("Game Over Buttons")]
	public float timeBeforeButtons;
	public float movementDuration;
	public float maxY;
	public RectTransform menuButton;
	public RectTransform restartButton;
	public RectTransform quitButton;

	private int randomInt;

	// Use this for initialization
	void Awake () 
	{
		gameOver.SetActive (false);

		MasterAudio.PlaySound ("INTRO_MATCH");

		goalsEnabled = new GameObject[goalsNumber];

		team1points = 0;
		team2points = 0;		

		ballSpawnPoints = GameObject.FindGameObjectsWithTag("BallSpawn");
		playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");

		if (switchGoals)
			SetFirstGoals ();

		StartCoroutine (SetNamesToPlayers ());

	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTexts ();

		if(team1points >= pointsToWin && !gameEnded)
		{
			StartCoroutine(GameEnded (1));
		}

		if(team2points >= pointsToWin && !gameEnded)
		{
			StartCoroutine(GameEnded (2));
		}
	}

	void UpdateTexts ()
	{
		team1Score.text = team1points.ToString ();

		team2Score.text = team2points.ToString ();
	}

	IEnumerator SetNamesToPlayers ()
	{
		playersNameText[0].DOFade(0, 0);
		playersNameText[1].DOFade(0, 0);
		playersNameText[2].DOFade(0, 0);
		playersNameText[3].DOFade(0, 0);

		playersNameText[0].DOFade(1, fadeDuration);
		playersNameText[1].DOFade(1, fadeDuration);
		playersNameText[2].DOFade(1, fadeDuration);
		playersNameText[3].DOFade(1, fadeDuration);

		float timer = displayTime;

		if(!GlobalVariables.Instance.Gamepad1Connected)
			playersNameText[0].gameObject.SetActive(false);

		if(!GlobalVariables.Instance.Gamepad2Connected)
			playersNameText[1].gameObject.SetActive(false);

		if(!GlobalVariables.Instance.Gamepad3Connected)
			playersNameText[2].gameObject.SetActive(false);

		if(!GlobalVariables.Instance.Gamepad4Connected)
			playersNameText[3].gameObject.SetActive(false);


		if(GlobalVariables.Instance.Character1 != -1)
			playersNameText[GlobalVariables.Instance.Character1].color = charactersColor[0];

		if(GlobalVariables.Instance.Character2 != -1)
			playersNameText[GlobalVariables.Instance.Character2].color = charactersColor[1];

		if(GlobalVariables.Instance.Character3 != -1)
			playersNameText[GlobalVariables.Instance.Character3].color = charactersColor[2];

		if(GlobalVariables.Instance.Character4 != -1)
			playersNameText[GlobalVariables.Instance.Character4].color = charactersColor[3];

		StartCoroutine (TextFollow ());

		yield return new WaitForSeconds(displayTime);

		playersNameText[0].DOFade(0, fadeDuration);
		playersNameText[1].DOFade(0, fadeDuration);
		playersNameText[2].DOFade(0, fadeDuration);
		playersNameText[3].DOFade(0, fadeDuration);

		yield return new WaitForSeconds(fadeDuration);

		playersNameText[0].gameObject.SetActive(false);
		playersNameText[1].gameObject.SetActive(false);
		playersNameText[2].gameObject.SetActive(false);
		playersNameText[3].gameObject.SetActive(false);

		following = false;
	}

	IEnumerator TextFollow ()
	{
		while(following)
		{
			if(GlobalVariables.Instance.Character1 != -1)
				playersNameText[GlobalVariables.Instance.Character1].transform.position = Vector3.Lerp(playersNameText[GlobalVariables.Instance.Character1].transform.position, playersTF[0].position, lerpText);

			if(GlobalVariables.Instance.Character2 != -1)
				playersNameText[GlobalVariables.Instance.Character2].transform.position = Vector3.Lerp(playersNameText[GlobalVariables.Instance.Character2].transform.position, playersTF[1].position, lerpText);

			if(GlobalVariables.Instance.Character3 != -1)
				playersNameText[GlobalVariables.Instance.Character3].transform.position = Vector3.Lerp(playersNameText[GlobalVariables.Instance.Character3].transform.position, playersTF[2].position, lerpText);

			if(GlobalVariables.Instance.Character4 != -1)
				playersNameText[GlobalVariables.Instance.Character4].transform.position = Vector3.Lerp(playersNameText[GlobalVariables.Instance.Character4].transform.position, playersTF[3].position, lerpText);
		
			yield return null;
		}

		yield return null;
	}

	public void PointToTeam1 (int howManyPoints)
	{
		team1points += howManyPoints;
		StartCoroutine (GoalGlitch ());
		DOTween.Pause("Punch");
		StartCoroutine (PunchTextScale (team1Score.GetComponent<Text>()));
		StartCoroutine (GoalSounds ());
	}

	public void PointToTeam2 (int howManyPoints)
	{
		team2points += howManyPoints;
		StartCoroutine (GoalGlitch ());
		DOTween.Pause("Punch");
		StartCoroutine (PunchTextScale (team2Score.GetComponent<Text>()));
		StartCoroutine (GoalSounds ());
	}

	IEnumerator GoalSounds ()
	{
		MasterAudio.PlaySound ("GOAL");

		yield return new WaitForSeconds (0.5f);

		MasterAudio.PlaySound ("GOAL_Clap");
	}

	IEnumerator PunchTextScale (Text text)
	{
		yield return new WaitForSeconds (1);

		int fontSizeOriginal = text.fontSize;

		DOTween.To(()=> text.fontSize, x=> text.fontSize = x, fontSize, punchDuration).SetEase(punchEase).SetId("Punch");

		yield return new WaitForSeconds (punchDuration);
		yield return new WaitForSeconds (timeBeforeReset);

		DOTween.To(()=> text.fontSize, x=> text.fontSize = x, fontSizeOriginal, resetDuration).SetEase(resetEase).SetId("Punch");
	}


	public void InstantiateBall ()
	{
		Vector3 randomPos = new Vector3 ();

		do
			randomPos = ballSpawnPoints[Random.Range(0, ballSpawnPoints.Length)].transform.position;
		while(Physics.CheckSphere (randomPos, sphereRadius, sphereLayer));

		GameObject ballClone = Instantiate (ballPrefab, randomPos, ballPrefab.transform.rotation) as GameObject;
		ballClone.GetComponent<MeshRenderer>().material = ballMaterials [Random.Range(0, ballMaterials.Length)];
		ballClone.transform.SetParent (goalsParents);

		BallParticulesCreation (randomPos);
	}

	public void DestroyPlayerVoid (GameObject player, Team team)
	{
		StartCoroutine (DestroyPlayer (player, team));
	}

	IEnumerator DestroyPlayer (GameObject player, Team team)
	{
		if(team == Team.Team1)
			Instantiate (team1DestructionParticles, player.transform.position, team1DestructionParticles.transform.rotation);
		else
			Instantiate (team2DestructionParticles, player.transform.position, team2DestructionParticles.transform.rotation);

		player.SetActive (false);

		yield return new WaitForSeconds (timeBeforeCreation);

		Vector3 randomPos = new Vector3 ();

		do
			randomPos = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)].transform.position;
		while(Physics.CheckSphere (randomPos, sphereRadius, sphereLayer));

		if(team == Team.Team1)
			Instantiate (team1ApparitionParticles, new Vector3(randomPos.x, randomPos.y, randomPos.z), team1ApparitionParticles.transform.rotation);
		else
			Instantiate (team2ApparitionParticles, new Vector3(randomPos.x, randomPos.y, randomPos.z), team2ApparitionParticles.transform.rotation);

		//yield return new WaitForSeconds (0.5f);

		for(int i = 0; i < player.transform.childCount; i++)
		{
			if(player.transform.GetChild(i).tag == "Stun")
				Destroy(player.transform.GetChild(i).gameObject);
		}

		player.transform.position = randomPos;
		player.SetActive (true);

	}

	void BallParticulesCreation (Vector3 pos)
	{
		Instantiate (ballCreationParticles, pos, ballCreationParticles.transform.rotation);
	}

	void SetFirstGoals ()
	{
		for(int i = 0; i < allGoals.Length; i++)
		{
			allGoals [i].SetActive (false);
		}

		for(int i = 0; i < goalsNumber; i++)
		{
			do
			{
				randomInt = Random.Range (0, allGoals.Length);
			}
			while(allGoals [randomInt].activeSelf == true);

			allGoals [randomInt].SetActive(true);
			goalsEnabled [i] = allGoals [randomInt];
		}
	}

	public void SwitchGoals (GameObject whichGoal)
	{
		if(switchGoals)
		{
			int whichIndex = 0;

			for(int i = 0; i < goalsEnabled.Length; i++)
			{
				if (whichGoal == goalsEnabled [i])
					whichIndex = i;
			}
				
			do
			{
				randomInt = Random.Range (0, allGoals.Length);
			}
			while(allGoals [randomInt].activeSelf == true);

			whichGoal.SetActive (false);
			allGoals [randomInt].SetActive(true);
			goalsEnabled [whichIndex] = allGoals [randomInt];
		}
	}

	IEnumerator GoalLights ()
	{
		Color tempColor = GetComponent<Renderer> ().material.GetColor ("_Color2");
		Color originalColor = lights[0].GetColor("_Color2");
		float originalBloom = lights[0].GetFloat("_Bloom");

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

	IEnumerator GoalGlitch ()
	{
		GameObject cameraM = GameObject.FindGameObjectWithTag ("MainCamera");

		cameraM.GetComponent<Glitch> ().enabled = true;
		DOTween.To (() => cameraM.GetComponent<RGBSplit> ().Amount, x=> cameraM.GetComponent<RGBSplit> ().Amount = x, rgbSpliValue, 0.2f);

		yield return new WaitForSeconds (glitchDuration);

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Glitch> ().enabled = false;
		DOTween.To (() => cameraM.GetComponent<RGBSplit> ().Amount, x=> cameraM.GetComponent<RGBSplit> ().Amount = x, 0, 0.5f);

		yield return null;
	}


	IEnumerator GameEnded (int whichTeam)
	{
		gameEnded = true;

		MasterAudio.PlaySound ("WIN");

		textteam1.SetActive(false);
		textteam2.SetActive(false);

		for(int i = 0; i < goalsEnabled.Length; i++)
		{
			goalsEnabled [i].GetComponent<Renderer> ().enabled = false;
			goalsEnabled [i].transform.GetChild (0).gameObject.SetActive (false);
			goalsEnabled [i].transform.GetChild (1).gameObject.SetActive (false);
			goalsEnabled [i].transform.GetChild (2).gameObject.SetActive (false);
		}

		yield return new WaitForSeconds (0.5f);

		game.SetActive(false);
		gameOver.SetActive(true);

		StartCoroutine (SetGameOverButtons ());

		if(whichTeam == 1)
		{
			textteam1.SetActive(true);

			charactersAnim[0].SetTrigger("victoire");
			charactersAnim[1].SetTrigger("victoire2");
			charactersAnim [0].transform.DOLocalRotate (new Vector3 (0, 140, 0), 1);
			charactersAnim [1].transform.DOLocalRotate (new Vector3 (0, 140, 0), 1);

			charactersAnim[2].SetTrigger("defaite");
			charactersAnim[3].SetTrigger("defaite");

		}

		if(whichTeam == 2)
		{
			textteam2.SetActive(true);

			charactersAnim[2].SetTrigger("victoire");
			charactersAnim[3].SetTrigger("victoire2");
			charactersAnim [2].transform.DOLocalRotate (new Vector3 (0, 140, 0), 1);
			charactersAnim [3].transform.DOLocalRotate (new Vector3 (0, 140, 0), 1);

			charactersAnim[0].SetTrigger("defaite");
			charactersAnim[1].SetTrigger("defaite");
		}

		if(GlobalVariables.Instance.Character1 == -1)
			charactersAnim[0].gameObject.SetActive(false);

		if(GlobalVariables.Instance.Character2 == -1)
			charactersAnim[1].gameObject.SetActive(false);

		if(GlobalVariables.Instance.Character3 == -1)
			charactersAnim[2].gameObject.SetActive(false);

		if(GlobalVariables.Instance.Character4 == -1)
			charactersAnim[3].gameObject.SetActive(false);


		GlobalVariables.Instance.GameOver = true;
	}

	IEnumerator SetGameOverButtons ()
	{
		menuButton.GetComponent<Button> ().interactable = false;
		restartButton.GetComponent<Button> ().interactable = false;
		quitButton.GetComponent<Button> ().interactable = false;

		yield return new WaitForSeconds (timeBeforeButtons);

		menuButton.DOAnchorPosY (maxY, movementDuration).SetEase (Ease.InOutCubic).SetDelay(0.5f);
		restartButton.DOAnchorPosY (maxY, movementDuration).SetEase (Ease.InOutCubic);
		quitButton.DOAnchorPosY (maxY, movementDuration).SetEase (Ease.InOutCubic).SetDelay(0.5f);

		menuButton.GetComponent<Button> ().interactable = true;
		restartButton.GetComponent<Button> ().interactable = true;
		quitButton.GetComponent<Button> ().interactable = true;

		yield return new WaitForSeconds (movementDuration);

		restartButton.GetComponent<Button> ().Select ();
	}

	public void Menu ()
	{
		SceneManager.LoadScene("Menu");
	}

	public void Restart ()
	{
		SceneManager.LoadScene("Level 3");
	}

	public void Quit ()
	{
		Application.Quit ();
	}
		
}
