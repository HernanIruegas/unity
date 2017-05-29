using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

	public TextAsset textFileCasillas, textFileBonus;

	static string player1Name = "", player2Name = "";

	public static int turn = 0;

	static bool moviendoDetencion = false;

	static GameObject goPlayer1, goPlayer2;
	static GameObject[] goArray = new GameObject[2];

	static string player1String;
	static string player2String;

	public Image Dado, imagenTarjetaBonus, imagenTarjetaReto;

	//2 = está disponible, 0 = player one owns it, 1 = player two owns it
	int[] casillasCompradas = {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2};

	int[] precioRentaCasillas = {50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50};

	int[] respuestasRetos = {1,1,3,2,3,3,3,2,3};

	string[] nombresDeCasillas;
	
	public Transform panelComprar, panelSinDinero, panelPagaRenta, panelCasillaPropia, panelBonus, panelReto,
						panelRetoPregunta, panelRespuestaCorrecta, panelRespuestaIncorrecta, botonTirarDado,
						tarjetaBonusGenerica, tarjetaRetoGenerica;

	public Text textCasillaCompra, textCasillaSinDinero, textCasillaPaga, textCasillaPropia,
				textMoneyPlayerOne, textMoneyPlayerTwo, textPropertiesPlayerOne, textPropertiesPlayerTwo, textPrecioRenta,
				textNombreJugador1, textNombreJugador2, textJugadorEnTurno;

	public static bool jugadorEnMovimiento = false, jugadorEnBonus = false, tarjetaBonus = false;
	public static double HastaAqui;
	public static int posBoard, DesdeAqui, bonusController, retoController;

	public struct playerSt{
		public int money, cell, cant;
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
			player1String = s;
		else
			player2String = s;
	}

	public static void setbothplayers(){
		goPlayer1 = Resources.Load(player1String) as GameObject;
		goPlayer2 = Resources.Load(player2String) as GameObject;

		goArray[0] = Instantiate(goPlayer1) as GameObject;
		goArray[1] = Instantiate(goPlayer2) as GameObject;
		goArray[0].transform.position = new Vector3(55,190,0);
		goArray[1].transform.position = new Vector3(279,190,0);
		goArray[0].transform.localScale += new Vector3(5.5F, 5.5F, 0);
		goArray[1].transform.localScale += new Vector3(5.5F, 5.5F, 0);

		goArray[0] = Instantiate(goPlayer1) as GameObject;
		goArray[1] = Instantiate(goPlayer2) as GameObject;
		goArray[0].transform.position = new Vector3(91,44,0);
		goArray[1].transform.position = new Vector3(91,44,0);
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

		textPropertiesPlayerOne.text = "Sin Propiedades";
		textPropertiesPlayerTwo.text = "Sin Propiedades";

		nombresDeCasillas = (textFileCasillas.text.Split('\n'));
	}

	public void bonusMoneyController(){
		//Debug.Log("money control " + turn);
		switch(bonusController){
			case 1:
				if(plArray[(turn+1)%2].money >= 100){
					plArray[turn].money += 100;
					plArray[(turn+1)%2].money -= 100;
				} else{
					plArray[turn].money += plArray[(turn+1)%2].money;
					plArray[(turn+1)%2].money = 0;
				}
				terminaTurno();
				revisarDetencion();
				break;
			case 6:
				plArray[turn].money += 150;
				terminaTurno();
				revisarDetencion();
				break;
		}
	}

	public void bonusLocationController(){
		Debug.Log("location control" + bonusController);
		switch(bonusController){
			case 0: //inicio
				HastaAqui = (28 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 2: //punto blanco
				HastaAqui = (14 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 3: //Focus group
				HastaAqui = (24 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 4: //Asesoria grupal
				HastaAqui = (25 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 5: //linkedIn
				HastaAqui = (27 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 7: //Ssimulación entrevista
				HastaAqui = (4 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 8: //visita a empresas
				HastaAqui = (22 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 9: //inicio
				HastaAqui = (28 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
			case 10: //internship
				HastaAqui = (13 - plArray[turn].cell);
				HastaAqui = ((HastaAqui + 28) % 28) * 22;
				jugadorEnMovimiento = true;
				break;
		}
		DesdeAqui = 0;
	}

	public void showPanels(){
		Debug.Log("showPanels " + turn);

		int posActual = plArray[turn].cell;

		if(posActual == 0){
			//inicio
			terminaTurno();
			revisarDetencion();
		} else if(posActual == 7){
			Debug.Log("Caso 7: " + turn);
			moviendoDetencion = false;
			plArray[turn].detencion = true;
			terminaTurno();
			revisarDetencion();
			Debug.Log("Caso 7: " + turn);
		} else if(posActual == 14){
			//punto blanco
			terminaTurno();
			revisarDetencion();
		} else if(posActual == 21){
			Debug.Log("Caso 21: " + turn);
			DesdeAqui = 0;
			HastaAqui = 14 * 22;
			jugadorEnMovimiento = true;
			moviendoDetencion = true;
			Debug.Log("Caso 21: " + turn);
		} else if(posActual == 2 || posActual == 9 || posActual == 16 || posActual == 23){
			//bonus
			jugadorEnBonus = true;
			tarjetaBonus = true;
			panelBonus.gameObject.SetActive(true);
		} else if(posActual == 5 || posActual == 12 || posActual == 19 || posActual == 26){
			//retos
			panelReto.gameObject.SetActive(true);
		} else{;
			//1, 3, 4, 6, 8, 10, 11, 13, 15, 17, 18, 20, 22, 24, 25, 27
			if(casillasCompradas[posActual] == 2){ //Cuando no tiene dueño le pregunta al jugador si la quiere comprar.
				if(plArray[turn].money >= 50){ //Si le alcanza
					panelComprar.gameObject.SetActive(true);
					textCasillaCompra.text = nombresDeCasillas[posActual];
				} else{ //No tiene dinero suficiente para comprar.
					panelSinDinero.gameObject.SetActive(true);
					textCasillaSinDinero.text = nombresDeCasillas[posActual];
				}
			} else if(casillasCompradas[posActual] != turn && !jugadorEnBonus){ //Si cae en una casilla que le pertenece a otro jugador.
				panelPagaRenta.gameObject.SetActive(true);
				textPrecioRenta.text = "Debes pagar: " + precioRentaCasillas[posActual];
				textCasillaPaga.text = nombresDeCasillas[posActual];
			} else if(casillasCompradas[posActual] == turn){ //Casilla propia
				precioRentaCasillas[posActual] *= 2;
				panelCasillaPropia.gameObject.SetActive(true);
				textCasillaPropia.text = nombresDeCasillas[posActual];
			} else{
				terminaTurno();
				revisarDetencion();
			}
		}
		Debug.Log("casi if:  " + turn);
	}

	void revisarDetencion(){
		if(plArray[turn].detencion == true){
			plArray[turn].detencion = false;
			turn = (turn+1)%2;
		}
	}
	
	void terminaTurno(){
		turn = (turn+1)%2;
		jugadorEnBonus = false;
		botonTirarDado.gameObject.SetActive(true);
		tarjetaBonusGenerica.gameObject.SetActive(false);
		tarjetaRetoGenerica.gameObject.SetActive(false);
	}

	public void playTurn(){
		botonTirarDado.gameObject.SetActive(false);
		int dice = Random.Range(1,7);
		Dado.sprite = Resources.Load<Sprite> ("dado" + dice);
		DesdeAqui = 0;
		HastaAqui = dice*22;
		jugadorEnMovimiento = true;
	}

	public void RechazarCompra(){
		panelComprar.gameObject.SetActive(false);
		terminaTurno();
		revisarDetencion();
	}

	public void AceptarCompra(){
		plArray[turn].money -= 50;
		casillasCompradas[plArray[turn].cell] = turn;
		if(turn == 0){
			if(textPropertiesPlayerOne.text == "Sin Propiedades"){
				textPropertiesPlayerOne.text = nombresDeCasillas[plArray[turn].cell];
			} else{
				textPropertiesPlayerOne.text += "\n"+ nombresDeCasillas[plArray[turn].cell];
			}
		} else{
			if(textPropertiesPlayerTwo .text == "Sin Propiedades"){
				textPropertiesPlayerTwo.text = nombresDeCasillas[plArray[turn].cell];
			} else{
				textPropertiesPlayerTwo.text += "\n"+ nombresDeCasillas[plArray[turn].cell];
			}
		}
		plArray[turn].cant++;
		panelComprar.gameObject.SetActive(false);
		terminaTurno();
		revisarDetencion();
		if(plArray[turn].cant + plArray[(turn+1)%2].cant == 16){
			if(plArray[turn].cant > plArray[(turn+1)%2].cant){
				//gana turn
				SceneManager.LoadScene(3);

			} else if(plArray[turn].cant < plArray[(turn+1)%2].cant){ 
				SceneManager.LoadScene(4);//gana turn+1

			} else{
				if(plArray[turn].money > plArray[(turn+1)%2].money){
					SceneManager.LoadScene(3);//gana turn

				} else if(plArray[turn].money < plArray[(turn+1)%2].money){ 
					SceneManager.LoadScene(4);//gana turn+1

				} else{ //empate

				}
			}
		}
	}

	public void ContinuarSinFondos(){
		panelSinDinero.gameObject.SetActive(false);
		terminaTurno();
	}

	public void tomarTarjetaBonus(){
		panelBonus.gameObject.SetActive(false);
		bonusController = Random.Range(0,11);
		imagenTarjetaBonus.sprite = Resources.Load<Sprite> ("bonus" + bonusController);
		tarjetaBonusGenerica.gameObject.SetActive(true);
		bonusMoneyController();
		bonusLocationController();
	}

	public void tomarTarjetaReto(){
		panelReto.gameObject.SetActive(false);
		retoController = Random.Range(0,9);
		imagenTarjetaReto.sprite = Resources.Load<Sprite> ("reto" + retoController);
		tarjetaRetoGenerica.gameObject.SetActive(true);
		panelRetoPregunta.gameObject.SetActive(true);
	}

	public void contestaPregunta(int r){
		panelRetoPregunta.gameObject.SetActive(false);
		if(r == respuestasRetos[retoController]){
			panelRespuestaCorrecta.gameObject.SetActive(true);
		} else{
			panelRespuestaIncorrecta.gameObject.SetActive(true);
		}
	}

	public void continuarPregunta(){
		panelRespuestaCorrecta.gameObject.SetActive(false);
		panelRespuestaIncorrecta.gameObject.SetActive(false);
		terminaTurno();
		revisarDetencion();
	}

	public void ContinuarPagaRenta(){
		panelPagaRenta.gameObject.SetActive(false);
		if(plArray[turn].money >= precioRentaCasillas[plArray[turn].cell]){
			plArray[turn].money -= precioRentaCasillas[plArray[turn].cell];
			plArray[(turn+1)%2].money += precioRentaCasillas[plArray[turn].cell];
		} else{
			plArray[(turn+1)%2].money += plArray[turn].money;	
			plArray[turn].money = 0;
		}
		terminaTurno();
		revisarDetencion();
	}

	public void ContinuarCasillaPropia(){
		panelCasillaPropia.gameObject.SetActive(false);
		terminaTurno();
		revisarDetencion();
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

		textMoneyPlayerOne.text = "$" + plArray[0].money + "\n\nPropiedades: ";
		textMoneyPlayerTwo.text = "$" + plArray[1].money + "\n\nPropiedades: ";

		textNombreJugador1.text = player1Name;
		textNombreJugador2.text = player2Name;

		if(turn == 0){
			textJugadorEnTurno.text = "Turno: " + player1Name;
		} else{
			textJugadorEnTurno.text = "Turno: " + player2Name;
		}
	}
}