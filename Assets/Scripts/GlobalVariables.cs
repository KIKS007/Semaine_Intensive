using UnityEngine;
using System.Collections;

public class GlobalVariables : Singleton<GlobalVariables> 
{
	protected GlobalVariables () {} // guarantee this will be always a singleton only - can't use the constructor!

	public int Character1 = -1;
	public int Character2 = -1;
	public int Character3 = -1;
	public int Character4 = -1;

	public bool Gamepad1Connected = false;
	public bool Gamepad2Connected = false;
	public bool Gamepad3Connected = false;
	public bool Gamepad4Connected = false;

	public bool GameOver = true;

	public bool GamePaused = false;
}