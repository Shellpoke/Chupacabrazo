using UnityEngine;

public class GoatBitten : MonoBehaviour
{
    public Transform player;
    private bool isDead = false;
    public int health = 1;

    void Update()
    {
        if (isDead || player == null)
        {
            return;
        }
    }
    public void TakeBite(int damage)
    {
        if (isDead)
        {
            return;
        }

        health -= damage;

        Debug.Log(gameObject.name + " was bitten!");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}