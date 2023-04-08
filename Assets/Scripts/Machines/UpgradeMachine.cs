using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeMachine : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private TMP_Text scoreText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.Upgrade();
            scoreText.text = coffeeController.getScore().ToString() + " $";
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
