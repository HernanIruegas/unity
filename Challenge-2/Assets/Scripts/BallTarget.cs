using UnityEngine;
using UnityEngine.SceneManagement;

public class BallTarget : MonoBehaviour {

    public int scoreValue;
    public GameController gameController;
   // public AudioClip Hit;
	// Use this for initialization
	void Start () {
        /*GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = Hit;*/
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Problem: cannot find the GameController script");
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Target")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            gameController.AddScore();
            //GetComponent<AudioSource>().Play();
        }
        if (col.gameObject.name == "LooseTarget")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(1);
        }
        if (col.gameObject.name == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}

