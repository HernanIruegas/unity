using UnityEngine;
using System.Collections;

public class collition : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name=="Wall"){
			wall.crash++;
	}
}
}