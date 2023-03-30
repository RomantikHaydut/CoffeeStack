using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Transform coffee; 
    [SerializeField] private Transform lid;
    [SerializeField] private Transform sleeve;
    private CoffeeHolder coffeeHolder;
    [SerializeField] private float speed = 4f;
    private float lerpFactor = 0.1f;
    private float followDistanceZ = 1.0f;
    private bool canFollow = false;
    private int index;
    private Animator animator;

    private void Awake()
    {
        coffeeHolder = FindObjectOfType<CoffeeHolder>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (canFollow)
        {
            int coffeeCount = coffeeHolder.CoffeeCount();
            float lerpedPositionX = Mathf.Lerp(transform.position.x, target.position.x, (coffeeCount * speed) / (lerpFactor * index) * Time.deltaTime);
            transform.position = new Vector3(lerpedPositionX, transform.position.y, target.position.z + followDistanceZ);
        }
    }

    public void SetFollower(Transform follower)
    {
        target = follower;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void CoffeeFilling()
    {
        coffee.gameObject.SetActive(true);
    }
    public void CoffeeLidding()
    {
        lid.gameObject.SetActive(true);
    }
    public void CoffeeSleeving()
    {
        sleeve.gameObject.SetActive(true);
    }

    public void Popup()
    {
        animator.SetTrigger("Popup");
    }

    private void ClosePopup()
    {
        animator.ResetTrigger("Popup");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!canFollow)
            {
                coffeeHolder.AddCoffeeToList(gameObject, this);
                canFollow = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Coffee"))
        {
            if (!canFollow)
            {
                coffeeHolder.AddCoffeeToList(gameObject, this);
                canFollow = true;
            }
        }
    }

}
