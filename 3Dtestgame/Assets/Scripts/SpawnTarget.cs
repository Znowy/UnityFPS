using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public SpawnItem itemSpawner;

    public void SpawnWoodenCrate(Vector3 spawnPoint, float health)
    {
        GameObject WoodenCrate = (GameObject)Instantiate(Resources.Load("Prefabs/WoodenCrate"), spawnPoint, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        WoodenCrate.GetComponent<Target>().itemSpawner = itemSpawner;
    }
}
