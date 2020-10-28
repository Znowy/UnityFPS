using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public int ammoGiven = 50;

    public SpawnItem itemSpawner;

    public void ReceiveDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        itemSpawner.SpawnAmmoBox(transform.position, ammoGiven);
        Destroy(gameObject);
    }
}
