using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coffee"))
        {
            FindObjectOfType<PlayerController>().LineBreak(other.gameObject);
            GetComponent<Collider>().enabled = false;
        }
    }
}
