using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeMachine : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject tripleCoffee;
    private Animator anim;
    private void Awake()
    {
     //   anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.Upgrade();
            scoreText.text = coffeeController.GetScore().ToString() + " $";
            // anim.SetBool("popUp", true);
            canvas.SetActive(true);
            tripleCoffee.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        //closeText();
    }

  /*  void closeText()
    {
        anim.SetBool("popUp", false);
    }*/
}
