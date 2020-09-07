using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kameraman : MonoBehaviour
{
    public float speed = 2;
    public float incrementFactor = 0.10f;

    public float spawnHelper = 4.5f;
    public GameObject ball;
    public float ballForce = 700;
    public static bool camMoving = false;
    //public GameObject restartButton;

    private CharacterController controller;
    private bool collision = false;
    private Camera _cam;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        Debug.Log("Speed is" + speed);

        float mousePosx = Input.mousePosition.x;
        float mousePosy = Input.mousePosition.y;

        Vector3 BallInstantiatePoint = _cam.ScreenToWorldPoint(new Vector3(mousePosx, mousePosy, _cam.nearClipPlane + spawnHelper));

        if (!collision && camMoving)
        {
            controller.Move(Vector3.forward * Time.deltaTime * speed);
            speed = speed + incrementFactor;
        }
        else if (collision || !camMoving)
        {
            controller.Move(Vector2.zero);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject ballRigid;
            ballRigid = Instantiate(ball, BallInstantiatePoint, transform.rotation) as GameObject;
            ballRigid.GetComponent<Rigidbody>().AddForce(Vector3.forward * ballForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glass"))
        {
            collision = true;
            Debug.Log("HELL YEAH!");
            camMoving = false;
            //restartButton.SetActive(true);
        }
    }

    public void StartCam()
    {
        camMoving = !camMoving;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
