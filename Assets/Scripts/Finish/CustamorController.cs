using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustamorController : MonoBehaviour
{
    [SerializeField] private Transform coffeeHolderTransform;
    private Animator animator;
    private bool isPicked = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPicked)
        {
            if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
            {
                coffeeController.Sell();
                PickCoffee(coffeeController.gameObject);
                isPicked = true;
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                animator.SetBool("Picked", true);
                isPicked = true;
            }
        }

    }

    private void PickCoffee(GameObject coffee)
    {
        animator.SetBool("Picked", true);
        coffee.transform.parent = coffeeHolderTransform;
        coffee.transform.localPosition = Vector3.zero;
    }
}
