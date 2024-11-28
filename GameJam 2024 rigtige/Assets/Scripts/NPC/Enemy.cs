using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public virtual void OnUpdate() { }
    void Update()
    {
        OnUpdate();
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void TakeDamage(int damage)
    {
        health += damage;
    }

}
