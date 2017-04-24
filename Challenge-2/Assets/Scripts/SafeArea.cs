using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeArea : MonoBehaviour
{
    public Transform messagewindow;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision ca)
    {
        string nombre = ca.gameObject.name;
        if (nombre == "Player")
        {
            messagewindow.gameObject.SetActive(false);
        }
    }


}
