using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> plateList;

    private int plateIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CoffeeController coffeeController))
        {
            coffeeController.GoToTable(Plate());
            IncreasePlateIndex();
        }
    }

    private Transform Plate()
    {
        return plateList[plateIndex].transform;
    }

    private void IncreasePlateIndex()
    {
        plateIndex++;
        if (plateIndex >= plateList.Count)
        {
            plateIndex = 0;
        }

        print(plateIndex + "asdasdas");
    }
}
