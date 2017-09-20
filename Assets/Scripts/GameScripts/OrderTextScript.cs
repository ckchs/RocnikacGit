using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderTextScript : MonoBehaviour
{
	UnityEngine.UI.Text txt;
	public Slider AllyOrderSlider;

	void Start ()
	{
		AllyOrderSlider.onValueChanged.AddListener (
			i => {valueChangedOnSlider (i);}
		);
		txt = gameObject.GetComponent<Text> ();

	}

	public void valueChangedOnSlider (float value)
	{
		txt.text = ("Unit order: " + value);
	}
}
