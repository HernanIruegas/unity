using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	static GameObject goPlayer1, goPlayer2;
	static GameObject[] goArray = new GameObject[2];
	

	static string player1String; //duck,deer,DavidNoel,hen,pavoreal
	static string player2String;


	//int casillasCompradas[28];
	//string nombreDeCasillas[28];

	public Transform panelComprar;

	public Text casillaParaComprar;

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
					//goArray[turn].transform.Translate(22,0,0);
					break;
				case 1: //arriba
					//goArray[turn].transform.Translate(0,22,0);
					break;
				case 2: //izquierda
					//goArray[turn].transform.Translate(-22,0,0);
					break;
				case 3: //abajo
					//goArray[turn].transform.Translate(0,-22,0);
					break;
			}
		}
	}

	static playerSt[] plArray = new playerSt[2];

	public static int turn = 0;

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

		//nombreDeCasillas = {
		//	"Inicio","Plan de Vida y Carrera", "Bonus", "Taller 4", ""
		//}

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
			//	if(casillasCompradas[posActual] == 2){
			//		panelComprar.gameObject.SetActive(true);
			//		casillaParaComprar.Text = 

			//	}
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

		turn = ++turn%2;
	}


	// Update is called once per frame
	void Update(){
	}

	public Sprite[] sprites;
	public Sprite deck;

	public int rollDice(){
		
		deck = Instantiate(sprites[1]) as Sprite;
		Vector3 scale = new Vector3( 4F, 4F, 0 );
    	transform.localScale = scale;
    	 Vector3 position = new Vector3( 96,44,0 );
    	 transform.position = position;

		int num = Random.Range(1,7);
		Debug.Log(num);
		return num;
	}
}