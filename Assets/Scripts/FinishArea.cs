using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{ 
    private bool isFinished=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFinished)
            {
                FindObjectOfType<PlayerController>().FinishGame();
                isFinished = true;
            }
        }
    }
}
