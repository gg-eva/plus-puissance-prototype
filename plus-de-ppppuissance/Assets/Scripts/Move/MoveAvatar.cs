using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAvatar : MonoBehaviour {

    //Properties
    [Range(0, 1)]
    public float acceleration;

    [Range(0, 5)]
    public float velocityMax;

    [Range(5, 25)]
    public float dashVelocity;

    [Range(0, 10)]
    public float dashDistance;

    [Range(0, 3)]
    public float dashTimer;

    //Inputs
    [HideInInspector]
    public float input_forward, input_right;

    [HideInInspector]
    public bool input_dash;

    //External game objects and scripts

    //TODO
    //DEDUCT ORIENTATION OF AVATAR WITH PATH
    //PATH ALSO USED FOR CAMERA

    //Components
    private Rigidbody rb;

    //Variables
    private bool canDash;
    private bool dashing;
    private Vector3 dashTarget;

    void Start () {
        canDash = true;
        dashing = false;
        rb = GetComponent<Rigidbody>();
    }
	

	void FixedUpdate () {
        if (input_dash && canDash || dashing && !canDash)
        {
            if (!dashing)
                InitDash();
            Dash();
        }
        else
            Move();

        //Debug.Log("INPUT X " + input_forward);
        //Debug.Log("INPUT Y  " + input_right);
        //Debug.Log("Speed " + Vector3.ProjectOnPlane(rb.velocity, Vector3.up).magnitude);
    }

    void InitDash()
    {
        rb.useGravity = false;
        dashTarget = transform.position + dashDistance * (input_forward * transform.forward + input_right * transform.right).normalized;
        dashing = true;
        StartCoroutine(DashTimer());
    }

    void EndDash()
    {
        dashing = false;
        rb.useGravity = true;
    }

    void Dash()
    {
        Vector3 direction = Vector3.ProjectOnPlane(dashTarget - transform.position, Vector3.up);
        if (Mathf.Abs(direction.magnitude) > 0.1f)
        {
            Vector3 diffVelocity;

            if (Mathf.Abs(direction.magnitude) > 0.5f)
                diffVelocity = dashVelocity * direction.normalized - rb.velocity;
            else
                diffVelocity = velocityMax * direction.normalized - rb.velocity;

            rb.AddForce(diffVelocity, ForceMode.VelocityChange);
        }
        else
            EndDash();
    }

    IEnumerator DashTimer()
    {
        canDash = false;
        yield return new WaitForSeconds(dashTimer);

        canDash = true;
        EndDash();
    }

    void Move()
    {
        //Velocity on a plane
        Vector3 diffVelocity = ComputeMoveVelocity() - Vector3.ProjectOnPlane(rb.velocity, Vector3.up);

        rb.AddForce(acceleration * diffVelocity, ForceMode.VelocityChange);
    }

    Vector3 ComputeMoveVelocity()
    {
        Vector3 targetVelocity = velocityMax * (input_forward * transform.forward + input_right * transform.right);
        if (targetVelocity.magnitude > velocityMax)
            targetVelocity *= velocityMax / targetVelocity.magnitude;

        return targetVelocity;
    }
}
