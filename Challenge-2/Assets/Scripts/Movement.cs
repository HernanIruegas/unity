using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour {

    
    
    public float speedH = 2.0f;
    private float yaw = 0.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
        yaw += speedH * Input.GetAxis("Mouse X");
        transform.Translate(Input.GetAxis("Horizontal") * 20.0F * Time.deltaTime, 0, Input.GetAxis("Vertical") * 25.0F * Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Horizontal") * 80.0F * Time.deltaTime, 0f);
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
        
        
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Substring(0,5) == "Agent")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(1);
        }
        
    }

    
}
