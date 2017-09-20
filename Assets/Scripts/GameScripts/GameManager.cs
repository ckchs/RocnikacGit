using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using rocnikacClassy;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	static int numberOfCharacters = 3;
	GameObject[,] plane;
	GameObject[] alliedUnits = new GameObject[numberOfCharacters];
	AllyUnitScript[] alliedUnitsScripts = new AllyUnitScript[numberOfCharacters];
	GameObject[] enemyUnits = new GameObject[numberOfCharacters];
	GamePlan abstractGamePlan;
	public GameObject PlaneSquarePrefab;
	public GameObject AliedUnitPrefab;
	public GameObject EnemyUnitPrefab;
	public UnityEngine.UI.Text GameInformText;
	gameStates actualGameState;
	int size;
	AllyUnitScript selectedCharacter;
	GameObject selectedAllyObject;
	List<PlaneSquareScript> highlightedPlanes;
	TurnPlaner turnPlaner;
	// Use this for initialization
	void Start ()
	{
		size = 10;
		abstractGamePlan = new GamePlan (size, numberOfCharacters);
		actualGameState = gameStates.makingTurnWaiting;
		highlightedPlanes = new List<PlaneSquareScript> ();
		plane = new GameObject[size, size];
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				plane [i, j] = UnityEngine.Object.Instantiate (PlaneSquarePrefab, new Vector3 (10 * i, 0, 10 * j), Quaternion.identity) as GameObject;

				var script = plane [i, j].GetComponent<PlaneSquareScript> ();
				script.position.x = i;
				script.position.y = j;
				script.GM = this;
				script.SquareToSelectChild.GetComponent<SquareToSelect> ().GM = this;

			}
		}
		createCharacterAtPos (0, 1, new Fighter (0, 1, true));
		createCharacterAtPos (0, 5, new Fighter (0, 5, true));
		createCharacterAtPos (0, 8, new Fighter (0, 8, true));
		createCharacterAtPos (9, 1, new Fighter (9, 1, false));
		createCharacterAtPos (9, 8, new Fighter (9, 8, false));
		createCharacterAtPos (9, 5, new Fighter (9, 5, false));


		turnPlaner = new TurnPlaner (abstractGamePlan.alliedCharacters);

	}



	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (actualGameState);
	}

	void createCharacterAtPos (int x, int y, Character chr)
	{
		Position pos;
		pos.x = x;
		pos.y = y;
		if (chr.allied) {
			var index = abstractGamePlan.AddAlliedCharacterAtPosition (chr, pos);
			alliedUnits [index - 1] = UnityEngine.Object.Instantiate (AliedUnitPrefab, new Vector3 (10 * x, 5, 10 * y), Quaternion.identity, gameObject.transform) as GameObject;
			var component = alliedUnits [index - 1].GetComponent<AllyUnitScript> ();
			component.ThisCharacter = chr;
			component.GM = this;
			component.AllyOrderSlider.maxValue = numberOfCharacters;
			component.AllyOrderSlider.value = (float)(index);
			alliedUnitsScripts [index - 1] = component;

		} else {
			var index = abstractGamePlan.AddEnemyCharacterAtPosition (chr, pos);
			enemyUnits [index - 1] = UnityEngine.Object.Instantiate (EnemyUnitPrefab, new Vector3 (10 * x, 5, 10 * y), Quaternion.identity, gameObject.transform) as GameObject;
		}


	}

	public void ClickedOnAlliedCharacter (GameObject go, AllyUnitScript aus)
	{
		if (actualGameState != gameStates.makingTurnWaiting) {
			return;
		}
		var chr = aus.ThisCharacter;
		actualGameState = gameStates.makingTurnSelectingMovement;
		selectedCharacter = aus;
		selectedAllyObject = go;
		aus.AlliedUnitCanvas.SetActive (true);
		go.layer = 2;//make unclickable
		Color c = selectedAllyObject.GetComponent<Renderer>().material.color;
		c.a = 0.5f;
		selectedAllyObject.GetComponent<Renderer>().material.color = c;
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				if (Math.Abs (i - chr.actualPosition.x) + Math.Abs (j - chr.actualPosition.y) <= chr.movementRange) {
					highlightedPlanes.Add (plane [i, j].GetComponent<PlaneSquareScript> ());
					highlightedPlanes [highlightedPlanes.Count - 1].Highlight ();
				}
			}
		}
		var actuallySelectedIndex = Array.IndexOf (alliedUnits, go);
		var actualOrderOfSelected = turnPlaner.getOrder (aus.ThisCharacter);
		GameInformText.text = String.Format ("Selected Char: {0} Selected order: {1}", actuallySelectedIndex.ToString (), actualOrderOfSelected.ToString ());
		//Debug.Log (String.Format ("Selected Char: {0} Selected order: {1}", actuallySelectedIndex.ToString (), actualOrderOfSelected.ToString ()));

	}

	public void ClickedOnPlaneSquare (int x, int y)
	{
		Position pos;
		pos.x = x;
		pos.y = y;
		if (actualGameState == gameStates.makingTurnSelectingMovement) {
			turnPlaner.setMovement (selectedCharacter.ThisCharacter, pos);
			goToGameStateSelectingAttack (pos);
		} else {
			if (actualGameState == gameStates.makingTurnSelectingAttack) {
				turnPlaner.setAttack (selectedCharacter.ThisCharacter, pos);
				CancelSelectingMovementOrAttack ();
			}
		}
	}

	public void orderChange (int value, AllyUnitScript caller)
	{
		if (caller!=selectedCharacter) {
			return;
		}
		turnPlaner.setOrder (selectedCharacter.ThisCharacter, value - 1);
		for (int i = 0; i < alliedUnits.Length; i++) {
			if (selectedCharacter!=alliedUnitsScripts[i]) {
				alliedUnitsScripts[i].AllyOrderSlider.value=1+(float) turnPlaner.getOrder (alliedUnitsScripts[i].ThisCharacter);
			}


		}

	}

	void goToGameStateSelectingAttack (Position pos)
	{
		actualGameState = gameStates.makingTurnSelectingAttack;
		clearHighlightedSquares ();

		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				if ((Math.Abs (i - pos.x) + Math.Abs (j - pos.y) <= selectedCharacter.ThisCharacter.attackRange) &&
				    (Math.Abs (i - pos.x) + Math.Abs (j - pos.y) > 0)) {
					
					highlightedPlanes.Add (plane [i, j].GetComponent<PlaneSquareScript> ());
					highlightedPlanes [highlightedPlanes.Count - 1].Highlight ();
				}
			}
		}
	}

	void clearHighlightedSquares ()
	{
		foreach (var item in highlightedPlanes) {
			item.UnHighlight ();
		}
		highlightedPlanes.Clear ();
	}

	public void CancelSelectingMovementOrAttack ()
	{
		actualGameState = gameStates.makingTurnWaiting;
		selectedAllyObject.layer = 0;
		Color c = selectedAllyObject.GetComponent<Renderer>().material.color;
		c.a = 1f;
		selectedAllyObject.GetComponent<Renderer>().material.color = c;
		selectedCharacter.AlliedUnitCanvas.SetActive (false);
		clearHighlightedSquares ();
	}
}
