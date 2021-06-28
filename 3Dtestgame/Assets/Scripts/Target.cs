using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public enum Relationship {Friendly, Enemy, Neutral};
    private Color[] colorState = new Color[] { Color.green, Color.red, Color.blue };

    public string enemyName;
    public float maxHealth;
    public float health;
    public int ammoGiven;
    public Relationship targetRelationship;

    public RectTransform healthbar;
    public float healthbarWidth;

    public Text nameText;

    public SpawnItem itemSpawner;

    private void Start()
    {
        nameText.text = enemyName;
        nameText.color = colorState[(int)targetRelationship];
    }

    public void ReceiveDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }

        healthbar.sizeDelta = new Vector2(health / (maxHealth / healthbarWidth), healthbar.sizeDelta.y);
    }

    void Die()
    {
        itemSpawner.SpawnAmmoBox(transform.position, ammoGiven);
        Destroy(gameObject);
    }
}
