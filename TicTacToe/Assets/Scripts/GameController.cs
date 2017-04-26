using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player{
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor{
	public Color panelColor, textColor;
}

public class GameController : MonoBehaviour {
	public Text[] buttonList;
	private string playerSide, computerSide;
	private int moveCount, value;
	public GameObject gameOverPanel, restartButton, startInfo;
	public Text gameOverText;
	public Player playerX, playerO;
	public PlayerColor activePlayerColor, inactivePlayerColor;
	public bool playerMove;
	public float delay;
	public int[] iArr;
	public string[] Agathe;
	public int a, b, c;
	public bool[] gamePosibilities;


	// Use this for initialization
	void Awake() {
		gameOverPanel.SetActive(false);
		SetGameControllerReferenceOnButtons();
		moveCount = 0;
		restartButton.SetActive(false);
		playerMove = true;
	}

	void SetGameControllerReferenceOnButtons(){
		for(int i = 0; i<buttonList.Length; i++){
			buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
		}
	}

	public void SetStartingSide(string startingSide){
		playerSide = startingSide;
		if(playerSide == "X"){
			SetPlayerColors(playerX,playerO);
			computerSide = "O";
		}else{
			SetPlayerColors(playerO,playerX);
			computerSide = "X";
			playerMove = false;
		}
		StartGame();
	}

	void StartGame(){
		SetBoardInteractable(true);
		SetPlayerButtons(false);
		startInfo.SetActive(false);
		iArr = new int[9];
		for(int i = 0; i < 9; i++){
			iArr[i] = 0;
		}
		Agathe = new string[8];
		Agathe[0] = "840"; Agathe[1] = "246"; Agathe[2] = "678"; Agathe[3] = "036"; Agathe[4] = "147"; Agathe[5] = "258";
    	Agathe[6] = "012"; Agathe[7] = "345";
		gamePosibilities = new bool[5];
		for(int i = 0; i < 5; i++){
			gamePosibilities[i] = false;
		}
	}

	public string GetPlayerSide(){
		return playerSide;
	}

	public string GetComputerSide(){
		return computerSide;
	}

	public void EndTurn(){
		moveCount++;
		if((buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) || (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) || (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) ||
	   (buttonList[0].text  == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) || (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) || (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) ||
	   (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) || (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)){
			GameOver(playerSide);
		}else if((buttonList[0].text == computerSide && buttonList[1].text == computerSide && buttonList[2].text == computerSide) || (buttonList[3].text == computerSide && buttonList[4].text == computerSide && buttonList[5].text == computerSide) || (buttonList[6].text == computerSide && buttonList[7].text == computerSide && buttonList[8].text == computerSide) ||
	   (buttonList[0].text  == computerSide && buttonList[3].text == computerSide && buttonList[6].text == computerSide) || (buttonList[1].text == computerSide && buttonList[4].text == computerSide && buttonList[7].text == computerSide) || (buttonList[2].text == computerSide && buttonList[5].text == computerSide && buttonList[8].text == computerSide) ||
	   (buttonList[0].text == computerSide && buttonList[4].text == computerSide && buttonList[8].text == computerSide) || (buttonList[2].text == computerSide && buttonList[4].text == computerSide && buttonList[6].text == computerSide)){
			GameOver(computerSide);
		}else{
			ChangeSides();
			delay = 10;
		}

		if(moveCount>=9){
			GameOver("draw");
		}
	}

	void SetPlayerColors(Player newPlayer, Player oldPlayer){
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

	void GameOver(string winningPlayer){
		SetBoardInteractable(false);
		if(winningPlayer == "draw"){
			SetGameOvertext("It's a draw...");
			SetPlayerColorsInactive();
		}else{
			SetGameOvertext(winningPlayer + " Wins!");
		}	
		restartButton.SetActive(true);
	}

	void ChangeSides(){
		playerMove = (playerMove == true) ? false : true;

		if(playerMove == true){
			SetPlayerColors(playerX, playerO);
		}else{
			SetPlayerColors(playerO, playerX);
		}
	}

	void SetGameOvertext(string value){
		gameOverPanel.SetActive(true);
		gameOverText.text = value;
	}

	public void RestartGame(){
		moveCount = 0;
		gameOverPanel.SetActive(false);
		for(int i = 0; i<buttonList.Length; i++){
			buttonList[i].text = "";
		}
		restartButton.SetActive(false);
		SetPlayerButtons(true);
		SetPlayerColorsInactive();
		startInfo.SetActive(true);
		delay = 2;
		playerMove = true;
	}

	void SetBoardInteractable(bool toggle){
		for(int i = 0; i<buttonList.Length; i++){
			buttonList[i].GetComponentInParent<Button>().interactable = toggle;
		}
	}

	void SetPlayerButtons(bool toggle){
		playerX.button.interactable = toggle;
		playerO.button.interactable = toggle;
	}

	void SetPlayerColorsInactive(){
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;
	}

	public int LogicaMamalona(){
    //si puede ganar, ya gana...si tiene 2, pone el tercero
    for(int i = 0; i < 8; i++){
        a = (Agathe[i][0])-48;
        b = (Agathe[i][1])-48;
        c = (Agathe[i][2])-48;
        if(iArr[a] + iArr[b] + iArr[c] == 2){
            if(iArr[a] == 0){
            	iArr[a] = 1;
            	return a;
            }else if(iArr[b] == 0){
            	iArr[b] = 1;
            	return b;
            }else{
            	iArr[c] = 1;
            	return c;
            }            
        }
    }
    //Si el otro puede ganar, lo bloquea
    for(int i = 0; i < 8; i++){
    	a = (Agathe[i][0])-48;
        b = (Agathe[i][1])-48;
        c = (Agathe[i][2])-48;
        if(iArr[a] + iArr[b] + iArr[c] == 8){
            if(iArr[a] == 0){
            	iArr[a] = 1;
            	return a;
            }else if(iArr[b] == 0){
            	iArr[b] = 1;
            	return b;
            }else{
            	iArr[c] = 1;
            	return c;
            }  
        }
    }
    //Si no puede ganar, ni bloquear... entonces pone la segunda
    for(int i = 0; i < 8; i++){
    	a = (Agathe[i][0])-48;
        b = (Agathe[i][1])-48;
        c = (Agathe[i][2])-48;
        if(iArr[a] + iArr[b] + iArr[c] == 1){
            if(iArr[a] == 0){
            	iArr[a] = 1;
            	return a;
            }else if(iArr[b] == 0){
            	iArr[b] = 1;
            	return b;
            }else{
            	iArr[c] = 1;
            	return c;
            }  
        }
    }
    //pone la primer casilla disponible dentro del array 
    for(int i = 0; i < 9; i++){
        if(iArr[i] == 0){
            iArr[i] = 1;
            return i;
        }
    }
    return 0;
}

	int gignac(){
		 if(moveCount == 1 && iArr[4] == 0){
            iArr[4] = 1;
            return 4;
         }else if(moveCount == 3 && ((iArr[0] == 4 && iArr[7] == 4) || (iArr[8] == 4 && iArr[3] == 4))){
         	iArr[6] = 1;
         	return 6;
         }else if(moveCount == 3 && (iArr[2] == 4 || iArr[1] == 4) && iArr[3] == 4){
         	iArr[0] = 1;
         	return 0;
         }else if(moveCount == 3 && iArr[6] == 4 && iArr[1] == 4){
         	iArr[8] = 1;
         	return 8;
         }else if(moveCount == 3 && iArr[4] == 1 && ((iArr[0] == 4 && iArr[8] == 4) || (iArr[2] == 4 && iArr[6] == 4))){
            iArr[5] = 1;
            return 5;
		 }else{
         	return LogicaMamalona();
         }    
	}

	public int CompuEmpieza(){	
        if(moveCount == 0){
            iArr[2] = 1; //Pone en la esquina superior derecha. TURNO 1
            return 2;
        }if(moveCount==2){
            if(iArr[8] == 4 || iArr[0] == 4) gamePosibilities[0] = true; //Esquinas adyacentes
            else if(iArr[1] == 4 || iArr[5] == 4) gamePosibilities[1] = true; //Al lado
            else if(iArr[3] == 4 || iArr[7] == 4) gamePosibilities[2] = true; //Lado caballo
            else if(iArr[6] == 4) gamePosibilities[3] = true; //Contra esquina
            else gamePosibilities[4] = true;
        }if(gamePosibilities[0]){ //En esquinas adyacentes  
            if(moveCount == 2){
                iArr[6] = 1; //Ya valio madre el jugador.
                return 6;
            }else if(moveCount == 4){   
                if (iArr[4] == 4){ //En centro? TURNO 5
                    if(iArr[8] == 0){
                    	iArr[8] = 1;
                    	return 8;
                    }
                    else{
                    	iArr[0] = 1;
                    	return 0;
                    }      
                }else return LogicaMamalona();
            }          
        }else if(gamePosibilities[1]){ //Al lado
            if(moveCount == 2){
                if(iArr[1] == 4){
                    iArr[8] = 1;
                    return 8;
                }
                else{
                    iArr[0] = 1;
                    return 0;
                }
            }else if(moveCount == 4){
                if(iArr[1] == 4 && iArr[5] == 4){
                    iArr[4] = 1;
                    return 4;
                }
                else{
                    return LogicaMamalona();
                }       
            }
        }else if(gamePosibilities[2]){ //Lado caballo
            if(moveCount == 2){
                    iArr[8] = 1;
                    return 8;
            }else if(moveCount == 4){
                if(iArr[5] == 4){
                    iArr[4] = 1;
                    return 4;
                }
                else{
                	return LogicaMamalona();
                }
            }
        }else if(gamePosibilities[3]){ //Contra esquina
            if(moveCount == 2){
                    iArr[0] = 1;
                    return 0;
            }else if (moveCount == 4)
            {
            	if(iArr[1] == 4){
					iArr[8] = 1;
	                return 8;  
            	}
            	else
            		return LogicaMamalona();      
            }  
            else
                return LogicaMamalona();
        }else{//En el centro
            if(moveCount == 2){
                    iArr[6] = 1;
                    return 6;
            }else if(moveCount == 4){
                if(iArr[8]==4){
                    iArr[0] = 1;
                    return 0;
                }
                else if(iArr[0] == 4){
                    iArr[8] = 1;
                    return 8;
                }
                else
                	return LogicaMamalona();        
            }else
                return LogicaMamalona();        
        }if(moveCount > 5) return LogicaMamalona(); 

        return 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerMove == false){
			delay += delay * Time.deltaTime;
			if(delay >= 20){
				if(moveCount == 0 || moveCount == 2 || moveCount == 4){
					value = CompuEmpieza();
				}else{
					value = gignac();
				}
				if(buttonList[value].GetComponentInParent<Button>().interactable == true){
					buttonList[value].text = GetComputerSide();
					buttonList[value].GetComponentInParent<Button>().interactable = false;
					EndTurn();
				}
			}
		}	
	}
}