using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleeveMachine : MonoBehaviour
{
    [SerializeField] private Transform coffee;
    private bool isTurned = false;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.CoffeeSleeving();
            coffeeController.Down();
            if (!isTurned)
            {
                isTurned = true;
                StartCoroutine(Turn_Coroutine());
            }

        }
    }

    private IEnumerator Turn_Coroutine()
    {
        float turnDegree = 0;
        while (true)
        {
            if (turnDegree >= 70f)
            {
                yield break;
            }
            yield return new WaitForFixedUpdate();
            coffee.Rotate(Vector3.up * 125 * Time.deltaTime, Space.Self);
            turnDegree += 125 * Time.deltaTime;

        }
    }
}
