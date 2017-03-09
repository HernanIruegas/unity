using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoringControl : MonoBehaviour {
	public Text txt;
	public static int crash;

	// Use this for initialization
	void Start () {
		txt.text = "cubes destroyed: ";
		crash = 0;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "cubes destroyed: " + crash;
	}
}