using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sword : MonoBehaviour
{
    public int damage;
    List<Enemy> enemysList = new List<Enemy>();
    public float angle;
    private Animator slayAnimator;
    public Animator animator;
    void Start()
    {
        slayAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,angle+90); 

        // Retrieve the parameters driving the blend tree
        float horizontal = animator.GetFloat("X");
        float vertical = animator.GetFloat("Y");

        // Calculate the angle in degrees
        angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

        // Ensure the angle is positive (0 to 360 degrees)
        if (angle < 0)
        {
            angle += 360f;
        }
    }
    public void GiveDamage()
    {
        slayAnimator.SetTrigger("Slay");
        foreach (Enemy enemy in enemysList)
        {
            enemy.TakeDamage(damage);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
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
