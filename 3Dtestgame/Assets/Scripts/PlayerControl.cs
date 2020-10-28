using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float health = 100f;
    public int ammoCount = 120;

    public PlayerUI playerHud;

    // Start is called before the first frame update
    void Start()
    {
        playerHud.healthHud.UpdateHealthUI(health);
    }

    // If onlyUseIfMax is true, then ammo is only used if ammoSpent <= ammoCount
    public int SpendAmmo(int ammoSpent, bool onlyUseIfMax)
    {
        if (ammoSpent <= ammoCount)
        {
            ammoCount -= ammoSpent;
            playerHud.ammoHud.UpdateAmmoCount(ammoCount);
            return ammoSpent;
        }
        else if (!onlyUseIfMax)
        {
            if (ammoCount > 0)
            {
                int ammoUsed = ammoCount;
                ammoCount = 0;
                playerHud.ammoHud.UpdateAmmoCount(ammoCount);
                return ammoUsed;
            }
        }
        return 0;
    }

    public void ReceiveAmmo(int ammoReceived)
    {
        ammoCount += ammoReceived;
        playerHud.ammoHud.UpdateAmmoCount(ammoCount);
    }
}
