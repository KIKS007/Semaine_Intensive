using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraScreenShake : MonoBehaviour 
{
	public float shakeDuration = 0.5f;
	public int shakeVibrato = 100;
	public float shakeRandomness = 45;

	public bool canShake;
	public float screenForce;

	private Vector3 initialPosition;

	private Vector3 shakeStrenth = new Vector3 (1, 1, 1);

	// Use this for initialization
	void Start () 
	{
		//initialPosition = transform.position;
		initialPosition = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		if(canShake)
		{
			canShake = false;
			CameraShaking(shakeDuration, screenForce);
		}
	}

	public void CameraShaking (float duration, float shakeForce)
	{
		canShake = false;
		//print("Shaking");
		transform.DOShakePosition (duration, shakeStrenth * shakeForce, shakeVibrato, shakeRandomness).OnComplete (ResetCameraPosition).SetId("ScreenShake");
	}

	void ResetCameraPosition ()
	{
		/*if(!DOTween.IsTweening("ScreenShake"))
		{
			transform.DOMove(initialPosition, 0.5f);
		}*/
	}

}