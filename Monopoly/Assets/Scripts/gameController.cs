using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	public TextAsset textFileCasillas, textFileBonus;

	static string player1Name = "", player2Name = "";

	public static int turn = 0;

	static bool moviendoDetencion = false;

	static GameObject goPlayer1, goPlayer2;
	static GameObject[] goArray = new GameObject[2];

	static string player1String;
	static string player2String;

	public Image Dado;

	//2 = está disponible, 0 = player one owns it, 1 = player two owns it
	int[] casillasCompradas = {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2};

	int[] PrecioRentaCasillas = {50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50};

	string[] nombresDeCasillas;
	string[] textosDeBonus;
	
	public Transform panelComprar, panelSinDinero, panelPagaRenta, panelCasillaPropia, panelBonus, buttonTirarDado;

	public Text textCasillaCompra, textCasillaSinDinero, textCasillaPaga, textCasillaPropia, textPanelBonus, 
				textMoneyPlayerOne, textMoneyPlayerTwo, textPropertiesPlayerOne, textPropertiesPlayerTwo, textPrecioRenta;
	
	public Text textNombreJugador1, textNombreJugador2;

	public static bool jugadorEnMovimiento = false, libreDePago = false;
	public static double HastaAqui;
	public static int posBoard, DesdeAqui, bonusController;

	public struct playerSt{
		public int money, cell;
		public bool detencion;
		public string[] properties;
	};

	static playerSt[] plArray = new playerSt[2];

	public static void setPlayersNames(string s1, string s2){
		player1Name = s1;
		player2Name = s2;
	}

	public static void setplayerString(string s, int p){
		if(p == 0)
			player2String = s;
		else
			player1String = s;
	}

	public static void setbothplayers(){
		goPlayer1 = Resources.Load(player1String) as GameObject;
		goPlayer2 = Resources.Load(player2String) as GameObject;
		goArray[0] = Instantiate(goPlayer1) as GameObject;
		goArray[1] = Instantiate(goPlayer2) as GameObject;
		goArray[0].transform.position = new Vector3(96,44,0);
		goArray[1].transform.position = new Vector3(96,44,0);
		goArray[0].transform.localScale += new Vector3(4F, 4F, 0);
		goArray[1].transform.localScale += new Vector3(4F, 4F, 0);
	}

	void Start(){
		for(int i = 0; i < 2; i++){
			plArray[i].properties = new string[28];
			plArray[i].money = 200;
			plArray[i].cell = 0;
			plArray[i].detencion = false;
			for(int j = 0; j < 28; j++){
				plArray[i].properties[j] = " ";
			}
		}

		textMoneyPlayerOne.text = "Dinero: " + plArray[0].money;
		textMoneyPlayerTwo.text = "Dinero: " + plArray[1].money;

		textPropertiesPlayerOne.text = "Sin Propiedades\n";
		textPropertiesPlayerTwo.text = "Sin Propiedades\n";

		textosDeBonus = (textFileBonus.text.Split('\n'));
		nombresDeCasillas = (textFileCasillas.text.Split('\n'));
	}

	public void bonusMoneyController(){
		switch(bonusController){
			case 1:
				if(plArray[(turn+1)%2].money >= 100){
					plArray[turn].money += 100;
					plArray[(turn+1)%2].money -= 100;
					//plArray[(turn+1)%2].money += 100; //?
					//plArray[turn].money -= 100; //?
				} else{
					plArray[turn].money += plArray[(turn+1)%2].money;
					plArray[(turn+1)%2].money = 0;
				}
				break;
			case 6:
				plArray[turn].money += 150;
				//plArray[(turn+1)%2].money += 150; //?
				break;
		}
	}

	public void bonusLocationController(){
		libreDePago = true;

		switch(bonusController){ //?
			case 0: //inicio
				HastaAqui = (28 - plArray[turn].cell);
				//HastaAqui = (28 - plArray[(turn+1)%2].cell);
				break;
			case 2: //punto blanco
				HastaAqui = (14 - plArray[turn].cell);
				//HastaAqui = (14 - plArray[(turn+1)%2].cell);
				break;
			case 3: //Focus group
				HastaAqui = (24 - plArray[turn].cell);
				//HastaAqui = (24 - plArray[(turn+1)%2].cell);
				break;
			case 4: //Asesoria grupal
				HastaAqui = (25 - plArray[turn].cell);
				//HastaAqui = (25 - plArray[(turn+1)%2].cell);
				break;
			case 5: //linkedIn
				HastaAqui = (27 - plArray[turn].cell);
				//HastaAqui = (27 - plArray[(turn+1)%2].cell);
				break;
			case 7: //Ssimulación entrevista
				HastaAqui = (4 - plArray[turn].cell);
				//HastaAqui = (4 - plArray[(turn+1)%2].cell);
				break;
			case 8: //visita a empresas
				HastaAqui = (22 - plArray[turn].cell);
				//HastaAqui = (22 - plArray[(turn+1)%2].cell);
				break;
			case 9: //inicio
				HastaAqui = (28 - plArray[turn].cell);
				//HastaAqui = (28 - plArray[(turn+1)%2].cell);
				break;
			case 10: //internship
				HastaAqui = (13 - plArray[turn].cell);
				//HastaAqui = (13 - plArray[(turn+1)%2].cell);
				break;
		}

		HastaAqui = ((HastaAqui + 28) % 28) * 22;
		DesdeAqui = 0;
		jugadorEnMovimiento = true;
	}

	public void showPanels(){
		int posActual = plArray[turn].cell;

		if(posActual == 0){
			//inicio
		} else if(posActual == 7){
			moviendoDetencion = false;
			plArray[turn].detencion =  true;
		} else if(posActual == 14){
			//punto blanco
		} else if(posActual == 21){
			DesdeAqui = 0;
			HastaAqui = 14 * 22;
			jugadorEnMovimiento = true;
			moviendoDetencion = true;
		} else if(posActual == 2 || posActual == 9 || posActual == 16 || posActual == 23){
			//bonus
			panelBonus.gameObject.SetActive(true);
			bonusController = Random.Range(0,11);
			textPanelBonus.text = textosDeBonus[bonusController];
		} else if(posActual == 5 || posActual == 12 || posActual == 19 || posActual == 26){
			//retos
		} else{
			//1, 3, 4, 6, 8, 10, 11, 13, 15, 17, 18, 20, 22, 24, 25, 27
			if(casillasCompradas[posActual] == 2){ //Cuando no tiene dueño le pregunta al jugador si la quiere comprar.
				if(plArray[turn].money >= 50){ //Si le alcanza
					panelComprar.gameObject.SetActive(true);
					textCasillaCompra.text = nombresDeCasillas[posActual];
				}else{ //No tiene dinero suficiente para comprar.
					panelSinDinero.gameObject.SetActive(true);
					textCasillaSinDinero.text = nombresDeCasillas[posActual];
				}
			}else if(casillasCompradas[posActual] == turn && !libreDePago){ //Si cae en una casilla que le pertenece a otro jugador.
				panelPagaRenta.gameObject.SetActive(true);
				textPrecioRenta.text = "Debes pagar: " + PrecioRentaCasillas[posActual];
				textCasillaPaga.text = nombresDeCasillas[posActual];
				if(plArray[turn].money >= PrecioRentaCasillas[posActual]){
					plArray[turn].money -= PrecioRentaCasillas[posActual];
					plArray[(turn+1)%2].money += PrecioRentaCasillas[posActual];
				}else{
					plArray[(turn+1)%2].money += plArray[turn].money;	
					plArray[turn].money = 0;
				}
			}else{ //Casilla propia
				PrecioRentaCasillas[posActual] *= 2;
				panelCasillaPropia.gameObject.SetActive(true);
				textCasillaPropia.text = nombresDeCasillas[posActual];
			}
		}

		if(plArray[(turn+1)%2].detencion == true){
			plArray[(turn+1)%2].detencion = false;
			turn = (turn+1)%2;
		}

		if(!moviendoDetencion){
			turn = (turn+1)%2;
			libreDePago = false;
			buttonTirarDado.gameObject.SetActive(true);
		}
	}
	
	public void playTurn(){
		Debug.Log(turn);
		buttonTirarDado.gameObject.SetActive(false);
		int dice = Random.Range(1,7);
		Dado.sprite = Resources.Load<Sprite> ("dado" + dice);
		DesdeAqui = 0;
		HastaAqui = dice*22;
		jugadorEnMovimiento = true;
	}

	public void RechazarCompra(){
		panelComprar.gameObject.SetActive(false);
	}

	public void AceptarCompra(){
		plArray[(turn+1)%2].money -= 50;
		//desplegar en lista de pertencias la cosa comprada
		if(casillasCompradas[plArray[(turn+1)%2].cell] == 2){
			casillasCompradas[plArray[(turn+1)%2].cell] = turn;
			plArray[(turn+1)%2].properties[plArray[(turn+1)%2].cell] = nombresDeCasillas[plArray[(turn+1)%2].cell];
		}

		for(int i = 0; i<28; i++){
			if((turn+1)%2 == 0){
				textPropertiesPlayerOne.text = " ";
			}else{
				textPropertiesPlayerTwo.text = " ";
			}
		}

		for(int i = 0; i<28;i++){
			if(plArray[(turn+1)%2].properties[i] != " "){
				if((turn+1)%2 == 0){
					textPropertiesPlayerOne.text += plArray[0].properties[i] + "\n";
				}else{
					textPropertiesPlayerTwo.text += plArray[1].properties[i] + "\n";
				}
			}
		}
		panelComprar.gameObject.SetActive(false);
	}

	public void ContinuarSinFondos(){
		panelSinDinero.gameObject.SetActive(false);
	}

	public void ContinuarBonus(){
		panelBonus.gameObject.SetActive(false);
		bonusMoneyController();
		bonusLocationController();
	}

	public void ContinuarPagaRenta(){
		panelPagaRenta.gameObject.SetActive(false);
	}

	public void ContinuarCasillaPropia(){
		panelCasillaPropia.gameObject.SetActive(false);
	}

	void Update(){
		if(jugadorEnMovimiento){
			posBoard = plArray[turn].cell/7;
			switch(posBoard){
				case 0://derecha
					goArray[turn].transform.Translate(1,0,0);
					break;
				case 1: //arriba
					goArray[turn].transform.Translate(0,1,0);
					break;
				case 2: //izquierda
					goArray[turn].transform.Translate(-1,0,0);
					break;
				case 3: //abajo
					goArray[turn].transform.Translate(0,-1,0);
					break;
			}

			DesdeAqui += 1;

			if(DesdeAqui%22 == 0){
				plArray[turn].cell = (plArray[turn].cell + 1)%28;
				if(plArray[turn].cell == 0 && !moviendoDetencion){
					plArray[turn].money += 200;
				}
			}

			if(DesdeAqui == HastaAqui){
				jugadorEnMovimiento = false;
				showPanels();
			}
		}

		textMoneyPlayerOne.text = "Dinero: " + plArray[0].money + "\nPropiedades: ";
		textMoneyPlayerTwo.text = "Dinero: " + plArray[1].money + "\nPropiedades: ";

		textNombreJugador1.text = player1Name;
		textNombreJugador2.text = player2Name;
	}
}