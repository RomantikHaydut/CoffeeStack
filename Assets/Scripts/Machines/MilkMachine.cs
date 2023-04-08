using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkMachine : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.CoffeeMilking();
        }
    }
}
