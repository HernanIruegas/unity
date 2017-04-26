﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

	public Button button;
	public Text buttonText;

	private GameController gameController;

	public void SetSpace(){
		if(gameController.playerMove == true){
			buttonText.text = gameController.GetPlayerSide();
			button.interactable = false;
			gameController.iArr[int.Parse(gameObject.tag)] = 4;

			gameController.EndTurn();
			//gameController.iArr [int.Parse (gameObject.tag)] = 4;
		}
	}

	public void SetGameControllerReference(GameController controller){
		gameController = controller;
	}
}
