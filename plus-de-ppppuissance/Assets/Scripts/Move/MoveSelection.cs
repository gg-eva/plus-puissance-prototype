using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelection : MonoBehaviour {

    //Properties
    [Range(0, 1)]
    public float acceleration;

    [Range(0, 20)]
    public float velocityMax;

    //Inputs
    [HideInInspector]
    public float input_forward, input_right;

    [HideInInspector]
    public bool input_lock;

    //External game objects and scripts
    GameObject go_avatar;

    //Components
    private Rigidbody rb;

    //Variables
    private bool locked;
    private float unlockTrigger;

	void Start () {
        locked = true;
        unlockTrigger = 0.2f;
        rb = GetComponent<Rigidbody>();
        go_avatar = GameObject.FindGameObjectWithTag("Player");
	}

    void FixedUpdate () {

        Vector3 inputDirection = input_forward * transform.forward + input_right * transform.right;

        if (inputDirection.magnitude > unlockTrigger)
            locked = false;

        if (input_lock)
            locked = true;

        if (locked)
        {
            rb.MovePosition(go_avatar.transform.position);
        }
        else
        {
            Vector3 targetVelocity = velocityMax * inputDirection;
            if (targetVelocity.magnitude > velocityMax)
                targetVelocity *= velocityMax / targetVelocity.magnitude;

            Vector3 diffVelocity = targetVelocity - Vector3.ProjectOnPlane(rb.velocity, transform.up);

            rb.AddForce(acceleration * diffVelocity, ForceMode.VelocityChange);
        }
	}
}
