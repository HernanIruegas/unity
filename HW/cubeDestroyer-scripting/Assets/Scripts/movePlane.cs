using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  
 public class movePlane : MonoBehaviour
  
 {      
        void Update(){

                transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        }
 }
