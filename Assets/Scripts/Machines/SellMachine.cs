using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMachine : MonoBehaviour
{
    [SerializeField] private Transform sellPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.Stole(sellPoint);
        }
    }
}
