using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LidMachine : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.CoffeeLidding();
            scoreText.text = coffeeController.GetScore().ToString() + " $";
            anim.SetBool("popUp", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        closeTrigger();
    }

    void closeTrigger()
    {
        anim.SetBool("popUp", false);
    }
}
