using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareToSelect : MonoBehaviour
{
	public rocnikacClassy.Position position;
	public GameManager GM;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnMouseDown ()
	{
		GM.ClickedOnPlaneSquare (position.x, position.y);
	}
}
