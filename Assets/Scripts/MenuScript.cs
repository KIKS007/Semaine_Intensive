using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;

public class MenuScript : MonoBehaviour 
{
	public EventSystem eventSys;
	public GameObject gamepadsManager;

	public GameObject mainCanvas;
	public GameObject creditsCanvas;
	public GameObject playCanvas;
	public GameObject instructionsCanvas;
	public GameObject controlsCanvas;

	public GameObject playButton;
	public GameObject beginButton;

	private GameObject previousSelectedGameobject = null;


	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;

		mainCanvas.SetActive(true);
		creditsCanvas.SetActive(false);
		playCanvas.SetActive(false);
		controlsCanvas.SetActive(false);
		instructionsCanvas.SetActive(false);

		GlobalVariables.Instance.Character1 = -1;
		GlobalVariables.Instance.Character2 = -1;
		GlobalVariables.Instance.Character3 = -1;
		GlobalVariables.Instance.Character4 = -1;
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

			MasterAudio.PlaySound ("MENU_Cancel");
		}

		KeepButtonSelected ();
	}

	void KeepButtonSelected ()
	{
		if(eventSys.currentSelectedGameObject != null)
			previousSelectedGameobject = eventSys.currentSelectedGameObject;

		else if (eventSys.currentSelectedGameObject == null && previousSelectedGameobject != null)
			previousSelectedGameobject.GetComponent<Button> ().Select ();
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
