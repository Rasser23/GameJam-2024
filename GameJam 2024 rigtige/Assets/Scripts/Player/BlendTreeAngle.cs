using UnityEngine;

public class BlendTreeAngle : MonoBehaviour
{
    public Animator animator;
    public float angle;
    void Update()
    {
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
}
