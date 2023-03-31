using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coffee"))
        {
            FindObjectOfType<CoffeeHolder>().LineBreak(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
