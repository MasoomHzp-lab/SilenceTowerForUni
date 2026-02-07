using UnityEngine;

public class LimbSwing : MonoBehaviour
{
    public float swingSpeed = 5f;
    public float swingPower = 50f;
    public Rigidbody2D body;
    private HingeJoint2D hinge;
    private float targetSpeed;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.useMotor = true;
    }

    void Update()
    {
        if (hinge == null || body == null) return;

        float moveSpeed = Mathf.Abs(body.linearVelocity.x);

        // فقط وقتی حرکت داره نوسان کن
        if (moveSpeed > 0.1f)
            targetSpeed = Mathf.Sin(Time.time * swingSpeed) * swingPower;
        else
            targetSpeed = 0;

        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = targetSpeed;
        hinge.motor = motor;
    }
}
