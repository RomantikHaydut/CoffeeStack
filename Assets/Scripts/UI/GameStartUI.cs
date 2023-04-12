using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            Destroy(gameObject);
        }
    }
}
