using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class wall : MonoBehaviour {
	public Text txt;
	public Text msg;
	public static int crash;
	public static string mensaje;

	// Use this for initialization
	void Start () {
		txt.text = "Choques: ";
		crash = 0;
		msg.text = "";
		mensaje = "";
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "Choques: "+crash;
		msg.text = mensaje;
	}
}
