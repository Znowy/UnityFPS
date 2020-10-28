using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoGiven = 50;

    public PlayerControl playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerController.ReceiveAmmo(ammoGiven);
            Destroy(gameObject);
            return;
        }
    }
}
