using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedForward = 1f;

    [SerializeField] private float bound = 3;

    [SerializeField] private float swipeSpeed = 1f;

    private void Update()
    {
        MovementForward();
        if (Input.touchCount > 0)
        {
            MoveHorizontal();
        }
    }

    private void MovementForward() // ileri hareket.
    {
        transform.position += Vector3.forward * Time.deltaTime * speedForward;
    }

    public void MoveHorizontal()
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
