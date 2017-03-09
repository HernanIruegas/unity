using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name=="Cube1"){
			Destroy(col.gameObject);
		}
}
}