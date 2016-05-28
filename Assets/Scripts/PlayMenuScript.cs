using UnityEngine;
using System.Collections;
using Rewired;
using DG.Tweening;
using UnityEngine.UI;
using DarkTonic.MasterAudio;

public class PlayMenuScript : MonoBehaviour 
{
	public GameObject playButton;
	public float playXMax;
	public float playXMin;

	public Color[] playersColors = new Color[4];
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

		ReInput.ControllerDisconnectedEvent += EnableTexts;
		ReInput.ControllerConnectedEvent += EnableTexts;

		EnableTexts2 ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetInputs ();

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

	void EnableTexts (ControllerStatusChangedEventArgs arg)
	{
		if(ReInput.controllers.GetJoystick (0) == null && playersText[0].GetComponent<Text> ().color != disabledColor)
		{
			playersText [0].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [0] = 2;

			playersText[0].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[0].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[0].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}

		if(ReInput.controllers.GetJoystick (1) == null && playersText[1].GetComponent<Text> ().color != disabledColor)
		{
			playersText [1].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [1] = 2;

			playersText[1].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[1].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[1].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}

		if(ReInput.controllers.GetJoystick (2) == null && playersText[2].GetComponent<Text> ().color != disabledColor)
		{
			playersText [2].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [2] = 2;

			playersText[2].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[2].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[2].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}

		if(ReInput.controllers.GetJoystick (3) == null && playersText[3].GetComponent<Text> ().color != disabledColor)
		{
			playersText [3].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [3] = 2;

			playersText[3].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[3].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[3].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}
	}

	void EnableTexts2 ()
	{
		if(ReInput.controllers.GetJoystick (0) == null && playersText[0].GetComponent<Text> ().color != disabledColor)
		{
			playersText [0].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [0] = 2;

			playersText[0].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[0].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[0].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}

		if(ReInput.controllers.GetJoystick (1) == null && playersText[1].GetComponent<Text> ().color != disabledColor)
		{
			playersText [1].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [1] = 2;

			playersText[1].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[1].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[1].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}

		if(ReInput.controllers.GetJoystick (2) == null && playersText[2].GetComponent<Text> ().color != disabledColor)
		{
			playersText [2].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [2] = 2;

			playersText[2].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[2].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[2].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}

		if(ReInput.controllers.GetJoystick (3) == null && playersText[3].GetComponent<Text> ().color != disabledColor)
		{
			playersText [3].DOAnchorPosX(xPositions[2], movementDuration).SetEase(movementEase);
			textInt [3] = 2;

			playersText[3].GetComponent<Text> ().DOColor (disabledColor, movementDuration);
			playersText[3].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
			playersText[3].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (disabledColor, movementDuration);
		}
	}

	void SetColor ()
	{
		for(int i = 0; i < playersText.Length; i++)
		{
			if(ReInput.controllers.GetJoystick (i) != null)
			{
				switch(textInt[i])
				{
				case 0:
					playersText [i].GetComponent<Text> ().DOColor (playersColors [0], movementDuration);
					playersText[i].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (playersColors [0], movementDuration);
					playersText[i].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (playersColors [0], movementDuration);
					break;
				case 1:
					playersText[i].GetComponent<Text> ().color = playersColors[1];
					playersText[i].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (playersColors [1], movementDuration);
					playersText[i].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (playersColors [1], movementDuration);
					break;
				case 2:
					playersText[i].GetComponent<Text> ().color = enabledColor;
					playersText[i].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (enabledColor, movementDuration);
					playersText[i].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (enabledColor, movementDuration);
					break;
				case 3:
					playersText[i].GetComponent<Text> ().color = playersColors[2];
					playersText[i].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (playersColors [2], movementDuration);
					playersText[i].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (playersColors [2], movementDuration);
					break;
				case 4:
					playersText[i].GetComponent<Text> ().color = playersColors[3];
					playersText[i].transform.GetChild(0).GetComponent<SpriteRenderer> ().DOColor (playersColors [3], movementDuration);
					playersText[i].transform.GetChild(1).GetComponent<SpriteRenderer> ().DOColor (playersColors [3], movementDuration);
					break;
				}
			}
		}
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
			MasterAudio.PlaySound ("NAVIGATION");

			playersText [whichPlayer].DOAnchorPosX(xPositions[ textInt[whichPlayer] + 1 ], movementDuration).SetEase(movementEase);

			textInt [whichPlayer] += 1;

			UpdateGamepads ();

			SetColor ();

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
			MasterAudio.PlaySound ("NAVIGATION");

			playersText [whichPlayer].DOAnchorPosX(xPositions[ textInt[whichPlayer] - 1 ], movementDuration).SetEase(movementEase);

			textInt [whichPlayer] -= 1;

			UpdateGamepads ();

			SetColor ();

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
