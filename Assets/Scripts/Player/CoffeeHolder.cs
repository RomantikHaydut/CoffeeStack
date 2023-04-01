using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> coffeeList;

    [SerializeField] private Transform coffeeHolderTransform;


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
}
