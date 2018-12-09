using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCreature : MonoBehaviour {

    //Properties
    [Range(0, 1)]
    public float acceleration;

    [Range(0, 5)]
    public float velocityMax;

    [Range(0, 3)]
    public float scopeAvatar;

    [Range(0, 3)]
    public float scopeOthers;

    [Range(0, 3)]
    public float repulsionFactorAvatar;

    [Range(0, 3)]
    public float repulsionFactorOthers;

    //Inputs
    [HideInInspector]
    public Vector3 targetPosition;

    //External game objects and scripts
    private GameObject go_avatar;

    //Components
    private Rigidbody rb;

    //Variables
    private NavMeshPath path;
    private int nextCornerIndex;

	void Start () {
        targetPosition = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        path = new NavMeshPath();
        nextCornerIndex = 0;

        go_avatar = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate () {
        UpdateCorner();
        Move();
    }

    public void UpdateTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        RecalculatePath();
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    void RecalculatePath()
    {
        if(path != null)
            path.ClearCorners();

        NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
        nextCornerIndex = 0;
    }

    void UpdateCorner()
    {
        if (path != null && path.status != NavMeshPathStatus.PathInvalid &&  nextCornerIndex < path.corners.Length - 1)
        {
            Vector3 distanceToCorner = Vector3.ProjectOnPlane(path.corners[nextCornerIndex] - transform.position, Vector3.up);

            if (Mathf.Abs(distanceToCorner.magnitude) < 0.1f)
                ++nextCornerIndex;
        }
    }

    void Move()
    {
        //Velocity on a plane
        Vector3 diffVelocity = ComputeMoveVelocity() - Vector3.ProjectOnPlane(rb.velocity, Vector3.up);

        rb.AddForce(acceleration * diffVelocity, ForceMode.VelocityChange);
    }

    Vector3 ComputeMoveVelocity()
    {
        if (path == null || path.status == NavMeshPathStatus.PathInvalid)
            return Vector3.zero;

        Vector3 targetVelocity = velocityMax * (Vector3.ProjectOnPlane(path.corners[nextCornerIndex] - transform.position, Vector3.up));

        targetVelocity += MoveRepulsion();

        if (targetVelocity.magnitude > velocityMax)
            targetVelocity *= velocityMax / targetVelocity.magnitude;

        return targetVelocity;
    }

    Vector3 MoveRepulsion()
    {
        Vector3 repulsion = Vector3.zero;

        Vector3 direction = go_avatar.transform.position - transform.position;
        if (Mathf.Abs(direction.magnitude) < scopeAvatar)
            repulsion -= repulsionFactorAvatar * (1 + scopeAvatar - Mathf.Abs(direction.magnitude)) * direction.normalized;

        foreach(GameObject go_creature in GameObject.FindGameObjectsWithTag("Creature"))
        {
            if(go_creature != this.gameObject)
            {
                direction = go_creature.transform.position - transform.position;
                if (Mathf.Abs(direction.magnitude) < scopeOthers)
                    repulsion -= repulsionFactorOthers * (1 + scopeOthers - Mathf.Abs(direction.magnitude)) * direction.normalized;
            }
        }

        return Vector3.ProjectOnPlane(repulsion, Vector3.up);
    }
}
