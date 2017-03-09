using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour {

	public void ChangeScene(string scene){
			SceneManager.LoadScene(scene);
	}

	void Update () {

		if(scoringControl.crash == 36){
			SceneManager.LoadScene(1);
		}
	}
}
