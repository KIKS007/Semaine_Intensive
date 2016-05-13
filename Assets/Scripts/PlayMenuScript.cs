using UnityEngine;
using System.Collections;
using Rewired;
using DG.Tweening;

public class PlayMenuScript : MonoBehaviour 
{

	public bool player1 = false;
	public bool player2 = false;
	public bool player3 = false;
	public bool player4 = false;

	public float yMin;
	public float yMax;
	public float tweenDuration;

	public Transform robot1;
	public Transform robot2;
	public Transform robot3;
	public Transform robot4;

	// Use this for initialization
	void Start () 
	{
		robot1.DOMoveY(yMin, 0);
		robot2.DOMoveY(yMin, 0);
		robot3.DOMoveY(yMin, 0);
		robot4.DOMoveY(yMin, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ReInput.controllers.GetJoystick(0) != null && !player1)
			Player1 (true);

		if(ReInput.controllers.GetJoystick(0) == null && player1)
			Player1 (false);
			

		if(ReInput.controllers.GetJoystick(1) != null && !player1)
			Player2 (true);

		if(ReInput.controllers.GetJoystick(1) == null && player1)
			Player2 (false);


		if(ReInput.controllers.GetJoystick(2) != null && !player1)
			Player3 (true);

		if(ReInput.controllers.GetJoystick(2) == null && player1)
			Player3 (false);


		if(ReInput.controllers.GetJoystick(3) != null && !player1)
			Player4 (true);

		if(ReInput.controllers.GetJoystick(3) == null && player1)
			Player4 (false);

	}

	void Player1 (bool ON)
	{
		if(ON)
		{
			robot1.DOMoveY(yMax, tweenDuration);
			player1 = true;
			Debug.Log("bite");
		}
		else
		{
			robot1.DOMoveY(yMin, tweenDuration);
			player1 = false;
		}
	}

	void Player2 (bool ON)
	{
		if(ON)
		{
			robot2.DOMoveY(yMax, tweenDuration);
			player2 = true;
		}
		else
		{
			robot2.DOMoveY(yMin, tweenDuration);
			player2 = false;
		}
	}

	void Player3 (bool ON)
	{
		if(ON)
		{
			robot3.DOMoveY(yMax, tweenDuration);
			player3 = true;
		}
		else
		{
			robot3.DOMoveY(yMin, tweenDuration);
			player3 = false;
		}
	}

	void Player4 (bool ON)
	{
		if(ON)
		{
			robot4.DOMoveY(yMax, tweenDuration);
			player4 = true;
		}
		else
		{
			robot4.DOMoveY(yMin, tweenDuration);
			player4 = false;
		}
	}
}
