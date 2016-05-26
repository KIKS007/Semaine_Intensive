using UnityEngine;
using System.Collections;

public class TriggerTest : MonoBehaviour 
{
	void OnTriggerEnter (Collider other)
	{
		Debug.Log (other);
	}
}
