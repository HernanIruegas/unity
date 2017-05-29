using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionPanel : MonoBehaviour {

	public static InputField input1, input2;

	public Transform panelCharacterSelection, panelInput, panelGame;

	public Text indicText;

	bool continueCharactersBool = true;

	int activePlayer = 1;
	
	public void continuarNombres(){
		gameController.setPlayersNames(input1.text, input2.text);
		panelInput.gameObject.SetActive(false);
		indicText.text = input1.text + ",";
		panelCharacterSelection.gameObject.SetActive(true);
	}

	public void clickDuck(){
		if(activePlayer == 1) {
			activePlayer = 2;
			gameController.setplayerString("duck", 0);
			indicText.text = input2.text + ",";
		} else{
			activePlayer = 3;
			gameController.setplayerString("duck", 1);
			panelCharacterSelection.gameObject.SetActive(false);
			panelGame.gameObject.SetActive(true);
		    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		    gameController.setbothplayers();
		}
	}

	public void clickDeer(){
		if(activePlayer == 1) {
			activePlayer = 2;
			gameController.setplayerString("deer", 0);
			indicText.text = input2.text + ",";
		} else{
			activePlayer = 3;
			gameController.setplayerString("deer", 1);
			panelCharacterSelection.gameObject.SetActive(false);
			panelGame.gameObject.SetActive(true);
		    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		    gameController.setbothplayers();

		}
	}

	public void clickDavidNoel(){
		if(activePlayer == 1) {
			activePlayer = 2;
			gameController.setplayerString("DavidNoel", 0);
			indicText.text = input2.text + ",";
		} else{
			activePlayer = 3;
			gameController.setplayerString("DavidNoel", 1);
			panelCharacterSelection.gameObject.SetActive(false);
			panelGame.gameObject.SetActive(true);
		    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		    gameController.setbothplayers();

		}
	}

	public void clickHen(){
		if(activePlayer == 1) {
			activePlayer = 2;
			gameController.setplayerString("Hen", 0);
			indicText.text = input2.text + ",";
		} else{
			activePlayer = 3;
			gameController.setplayerString("Hen", 1);
			panelCharacterSelection.gameObject.SetActive(false);
			panelGame.gameObject.SetActive(true);
		    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		    gameController.setbothplayers();

		}
	}

	public void clickPavoReal(){
		if(activePlayer == 1) {
			activePlayer = 2;
			gameController.setplayerString("Pavo real", 0);
			indicText.text = input2.text + ",";
		} else{
			activePlayer = 3;
			gameController.setplayerString("Pavo real", 1);
			panelCharacterSelection.gameObject.SetActive(false);
			panelGame.gameObject.SetActive(true);
		    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		    gameController.setbothplayers();
		    
		}
	}

	void Start(){
		input1 = GameObject.Find("InputField1").GetComponent<InputField>();
		input2 = GameObject.Find("InputField2").GetComponent<InputField>();
	}

	void Update(){
		if(continueCharactersBool){
			if(Input.GetKeyUp(KeyCode.Return)){
				continuarNombres();
				continueCharactersBool = false;
			}
		}
	}
}