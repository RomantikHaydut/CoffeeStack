using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialShow : MonoBehaviour
{
    [SerializeField] private List<GameObject> originalTriangle;
    [SerializeField] private GameObject cube;
    private Transform lastCube;
    private void Start()
    {
        lastCube = Instantiate(cube, new Vector3(0, 1, 0), Quaternion.identity).transform;
        InvokeRepeating("Spawn", 1f, 0.001f);

    }
    private void Spawn()
    {
        Vector3 direction = (RandomOriginalCube().position - lastCube.position) / 2;
        Vector3 spawnPos = transform.position += direction;
        lastCube = Instantiate(cube, spawnPos, Quaternion.identity).transform;

    }

    private Transform RandomOriginalCube()
    {
        int randomIndex = Random.Range(0, originalTriangle.Count);
        return originalTriangle[randomIndex].transform;
    }
}
