using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CharacterSelectionPanel : MonoBehaviour {

	public static InputField input1, input2;

	public Transform panelCharacterSelection, panelInput, panelGame;

	public Text indicText;

	bool continueCharactersBool = true;

	int activePlayer = 1;
	
	Selectable next = null;
	Selectable current = null;

	private EventSystem eventSystem;
	
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
        this.eventSystem = EventSystem.current;
        input1.Select();
	}

	void Update(){
		if(continueCharactersBool){
			if(Input.GetKeyUp(KeyCode.Return)){
				continuarNombres();
				continueCharactersBool = false;
			}

         	// When TAB is pressed, we should select the next selectable UI element
         	if (Input.GetKeyDown(KeyCode.Tab)) {
				next = null;
				current = null;
 
	            // Figure out if we have a valid current selected gameobject
	            if (eventSystem.currentSelectedGameObject != null) {
					// Unity doesn't seem to "deselect" an object that is made inactive
					if (eventSystem.currentSelectedGameObject.activeInHierarchy) {
						current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
					}
				}

				if (current != null) {
					// When SHIFT is held along with tab, go backwards instead of forwards
					if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
						next = current.FindSelectableOnLeft();
						if (next == null) {
							next = current.FindSelectableOnUp();
						}
					} else {
						next = current.FindSelectableOnRight();
						if (next == null) {
							next = current.FindSelectableOnDown();
						}
					}
				} else {
					// If there is no current selected gameobject, select the first one
					if (Selectable.allSelectables.Count > 0) {
						next = Selectable.allSelectables[0];
					}
				}

				if (next != null){
					next.Select();
				}
			}
		}
	}
}	