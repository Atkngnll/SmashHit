using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kameraman : MonoBehaviour
{
    public float speed = 1;
    public float incrementFactor = 0.02f;
    public float spawnHelper = 4.5f;
    public GameObject ball;
    public float ballForce = 700;
    public GameObject restartButton;
    public GameObject quitButton;

    //We use this when we implement UI
    public static bool camMoving = false;

    private CharacterController cameraChar;
    //A boolean whose value will be determined by OnTriggerEnter
    private bool collision = false;
    private Camera _cam;


    // Use this for initialization
    void Start()
    {
        cameraChar = gameObject.GetComponent<CharacterController>();
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Speed is " + speed);

        float mousePosx = Input.mousePosition.x;
        float mousePosy = Input.mousePosition.y;

        Vector3 BallInstantiatePoint = _cam.ScreenToWorldPoint(new Vector3(mousePosx, mousePosy, _cam.nearClipPlane + spawnHelper));

        //This checks if we have collided
        if (!collision && camMoving)
        {
            cameraChar.Move(Vector3.forward * Time.deltaTime * speed);
            //This is so that the camera's movement will speed up
            speed = speed + incrementFactor;
        }
        else if (collision || !camMoving)
        {
            cameraChar.Move(Vector3.zero);
        }

        if (Input.GetMouseButtonDown(0) && camMoving)
        {
            GameObject ballRigid;
            ballRigid = Instantiate(ball, BallInstantiatePoint, transform.rotation) as GameObject;
            ballRigid.GetComponent<Rigidbody>().AddForce(Vector3.forward * ballForce);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("glass"))
        {
            collision = true;
            Debug.Log("Collided with glass!! Man down!!");
            camMoving = false;
            restartButton.SetActive(true);
            quitButton.SetActive(true);
        }
    }
    public void StartCam()
    {
        camMoving = !camMoving;
    }
    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Exitting Game");
        Application.Quit();
    }
}
