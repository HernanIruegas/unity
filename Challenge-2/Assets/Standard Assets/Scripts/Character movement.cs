using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovement : MonoBehaviour {

    public float speed = 10.0F;
    float translation, leftright;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        leftright = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(leftright, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
	}
}
