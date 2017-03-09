using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class trigger : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		print ("Trigger action");
		wall.mensaje="Estos cubos valen el doble";
	}

	void OnTriggerExit (Collider other){
		print ("Trigger action");
		wall.mensaje="";
		
	}

}
