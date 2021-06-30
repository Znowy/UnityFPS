using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public PlayerControl playerController;

    public void SpawnAmmoBox(Vector3 spawnPoint, int ammoGiven)
    {
        if (ammoGiven > 0)
        {
            GameObject AmmoBox = (GameObject)Instantiate(Resources.Load("Prefabs/AmmoBox"), spawnPoint, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            AmmoBox.GetComponent<AmmoPickup>().playerController = playerController;
            AmmoBox.GetComponent<AmmoPickup>().ammoGiven = ammoGiven;
        }
    }
}
