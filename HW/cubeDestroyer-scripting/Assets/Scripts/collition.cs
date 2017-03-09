using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collition : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name=="Cube"){
			scoringControl.crash++;
		}
	}
}
