using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour {
public Text scoreText;
public Text txtTargets;
public int neededPoints;
private int score;
public static int feedback;


    // Use this for initialization
    void Start () {
        score = 0;
        feedback = 10;
        neededPoints = 10;
        UpdateScore();		
	}
	
	// Update is called once per frame
	void Update () {
        if (score == 100)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }
            
        if (Input.GetKeyDown("q"))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }

    }

    public void AddScore()
    {
        score += 10;
        feedback += 20;
        neededPoints -= 1;
        UpdateScore();
        
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        txtTargets.text = "Targets left: " + neededPoints;
    }
}
