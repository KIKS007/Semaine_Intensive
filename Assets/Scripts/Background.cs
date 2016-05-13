using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public int sortingOrder;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Renderer>().sortingOrder = sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
