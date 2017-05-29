using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	public static int turn = 0;

	static GameObject goPlayer1, goPlayer2;
	static GameObject[] goArray = new GameObject[2];

	static string player1String; //duck,deer,DavidNoel,hen,pavoreal
	static string player2String;

	int[] casillasCompradas = {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2};


	string [] nombreDeCasillas = {"Inicio" , "Plan de Vida y Carrera", "Bonus", "Taller 4", "Simulacion de Entrevista", "Reto", "Maratone Oportunidades Laborales", 
	"Detencion", "Empleatec", "Bonus", "Modalidad de Experiencia Profesional", "Asesoria de Corriculum", "Reto", "Internship", "Punto Planco", 
	"Indice Predictivo de Comportamiento", "Bonus", "Retroalimentacion de IPC", "Platicas informaticas de Empresas", "Reto", "CETEC", "Detencion", 
	"Visitas a empresas", "Bonus", "Focus Group", "Asesoria Grupal de Curriculum", "Reto", "Asesoria de LinkedIn"};

	public Transform panelComprar, panelSinDinero, panelPagaRenta, panelCasillaPropia;

	public Text textCasillaCompra, textCasillaSinDinero, textCasillaPaga, textCasillaPropia;

	bool esperaBoton;

	public struct playerSt{
		public int money, cell;

		public void move(int squares){
			for(int i = 0; i < squares; i++){
				moveSquare(cell);
				cell = ++cell%28;
			}
		}

		void moveSquare(int pos){
			pos /= 7;
			switch(pos){
				case 0://derecha
					goArray[turn].transform.Translate(22,0,0);
					break;
				case 1: //arriba
					goArray[turn].transform.Translate(0,22,0);
					break;
				case 2: //izquierda
					goArray[turn].transform.Translate(-22,0,0);
					break;
				case 3: //abajo
					goArray[turn].transform.Translate(0,-22,0);
					break;
			}
		}
	}

	static playerSt[] plArray = new playerSt[2];

	public static void setplayer1String(string s){
		player1String = s;
	}

	public static void setplayer2String(string s){
		player2String = s;
		setbothplayers();
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

	// Use this for initialization
	void Start(){
		for(int i = 0; i < 2; i++){
			plArray[i].money = 0;
			plArray[i].cell = 0;
		}

		//for(int i = 0; i < 28; i++)
		//	casillasCompradas[i] = 2;

	}
	
	public void playTurn(){
		int dice = rollDice();
		plArray[turn].move(dice);

		int posActual = plArray[turn].cell;
		switch(posActual){
			case 1:
			case 3:
			case 4: 
			case 6: 
			case 8: 
			case 10:
			case 11:
			case 13:
			case 15:
			case 17:
			case 18:
			case 20:
			case 22:
			case 24:
			case 25:
			case 27:
			if(casillasCompradas[posActual] == 2){
				if(plArray[turn].money >= 50){
					panelComprar.gameObject.SetActive(true);
					textCasillaCompra.text = nombreDeCasillas[posActual];
					//eleccion de boton
					esperaBoton = true;
					while(esperaBoton){
					}

				}else{
					panelSinDinero.gameObject.SetActive(true);
					textCasillaSinDinero.text = nombreDeCasillas[posActual];
					esperaBoton = true;
					Debug.Log("diego");
					while(esperaBoton){

					}
					Debug.Log("hernan");
				}
			} else if(casillasCompradas[posActual] != turn){
				panelPagaRenta.gameObject.SetActive(true);
				textCasillaPaga.text = nombreDeCasillas[posActual];
				if(plArray[turn].money >= 50){
					plArray[turn].money -= 50;
					plArray[(turn+1)%2].money += 50;
				} else{
					plArray[(turn+1)%2].money += plArray[turn].money;	
					plArray[turn].money = 0;
				}
				esperaBoton = true;
				while(esperaBoton){

				}
			} else{
				panelCasillaPropia.gameObject.SetActive(true);
				textCasillaPropia.text = nombreDeCasillas[posActual];
				esperaBoton = true;
				while(esperaBoton){

				}
			}
			//comprar
			//si tiene dueno, se le resta una cantidad al jugador en turno y se le agrega al dueno
			//si es tuyo no pasa nada
			//si no tiene dueno
			break;
			case 2:
			case 9:
			case 16:
			case 23:
			//bonus

			break;
			case 5:
			case 12:
			case 19:
			case 26:
			//reto
			break;
			case 0:
			//inicio
			break;
			case 7:
			//detencion 1
			break;
			case 14:
			//punto blanco
			break;
			case 21:
			//detencion 2
			break;
		}

		turn = (turn+1)%2;
	}

	public void RechazarCompra(){
		esperaBoton = false;
		panelComprar.gameObject.SetActive(false);
	}

	public void AceptarCompra(){
		plArray[turn].money -= 50;
		//desplegar en lista de pertencias la cosa comprada
		casillasCompradas[plArray[turn].cell] = turn;
		esperaBoton = false;
		panelComprar.gameObject.SetActive(false);
	}

	public void ContinuarSinFondos(){
		panelSinDinero.gameObject.SetActive(false);
		esperaBoton = false;
	}

	public void ContinuarPagaRenta(){
		panelPagaRenta.gameObject.SetActive(false);
		esperaBoton = false;
	}

	public void ContinuarCasillaPropia(){
		panelCasillaPropia.gameObject.SetActive(false);
		esperaBoton = false;
	}


	// Update is called once per frame
	void Update(){
	}



	static int rollDice(){
		int num = Random.Range(1,7);
		Debug.Log(num);
		return num;
	}

	public  Image DonDado;
    public void BotonMamalon(){
        int num = Random.Range(1,7);
        DonDado.sprite = Resources.Load<Sprite> ("dado"+num);
    }
}