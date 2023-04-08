using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleeveMachine : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.CoffeeSleeving();
            coffeeController.Down();
        }
    }
}
