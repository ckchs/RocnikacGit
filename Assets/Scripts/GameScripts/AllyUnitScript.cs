using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rocnikacClassy;
using UnityEngine.UI;

public class AllyUnitScript : MonoBehaviour
{
	public Slider AllyOrderSlider;
	public GameManager GM;
	public Character ThisCharacter;
	public GameObject AlliedUnitCanvas;
	// Use this for initialization
	void Start () {
		AllyOrderSlider.onValueChanged.AddListener (
			i => {valueChangedOnSlider (i);}
		);
	}

	void OnMouseDown()
	{
		GM.ClickedOnAlliedCharacter (gameObject,this);
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void CancelSelectingMovementOrAttack()
	{
		GM.CancelSelectingMovementOrAttack ();
	}

	void valueChangedOnSlider(float value)
	{
		GM.orderChange ((int) value,this);
	}

}
