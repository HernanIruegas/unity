using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCube : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name=="Cube"){
			Destroy(col.gameObject);
		}
}
}
