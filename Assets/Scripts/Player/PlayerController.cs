using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedForward = 5f;

    [SerializeField] private float bound = 3;

    [SerializeField] private float swipeSpeed = 8f;

    [SerializeField] private List<GameObject> coffeeList;

    [SerializeField] private Transform coffeeHolderTransform;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        MovementForward();

        MovementHorizontal();

        MovementCoffees();
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
    }

    private IEnumerator PopupAllCoffees()
    {
        for (int i = coffeeList.Count - 1; i >= 0; i--)
        {
            coffeeList[i].gameObject.GetComponent<CoffeeController>().Popup();
            yield return new WaitForSecondsRealtime(0.1f);
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

                transform.position = Vector3.MoveTowards(transform.position, hitPoint, Time.deltaTime * swipeSpeed);

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bound, bound), transform.position.y, transform.position.z);
            }
        }

    }
}
