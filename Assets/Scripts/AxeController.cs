using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float rotateSpeed = 0.25f;

    public float bound = 0.25f;

    private void Start()
    {
        StartCoroutine(Swing_Coroutine());

    }
    private IEnumerator Swing_Coroutine()
    {
        float direction = 1f;
        while (true)
        {
            yield return null;
            if (transform.localRotation.z > bound)
            {
                direction = -1f;
            }
            else if (transform.localRotation.z < -bound)
            {
                direction = 1f;
            }
            transform.Rotate(Vector3.forward * 360 * Time.deltaTime * rotateSpeed * direction);
        }
    }
}
