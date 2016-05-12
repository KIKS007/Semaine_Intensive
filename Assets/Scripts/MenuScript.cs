using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour 
{


	public void QuitGame ()
	{
		Application.Quit();

	}

	public void PlayGame ()
	{
		SceneManager.LoadScene("Main");
	}
		
}
