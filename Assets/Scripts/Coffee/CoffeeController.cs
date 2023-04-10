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
    [SerializeField] private List<GameObject> cupList;
    private int activeCupIndex = 0;
    private bool hasLid = false;
    private bool hasSleeve = false;
    private bool hasCoffee = false;
    private PlayerController playerController;
    private int index;
    [SerializeField] private float speed = 8f;
    [SerializeField] private Material milkyCoffeeMaterial;
    private float lerpFactor = 0.1f;
    private float followDistanceZ = 0.15f;
    private bool isFollowing = false;
    private bool isGrounded = true;
    private bool isSold = false;
    private Animator animator;
    private Rigidbody rigidbody;
    private int score = 0;
    private int coffee_money = 5;
    private int lid_money = 10;
    private int sleve_money = 15;
    private int upgrade_money = 20;


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
                if (index != 1)
                {
                    transform.position = new Vector3(lerpedPositionX, transform.position.y, target.transform.position.z + followDistanceZ);
                }
                else
                {
                    transform.position = new Vector3(lerpedPositionX, transform.position.y, target.transform.position.z);
                }

            }
        }
    }
    public int GetScore()
    {
        return score;
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
        if (!hasCoffee)
        {
            score += coffee_money;
        }
        hasCoffee = true;
    }

    public void CoffeeLidding()
    {

        cupList[activeCupIndex].GetComponent<CoffeeState>().CoffeeLidding();
        if (!hasLid)
        {
            score += lid_money;
        }
        hasLid = true;
    }

    public void CoffeeSleeving()
    {
        cupList[activeCupIndex].GetComponent<CoffeeState>().CoffeeSleeving();
        if (!hasSleeve)
        {
            score += sleve_money;
        }
        hasSleeve = true;
    }

    public void CoffeeMilking()
    {
        if (activeCupIndex == 0)
        {
            if (hasCoffee)
            {
                cupList[activeCupIndex].GetComponent<CoffeeState>().CoffeeRenderer().material = milkyCoffeeMaterial;
            }
        }
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
            score += upgrade_money;
            Popup();
        }
    }
    public void Down()
    {
        animator.SetTrigger("Down");
    }
    public void Popup()
    {
        animator.SetTrigger("Popup");
    }

    private void ClosePopup() // Animation event.
    {
        animator.ResetTrigger("Popup");
    }

    private void CloseDown()
    {
        animator.ResetTrigger("Down");
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

    public void Sell()
    {
        ScoreManager.Instance.AddScore(score);
        playerController.RemoveCoffeeFromList(gameObject, this);
        rigidbody.isKinematic = true;
        isSold = true;
    }

    public void Stole(Transform newTarget)
    {
        GetComponent<BoxCollider>().enabled = false;
        playerController.RemoveCoffeeFromList(gameObject, this);
        StartCoroutine(CloseObject(2f));
        StartCoroutine(Stole_Coroutine(newTarget));
    }

    private IEnumerator Stole_Coroutine(Transform newTarget)
    {
        rigidbody.isKinematic = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, newTarget.position.z);
        Vector3 dir = (newTarget.position - transform.position).normalized;
        float moveSpeed = 1f;
        this.enabled = false;
        while (true)
        {
            yield return null;
            transform.position += dir * Time.deltaTime * moveSpeed;
        }
    }

    private IEnumerator CloseObject(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }

    public void Destroy()
    {
        playerController.RemoveCoffeeFromList(gameObject,this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFollowing)
            {
                if (!isSold)
                {
                    playerController.AddCoffeeToList(gameObject, this);
                    isFollowing = true;
                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrounded)
        {
            if (collision.gameObject.CompareTag("Coffee"))
            {
                if (!isSold)
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
