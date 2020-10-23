using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text ammoText;

    public void UpdateAmmoUI(int currentAmmo, int ammoCount)
    {
        ammoText.text = currentAmmo + "/" + ammoCount;
    }

    public void UpdateCurrentAmmo(int currentAmmo)
    {
        ammoText.text = currentAmmo + "/" + ammoText.text.Substring(ammoText.text.IndexOf('/') + 1);
    }

    public void UpdateAmmoCount(int ammoCount)
    {
        ammoText.text = ammoText.text.Substring(0, ammoText.text.IndexOf('/') + 1) + ammoCount;
    }
}
