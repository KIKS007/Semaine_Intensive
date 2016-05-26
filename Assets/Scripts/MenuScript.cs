using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
	public GameObject gamepadsManager;

	public GameObject mainCanvas;
	public GameObject creditsCanvas;
	public GameObject playCanvas;
	public GameObject instructionsCanvas;
	public GameObject controlsCanvas;

	public GameObject playButton;
	public GameObject beginButton;

	// Use this for initialization
	void Start () 
	{
		mainCanvas.SetActive(true);
		creditsCanvas.SetActive(false);
		playCanvas.SetActive(false);
		controlsCanvas.SetActive(false);
		instructionsCanvas.SetActive(false);
	}

	void Update ()
	{
		if(Input.GetAxis("Cancel") > 0 && mainCanvas.activeSelf == false)
		{
			mainCanvas.SetActive(true);
			creditsCanvas.SetActive(false);
			playCanvas.SetActive(false);
			controlsCanvas.SetActive(false);
			instructionsCanvas.SetActive(false);

			playButton.GetComponent<Button>().Select ();
		}
	}

	public void Play () 
	{
		mainCanvas.SetActive(false);
		playCanvas.SetActive(true);

		beginButton.GetComponent<Button>().Select ();

	}

	public void Credits ()
	{
		mainCanvas.SetActive(false);
		creditsCanvas.SetActive(true);

	}

	public void Instructions ()
	{
		mainCanvas.SetActive(false);
		instructionsCanvas.SetActive(true);
	}

	public void Controls ()
	{
		mainCanvas.SetActive(false);
		controlsCanvas.SetActive(true);
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public void BeginGame ()
	{
		GlobalVariables.Instance.GameOver = false;
		SceneManager.LoadScene("Level 3");
	}
}
