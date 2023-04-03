using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    [Header("Jump Forces")]
    [SerializeField] private float xFactor = 0.3f;
    [SerializeField] private float yFactor = 6f;
    [SerializeField] private float zFactor = 10f;
    [Header("Transforms")]
    [SerializeField] private Transform target;
    [SerializeField] private List<GameObject> cupList;
    private int activeCupIndex = 0;
    private bool hasLid = false;
    private bool hasSleeve = false;
    private PlayerController playerController;
    private int index;
    [SerializeField] private float speed = 8f;
    private float lerpFactor = 0.1f;
    private float followDistanceZ = 0.15f;
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

    public void FollowPlayer()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                int coffeeCount = playerController.CoffeeCount();
                float lerpedPositionX = Mathf.Lerp(transform.position.x, target.position.x, (1f / index));
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
                float xPos = Mathf.MoveTowards(transform.position.x, playerController.transform.position.x, speed * Time.deltaTime * 0.1f * playerController.CoffeeCount() / index);
                //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                transform.position = new Vector3(xPos, transform.position.y, playerController.transform.position.z + index * followDistanceZ);
            }
        }
    }

    public void FollowPlayer3()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                float targetPositionX;
                if (Mathf.Abs(target.position.x - transform.position.x) >= 0.01f)
                {
                    targetPositionX = (target.position.x - transform.position.x) * Time.deltaTime * speed * (playerController.CoffeeCount() / (index));
                }
                else
                {
                    targetPositionX = 0;
                }

                float targetPositionZ = target.position.z + followDistanceZ;
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
        cupList[activeCupIndex].GetComponent<CoffeeState>().CoffeeFilling();
    }

    public void CoffeeLidding()
    {
        cupList[activeCupIndex].GetComponent<CoffeeState>().CoffeeLidding();
        hasLid = true;
    }

    public void CoffeeSleeving()
    {
        cupList[activeCupIndex].GetComponent<CoffeeState>().CoffeeSleeving();
        hasSleeve = true;
    }

    public void Upgrade()
    {
        if (activeCupIndex + 1 < cupList.Count)
        {
            cupList[activeCupIndex].SetActive(false);
            activeCupIndex++;
            cupList[activeCupIndex].SetActive(true);

            if (hasLid)
            {
                CoffeeLidding();
            }

            if (hasSleeve)
            {
                CoffeeSleeving();
            }
        }
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
