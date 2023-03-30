using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float smoothFollowTime = 1f;

    [SerializeField] private float bound = 1f;


    private float offsetZ;

    private void Start()
    {
        offsetZ = player.position.z - transform.position.z;
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }

    private void SmoothFollow()
    {
        Vector3 targetPosition = new Vector3(player.position.x , transform.position.y , player.position.z - offsetZ);
        Vector3 lerpedPosition =  Vector3.Lerp(transform.position,targetPosition, Time.deltaTime * smoothFollowTime);
        lerpedPosition.x = Mathf.Clamp(lerpedPosition.x, -bound, bound);
        transform.position = lerpedPosition;
    }
}
