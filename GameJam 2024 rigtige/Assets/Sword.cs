using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sword : MonoBehaviour
{
    float damage;
    List<Enemy> enemysList = new List<Enemy>();

    void Start()
    {
        damage = -1;
    }

    void Update()
    {
        
    }
    public void GiveDamage ()
    {
        foreach (Enemy enemy in enemysList)
        {

            Debug.Log("givedamage");
            enemy.TakeDamage(damage);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("made contact with something");  
        if (other.tag=="enemy")
        {
            Debug.Log("adding enemy");
            Enemy otherEnemy = other.gameObject.GetComponent<Enemy>();
            if (otherEnemy != null)
            {
                enemysList.Add(otherEnemy);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            Enemy otherEnemy = other.gameObject.GetComponent<Enemy>();
            if (otherEnemy != null)
            {
                enemysList.Remove(otherEnemy);
            }
        }
    }
     
}
