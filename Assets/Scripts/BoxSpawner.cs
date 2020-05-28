using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    public GameObject[] boxesPrefabs;

    void Start()
    {
        SpawnBox();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBox();
        }*/
    }

    public void SpawnBox()
    {
        Instantiate(boxesPrefabs[Random.Range(0,boxesPrefabs.Length)],transform.position, Quaternion.identity);
    }
}
