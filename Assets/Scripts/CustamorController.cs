using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustamorController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.Sell();
        }
    }
}
