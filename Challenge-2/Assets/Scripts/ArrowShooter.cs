
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{

    private GameObject arrowPrefab;

    // Use this for initialization
    void Start()
    {
        arrowPrefab = Resources.Load("Arrow") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newArrow = Instantiate(arrowPrefab) as GameObject;
            newArrow.transform.position = transform.position;
            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * GameController.feedback;
        }
    }
}