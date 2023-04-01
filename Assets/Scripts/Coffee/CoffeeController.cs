using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    [SerializeField] private CoffeeSO coffeeSO;
    [SerializeField] private float xFactor = 1f;
    [SerializeField] private float yFactor = 1f;
    [SerializeField] private float zFactor = 1f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform cup;
    [SerializeField] private Transform coffee;
    [SerializeField] private Transform lid;
    [SerializeField] private Transform sleeve;
    private CoffeeHolder coffeeHolder;
    [SerializeField] private float speed = 4f;
    private float lerpFactor = 0.1f;
    private float followDistanceZ = 1.0f;
    private bool isFollowing = false;
    private int index;
    private Animator animator;
    private Rigidbody rigidbody;
    private bool isGrounded = true;

    private void Awake()
    {
        coffeeHolder = FindObjectOfType<CoffeeHolder>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(coffeeSO.cupList[0],cup.transform.position,Quaternion.identity);
            Destroy(cup);
        }
    }

    private void FollowPlayer()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                int coffeeCount = coffeeHolder.CoffeeCount();
                float lerpedPositionX = Mathf.Lerp(transform.position.x, target.position.x, (coffeeCount * speed) / (lerpFactor * index) * Time.deltaTime);
                transform.position = new Vector3(lerpedPositionX, transform.position.y, target.position.z + followDistanceZ);
            }
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

    private void ClosePopup() // Animation event.
    {
        animator.ResetTrigger("Popup");
    }

    public void Jump()
    {
        isGrounded = false;
        target = null;
        isFollowing = false;
        float randomForceX = Random.Range(-5f, 5f) * xFactor;
        Vector3 randomForce = new Vector3(randomForceX, yFactor, zFactor);
        rigidbody.AddForce(randomForce, ForceMode.Impulse);
    }

    public bool IsFollowing()
    {
        return isFollowing;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFollowing)
            {
                coffeeHolder.AddCoffeeToList(gameObject, this);
                isFollowing = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrounded)
        {
            if (collision.gameObject.CompareTag("Coffee"))
            {
                if (!isFollowing) // Bu kahve takip etmiyor ise.
                {
                    if (collision.gameObject.GetComponent<CoffeeController>().IsFollowing())
                    {
                        coffeeHolder.AddCoffeeToList(gameObject, this);
                        isFollowing = true;
                    }
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

    }

}
