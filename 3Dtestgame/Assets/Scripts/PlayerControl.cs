using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int ammoCount = 120;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
