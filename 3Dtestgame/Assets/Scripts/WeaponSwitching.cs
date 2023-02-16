using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    public int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    void OnWeaponScroll(InputValue value)
    {
        int previousWeapon = currentWeapon;

        float scrollValue = value.Get<float>();

        if (scrollValue > 0f)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        else if (scrollValue < 0f)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

        if (currentWeapon != previousWeapon)
        {
            SelectWeapon();
        }
    }

    void OnWeaponSelect(InputValue value)
    {
        int previousWeapon = currentWeapon;

        currentWeapon = (int)value.Get<float>() - 1;

        if (currentWeapon != previousWeapon)
        {
            SelectWeapon();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
