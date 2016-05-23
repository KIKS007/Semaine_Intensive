using UnityEngine;
using System.Collections;

public class GlobalVariables : Singleton<GlobalVariables> 
{
	protected GlobalVariables () {} // guarantee this will be always a singleton only - can't use the constructor!

	public int Player1 = -1;
	public int Player2 = -1;
	public int Player3 = -1;
	public int Player4 = -1;

	public bool GameOver = true;
}