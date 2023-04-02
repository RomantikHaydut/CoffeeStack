using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    [Header("Jump Forces")]
    [SerializeField] private float xFactor = 0.3f;
    [SerializeField] private float yFactor = 6f;
    [SerializeField] private float zFactor = 10f;
    [Header("Transforms")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform cup;
    [SerializeField] private Transform coffee;
    [SerializeField] private Transform lid;
    [SerializeField] private Transform sleeve;
    private PlayerController playerController;
    private int index;
    private float speed = 4f;
    private float lerpFactor = 0.1f;
    private float followDistanceZ = 1.0f;
    private bool isFollowing = false;
    private bool isGrounded = true;
    private Animator animator;
    private Rigidbody rigidbody;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FollowPlayer()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                int coffeeCount = playerController.CoffeeCount();
                float lerpedPositionX = Mathf.Lerp(transform.position.x, target.position.x, (coffeeCount * speed) / (lerpFactor * index) * Time.deltaTime);
                transform.position = new Vector3(lerpedPositionX, transform.position.y, playerController.transform.position.z + followDistanceZ * index);
            }
        }
    }

    public void FollowPlayer2()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z + index);
                float xPos = Mathf.MoveTowards(transform.position.x, playerController.transform.position.x, speed * Time.deltaTime * playerController.CoffeeCount() / index * 2);
                //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                transform.position = new Vector3(xPos, transform.position.y, playerController.transform.position.z + index);
            }
        }
    }

    public void FollowPlayer3()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z + index);
                float targetPositionX;
                if (Mathf.Abs(target.position.x - transform.position.x) >= 0.005f)
                {
                    targetPositionX = (target.position.x - transform.position.x) * Time.deltaTime * (playerController.CoffeeCount() / index) * speed;
                }
                else
                {
                    targetPositionX = 0;
                }

                float targetPositionZ = target.position.z + 1;
                transform.position += new Vector3(targetPositionX, 0, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y, targetPositionZ);
            }
        }
    }

    public void SetFollower(Transform target)
    {
        this.target = target;
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
                playerController.AddCoffeeToList(gameObject, this);
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
                        playerController.AddCoffeeToList(gameObject, this);
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
