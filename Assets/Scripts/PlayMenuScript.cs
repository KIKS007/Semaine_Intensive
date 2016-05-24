using UnityEngine;
using System.Collections;
using Rewired;
using DG.Tweening;
using UnityEngine.UI;

public class PlayMenuScript : MonoBehaviour 
{
	public GameObject playButton;
	public float playXMax;
	public float playXMin;

	public RectTransform[] playersText = new RectTransform[4];
	public float[] xPositions = new float[5];
	public int[] textInt = new int[4] {2, 2, 2, 2};
	public float gapBetweenInputs;
	public float movementDuration;
	public Ease movementEase;

	public Color enabledColor;
	public Color disabledColor;

	public Transform robot1;
	public Transform robot2;
	public Transform robot3;
	public Transform robot4;

	[HideInInspector]
	public Player player1; // The Rewired Player
	[HideInInspector]
	public Player player2; // The Rewired Player
	[HideInInspector]
	public Player player3; // The Rewired Player
	[HideInInspector]
	public Player player4; // The Rewired Player

	private bool[] playerCanMove = new bool[4] {true, true, true, true};

	// Use this for initialization
	void Start () 
	{
		player1 = ReInput.players.GetPlayer(0);
		player2 = ReInput.players.GetPlayer(1);
		player3 = ReInput.players.GetPlayer(2);
		player4 = ReInput.players.GetPlayer(3);

		playersText[0].anchoredPosition = new Vector2 (xPositions[2], playersText[0].anchoredPosition.y);
		playersText[1].anchoredPosition = new Vector2 (xPositions[2], playersText[1].anchoredPosition.y);
		playersText[2].anchoredPosition = new Vector2 (xPositions[2], playersText[2].anchoredPosition.y);
		playersText[3].anchoredPosition = new Vector2 (xPositions[2], playersText[3].anchoredPosition.y);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetInputs ();

		EnableTexts ();

		CanPlay ();
	}

	void GetInputs ()
	{
		if (player1.GetAxis ("Movement_Horizontal") > 0 && playerCanMove[0])
			StartCoroutine (Right (0));

		if (player1.GetAxis ("Movement_Horizontal") < 0 && playerCanMove[0])
			StartCoroutine (Left (0));

		if (player2.GetAxis ("Movement_Horizontal") > 0 && playerCanMove[1])
			StartCoroutine (Right (1));

		if (player2.GetAxis ("Movement_Horizontal") < 0 && playerCanMove[1])
			StartCoroutine (Left (1));

		if (player3.GetAxis ("Movement_Horizontal") > 0 && playerCanMove[2])
			StartCoroutine (Right (2));

		if (player3.GetAxis ("Movement_Horizontal") < 0 && playerCanMove[2])
			StartCoroutine (Left (2));

		if (player4.GetAxis ("Movement_Horizontal") > 0 && playerCanMove[3])
			StartCoroutine (Right (3));

		if (player4.GetAxis ("Movement_Horizontal") < 0 && playerCanMove[3])
			StartCoroutine (Left (3));
	}

	void EnableTexts ()
	{
		if (ReInput.controllers.GetJoystick (0) != null)
			playersText[0].GetComponent<Text> ().color = enabledColor;
		else
			playersText[0].GetComponent<Text> ().color = disabledColor;

		if (ReInput.controllers.GetJoystick (1) != null)
			playersText[1].GetComponent<Text> ().color = enabledColor;
		else
			playersText[1].GetComponent<Text> ().color = disabledColor;

		if (ReInput.controllers.GetJoystick (2) != null)
			playersText[2].GetComponent<Text> ().color = enabledColor;
		else
			playersText[2].GetComponent<Text> ().color = disabledColor;

		if (ReInput.controllers.GetJoystick (3) != null)
			playersText[3].GetComponent<Text> ().color = enabledColor;
		else
			playersText[3].GetComponent<Text> ().color = disabledColor;
	}

