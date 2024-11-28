using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private HealthManager healthManager; // Reference to HPmanager


    public virtual void OnStart() { }
    void Start() 
    {   
         // Find HealthManager in the scene
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager == null)
        {
            Debug.LogError("HealthManager could not be found in the scene!");
        }
        
        OnStart();
    }


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

    
    protected void GiveDamage(int damage)
    {
        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
            Debug.Log($"Enemy dealt {damage} damage to the player.");
        }
        else
        {
            Debug.LogError("HealthManager is not assigned!");
        }
    }

}
