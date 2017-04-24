using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColWithPlayer : MonoBehaviour {

    public Transform messagewindow;


	


    void OnCollisionEnter(Collision ca)
    {
        string nombre = ca.gameObject.name;
        if (nombre == "Horse")
        {
           
           messagewindow.gameObject.SetActive(true);

        }
    }

}
