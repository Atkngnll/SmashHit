using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamıKırmak : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Top"))
        {
            Destroy(gameObject);
        }
    }
}
