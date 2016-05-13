using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour 
{

	public GameObject mainCanvas;
	public GameObject creditsCanvas;
	public GameObject playCanvas;
	public GameObject howToCanvas;

	public GameObject playButton;
	public GameObject beginButton;

	// Use this for initialization
	void Start () 
	{
		mainCanvas.SetActive(true);
		creditsCanvas.SetActive(false);
		playCanvas.SetActive(false);
		howToCanvas.SetActive(false);
	}

	void Update ()
	{
		if(Input.GetAxis("Cancel") > 0 && mainCanvas.activeSelf == false)
		{
			mainCanvas.SetActive(true);
			creditsCanvas.SetActive(false);
			playCanvas.SetActive(false);
			howToCanvas.SetActive(false);

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

	public void HowToPlay ()
	{
		mainCanvas.SetActive(false);
		howToCanvas.SetActive(true);
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public void BeginGame ()
	{
		SceneManager.LoadScene("Level 2");
	}
}
