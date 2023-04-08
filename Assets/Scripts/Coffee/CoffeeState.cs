using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeState : MonoBehaviour
{
    [SerializeField] private GameObject coffee;
    [SerializeField] private GameObject lid;
    [SerializeField] private GameObject sleeve;

    public void CoffeeFilling()
    {
        if (coffee != null)
        {
            coffee.SetActive(true);
        }
    }
    public void CoffeeLidding()
    {
        if (lid != null)
        {
            lid.SetActive(true);
        }
    }
    public void CoffeeSleeving()
    {
        if (sleeve != null)
        {
            sleeve.SetActive(true);
        }
    }

    public MeshRenderer CoffeeRenderer()
    {
        return coffee.GetComponent<MeshRenderer>();
    }

}
