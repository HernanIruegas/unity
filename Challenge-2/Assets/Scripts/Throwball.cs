using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwball : MonoBehaviour {

    GameObject prefab;
   

	void Start () {
        prefab = Resources.Load("Elven Long Bow Arrow") as GameObject;
       
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            GameObject arrow = Instantiate(prefab) as GameObject;
            arrow.AddComponent<BallTarget>();
            arrow.transform.position = transform.position + Camera.main.transform.forward * 2;
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 45;
        }
	}
}
