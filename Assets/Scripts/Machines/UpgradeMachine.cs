using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMachine : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.Upgrade();
        }
    }
}
