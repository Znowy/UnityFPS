using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int ammoCount = 120;

    public AmmoUI ammoHud;

    // If onlyUseIfMax is true, then ammo is only used if ammoSpent <= ammoCount
    public int SpendAmmo(int ammoSpent, bool onlyUseIfMax)
    {
        if (ammoSpent <= ammoCount)
        {
            ammoCount -= ammoSpent;
            return ammoSpent;
        }
        else if (!onlyUseIfMax)
        {
            if (ammoCount > 0)
            {
                int ammoUsed = ammoCount;
                ammoCount = 0;
                return ammoUsed;
            }
        }
        return 0;
    }

    public void ReceiveAmmo(int ammoReceived)
    {
        ammoCount += ammoReceived;
        ammoHud.UpdateAmmoCount(ammoCount);
    }
}
