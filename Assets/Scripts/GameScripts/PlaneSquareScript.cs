using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSquareScript : MonoBehaviour
{

	public rocnikacClassy.Position position;
	public GameManager GM;
	public GameObject SquareToSelectChild;
	public bool highlighted = false;
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("aye i started");
		SquareToSelectChild.SetActive (false);
		SquareToSelectChild.GetComponent<SquareToSelect> ().position = position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}



	public void Highlight ()
	{
		highlighted = true;
		SquareToSelectChild.SetActive (true);
	}

	public void UnHighlight ()
	{
		highlighted = false;
		SquareToSelectChild.SetActive (false);
	}
}
