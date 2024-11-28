using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public virtual void OnUpdate() { }
    void Update()
    {
        OnUpdate();
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void TakeDamage(float damage)
    {
        health += damage;
    }

}