	void CanPlay ()
	{
		int player1 = 0;
		int player2 = 0;
		int player3 = 0;
		int player4 = 0;
		int none = 0;

		int team1 = 0;
		int team2 = 0;

		for(int i = 0; i < textInt.Length; i++)
		{
			switch(textInt[i])
			{
			case 0:
				player1++;
				team1++;
				break;
			case 1:
				player2++;
				team1++;
				break;
			case 3:
				player3++;
				team2++;
				break;
			case 4:
				team2++;
				player4++;
				break;
			case 2:
				none++;
				break;
			}
		}

		if(player1 > 1 || player2 > 1 || player3 > 1 || player4 > 1 || none >= 3)
		{
			if(playButton.GetComponent<Button>().interactable == true)
			{
				playButton.GetComponent<Button> ().interactable = false;
				playButton.GetComponent<RectTransform> ().DOAnchorPosY (playXMin, movementDuration).SetEase(movementEase);
			}
		}
		else if(team1 == 1 && team2 == 0 
			|| team1 == 0 && team2 == 1
			|| team1 == 2 && team2 == 0 
			|| team1 == 0 && team2 == 2)
		{
			if(playButton.GetComponent<Button>().interactable == true)
			{
				playButton.GetComponent<Button> ().interactable = false;
				playButton.GetComponent<RectTransform> ().DOAnchorPosY (playXMin, movementDuration).SetEase(movementEase);
			}
		}
		else if(playButton.GetComponent<Button>().interactable == false)
		{
			playButton.GetComponent<Button> ().interactable = true;
			playButton.GetComponent<RectTransform> ().DOAnchorPosY (playXMax, movementDuration).SetEase(movementEase);
			playButton.GetComponent<Button> ().Select ();
		}
	}

	IEnumerator Right (int whichPlayer)
	{
		playerCanMove [whichPlayer] = false;

		if(textInt[whichPlayer] != 4)
		{
			playersText [whichPlayer].DOAnchorPosX(xPositions[ textInt[whichPlayer] + 1 ], movementDuration).SetEase(movementEase);

			textInt [whichPlayer] += 1;

			UpdateGamepads ();

			yield return new WaitForSeconds(movementDuration);

			yield return new WaitForSeconds(gapBetweenInputs);
		}
			
		playerCanMove [whichPlayer] = true;

		yield return null;
	}

	IEnumerator Left (int whichPlayer)
	{
		playerCanMove [whichPlayer] = false;

		if(textInt[whichPlayer] != 0)
		{
			playersText [whichPlayer].DOAnchorPosX(xPositions[ textInt[whichPlayer] - 1 ], movementDuration).SetEase(movementEase);

			textInt [whichPlayer] -= 1;

			UpdateGamepads ();

			yield return new WaitForSeconds(movementDuration);

			yield return new WaitForSeconds(gapBetweenInputs);
		}
			
		playerCanMove [whichPlayer] = true;

		yield return null;
	}

	void UpdateGamepads ()
	{
		GlobalVariables.Instance.Character1 = -1;
		GlobalVariables.Instance.Character2 = -1;
		GlobalVariables.Instance.Character3 = -1;
		GlobalVariables.Instance.Character4 = -1;	

		for(int i = 0; i < textInt.Length; i++)
		{
			switch(textInt[i])
			{
			case 0:
				GlobalVariables.Instance.Character1 = i;
				break;
			case 1:
				GlobalVariables.Instance.Character2 = i;
				break;
			case 3:
				GlobalVariables.Instance.Character3 = i;
				break;
			case 4:
				GlobalVariables.Instance.Character4 = i;
				break;
			}
		}


	}

	void OnDestroy ()
	{
		if(GlobalVariables.Instance.Character1 == 0	|| GlobalVariables.Instance.Character2 == 0	|| GlobalVariables.Instance.Character3 == 0	|| GlobalVariables.Instance.Character4 == 0)
			GlobalVariables.Instance.Gamepad1Connected = true;

		if(GlobalVariables.Instance.Character1 == 1	|| GlobalVariables.Instance.Character2 == 1	|| GlobalVariables.Instance.Character3 == 1	|| GlobalVariables.Instance.Character4 == 1)
			GlobalVariables.Instance.Gamepad2Connected = true;

		if(GlobalVariables.Instance.Character1 == 2	|| GlobalVariables.Instance.Character2 == 2	|| GlobalVariables.Instance.Character3 == 2	|| GlobalVariables.Instance.Character4 == 2)
			GlobalVariables.Instance.Gamepad3Connected = true;

		if(GlobalVariables.Instance.Character1 == 3	|| GlobalVariables.Instance.Character2 == 3	|| GlobalVariables.Instance.Character3 == 3	|| GlobalVariables.Instance.Character4 == 3)
			GlobalVariables.Instance.Gamepad4Connected = true;
	}
}
