using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedForward = 5f;

    [SerializeField] private float speedUpward = 3;

    [SerializeField] private float bound = 3;

    [SerializeField] private float swipeSpeed = 8f;

    [SerializeField] private float coffeeMoneyValue = 0;

    [SerializeField] private List<GameObject> coffeeList;

    [SerializeField] private Transform coffeeHolderTransform;

    private bool isMoneyAreaCame = false;

    private bool isFinishAreaCame = false;

    private bool isGameFinished = false;

    private bool isGameStarted = false;

    private int money;

    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private CinemachineTransposer transposer;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (!isGameStarted)
        {
            if (Input.touchCount > 0)
            {
                isGameStarted = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isGameStarted)
        {

            if (!isGameFinished)
            {
                if (!isMoneyAreaCame)
                {
                    if (!isFinishAreaCame)
                    {
                        MovementForward();

                        MovementHorizontal();

                        MovementCoffees();
                    }
                    else
                    {
                        MovementForward();

                        MovementCoffees();
                    }
                }
                else if (isMoneyAreaCame)
                {
                    transform.position += Vector3.up * speedUpward * Time.deltaTime;
                }
            }
        }
    }

    private void MovementCoffees()
    {
        for (int i = 0; i < coffeeList.Count; i++)
        {
            coffeeList[i].GetComponent<CoffeeController>().FollowPlayer();
        }
    }

    public void AddCoffeeToList(GameObject coffee, in CoffeeController coffeeController)
    {
        coffeeList.Add(coffee);
        int index = coffeeList.IndexOf(coffee);
        coffeeController.SetIndex(index + 1);
        if (index != 0)
        {
            coffeeController.SetFollower(coffeeList[index - 1].transform);
        }
        else
        {
            coffeeController.SetFollower(coffeeHolderTransform);
        }

        StartCoroutine(PopupAllCoffees());
        SoundManager.Instance.PlayCollectSoundSound();
    }

    public void RemoveCoffeeFromList(GameObject coffee, in CoffeeController coffeeController)
    {
        if (coffeeList.Contains(coffee))                     // eger kahve listemizde varsa.
        {
            int index = coffeeList.IndexOf(coffee);          // indexini aliyoruz.
            if (index == coffeeList.Count - 1)               // eger indeximiz son bardak ise 
            {
                coffeeList.Remove(coffee);                   // onu listeden cikarip metodu bitiriyoruz.
                return;
            }

            coffeeList.Remove(coffee);                      // son index degil ise yine listeden cikariyoruz.

            if (index != 0)
            {
                coffeeList[index].GetComponent<CoffeeController>().SetFollower(coffeeList[index - 1].transform);
            }
            else
            {
                coffeeController.SetFollower(coffeeHolderTransform);
            }

            StartCoroutine(PopupAllCoffees());               // tum kahvelerin animasyonlarini aciyoruz.
        }

    }

    private IEnumerator PopupAllCoffees()
    {
        for (int i = coffeeList.Count - 1; i >= 0; i--)
        {
            if (coffeeList.Count > i)
            {
                coffeeList[i].gameObject.GetComponent<CoffeeController>().Popup();
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }

    public int CoffeeCount()
    {
        return coffeeList.Count;
    }

    public void LineBreak(GameObject triggredCoffee)
    {
        int index = coffeeList.IndexOf(triggredCoffee);

        for (int i = coffeeList.Count - 1; i >= index; i--)
        {
            coffeeList[i].GetComponent<CoffeeController>().Jump();
            coffeeList.RemoveAt(i);
        }
    }

    public void MoneyArea()
    {
        isMoneyAreaCame = true;
        money = ScoreManager.Instance.GetScore();
        StartCoroutine(FinishRotate_Coroutine());
    }

    public void FinishArea()
    {
        if (!isFinishAreaCame)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            StartCoroutine(CameraSmoothDamp_Coroutine());

            isFinishAreaCame = true;
            StartCoroutine(FinishArea_Coroutine());
        }
    }

    public void AddMoney(int money)
    {
        coffeeMoneyValue += money;
        moneyText.text = coffeeMoneyValue.ToString();
    }

    private IEnumerator CameraSmoothDamp_Coroutine()
    {
        float timer = 0;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            virtualCamera.LookAt = gameObject.transform;
            if (timer >= 1)
            {
           
                yield break;
            }
            transposer.m_FollowOffset += Vector3.up * timer * 0.75f * Time.deltaTime; // Kamera yukselisi !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    private IEnumerator FinishArea_Coroutine()
    {
        float startPositionX = transform.position.x;
        float timer = 0;
        float speed = 1;
        while(true)
        {
            yield return new WaitForFixedUpdate();
            timer+= Time.deltaTime * speed;
            if (timer >= 1)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                yield break;
            }
            float lerpedPositionX = Mathf.Lerp(startPositionX, 0, timer);
            transform.position = new Vector3(lerpedPositionX, transform.position.y, transform.position.z);

        }
    }

    private IEnumerator FinishRotate_Coroutine()
    {
        float rotationZ = 0;
        while (true)
        {
            if (rotationZ >= 90)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
                yield break;
            }
            yield return null;
            transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
            //transform.Rotate(Vector3.up * 36 * Time.deltaTime);
            rotationZ += Time.deltaTime * 360;
        }
    }


    private void MovementForward() // ileri hareket.
    {
        transform.position += Vector3.forward * Time.deltaTime * speedForward;
    }

    public void MovementHorizontal()
    {
        if (Input.touchCount > 0)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.localPosition.z;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, 50))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = transform.position.y;
                hitPoint.z = transform.position.z;
                hitPoint.x = Mathf.Clamp(hitPoint.x , -bound,bound);
                transform.position = Vector3.MoveTowards(transform.position, hitPoint, Time.deltaTime * swipeSpeed);

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true); 
            money -= 5;
            if (money <= 0)
            {
                isGameFinished = true;
                GameManager.Instance.FinishLevel();
            }
        }
    }
}
