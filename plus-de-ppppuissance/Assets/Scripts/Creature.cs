using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {

    public Transform goal;
    public float jumpPower = 10;
    private NavMeshAgent agent;
    private Vector3 desiredVelocity;
    private Rigidbody rb;

    private bool jumping;

    static public float targetFactor = 50;

    private List<Creature> currentHerde = new List<Creature>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        desiredVelocity = Vector3.zero;
    }
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        jumping = false;
    }

    // Update is called once per frame
    void Update () {
        NavMeshHit hit;
        if (agent.Raycast(agent.nextPosition, out hit))
        {
            Debug.Log("Hit");
            Jump();
        }

        MoveCloserToTarget();
        HerdeBehavior();
    }

    private void FixedUpdate()
    {
        ClampVelocity();
        //rb.velocity = desiredVelocity;
        //desiredVelocity = rb.velocity;
        agent.velocity = desiredVelocity;
        
        //Performe actual movement based on our velocity
        //if (Mathf.Abs(velocity.x) > boidsController.maximalVelocity
        //    || Mathf.Abs(velocity.y) > boidsController.maximalVelocity
        //    || Mathf.Abs(velocity.z) > boidsController.maximalVelocity)
        //{
        //    float scaleFactor = boidsController.maximalVelocity
        //        / Mathf.Max(Mathf.Max(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y)), Mathf.Abs(velocity.z));
        //    velocity *= scaleFactor;
        //}

        //Check position and velocity, ensure the boid is within game space
        //if ((transform.position.x < boidsController.minX && velocity.x < 0)
        //    || (transform.position.x > boidsController.maxX && velocity.x > 0))
        //    velocity.x = -velocity.x * Random.Range(boidsController.reboundFactorMin, boidsController.reboundFactorMax);

        //if ((transform.position.y < boidsController.minY && velocity.y < 0)
        //    || (transform.position.y > boidsController.maxY && velocity.y > 0))
        //    velocity.y = -velocity.y * Random.Range(boidsController.reboundFactorMin, boidsController.reboundFactorMax);

        //if ((transform.position.z < boidsController.minZ && velocity.z < 0)
        //    || (transform.position.z > boidsController.maxZ && velocity.z > 0))
        //    velocity.z = -velocity.z * Random.Range(boidsController.reboundFactorMin, boidsController.reboundFactorMax);

        //Obstacle management
        //if (Physics.Raycast(transform.position, velocity, boidsController.reboundDistance))
        //{
        //    velocity = -velocity * Random.Range(boidsController.reboundFactorMin, boidsController.reboundFactorMax);
        //}

        //rb.velocity = velocity;
    }

    public float Distance(Creature other)
    {
        return Vector3.Distance(transform.position, other.transform.position);
    }

    private void Jump()
    {
        desiredVelocity += Vector3.up * jumpPower;
    }

    private void ClampVelocity()
    {
        if(desiredVelocity.magnitude > agent.speed)
        {
            float factor = agent.speed / desiredVelocity.magnitude;
            desiredVelocity *= factor;
        }
    }

    private void CheckIntendedDirection()
    {

    }

    public void SetHerde(List<Creature> creatures)
    {
        currentHerde = creatures;
    }

    //Herde behavior (group of creatures)
    private void HerdeBehavior()
    {
        MoveCloser(currentHerde);
        MoveWith(currentHerde);
        MoveAway(currentHerde);
    }



    //Move closer to a set of boids
    public void MoveCloser(List<Creature> creatures)
    {
        //    if (boids.Count < 1)
        //        return;

        //    //Calculate the average distances from the other boids
        //    Vector3 averageDistance = new Vector3(0, 0, 0);

        //    foreach (Boid boid in boids)
        //    {
        //        if (boid.transform.position == transform.position)
        //            continue;
        //        averageDistance += (transform.position - boid.transform.position);
        //    }
        //    averageDistance /= boids.Count;

        //    //Set our velocity towards the others
        //    velocity -= (averageDistance / boidsController.gatheringFactor);
    }

    //Move with a set of boids
    public void MoveWith(List<Creature> creatures)
    {
        //    if (boids.Count < 1)
        //        return;

        //    //Calculate the average velocity of the other boids
        //    Vector3 averageVelocity = new Vector3(0, 0, 0);

        //    foreach (Boid boid in boids)
        //    {
        //        averageVelocity += (boid.velocity);
        //    }
        //    averageVelocity /= boids.Count;

        //    //Set our velocity towards the others
        //    velocity += (averageVelocity / boidsController.gatheringFactor);
    }

    //Move away from a set of boids, this avoids crowding
    public void MoveAway(List<Creature> creatures)
    {
        //    if (boids.Count < 1)
        //        return;

        //    Vector3 distance = new Vector3(0, 0, 0);
        //    int numberClose = 0;

        //    float sqrtMinDist = Mathf.Sqrt(boidsController.minimalDistance);

        //    foreach (Boid boid in boids)
        //    {
        //        if (Distance(boid) < boidsController.minimalDistance)
        //        {
        //            numberClose++;

        //            Vector3 difference = transform.position - boid.transform.position;

        //            if (difference.x >= 0)
        //                difference.x = sqrtMinDist - difference.x;
        //            else
        //                difference.x = -sqrtMinDist - difference.x;

        //            if (difference.y >= 0)
        //                difference.y = sqrtMinDist - difference.y;
        //            else
        //                difference.y = -sqrtMinDist - difference.y;

        //            if (difference.z >= 0)
        //                difference.z = sqrtMinDist - difference.z;
        //            else
        //                difference.z = -sqrtMinDist - difference.z;

        //            distance += difference;
        //        }
        //    }

        //    if (numberClose == 0)
        //        return;

        //    velocity -= distance / boidsController.repulsionFactor;
    }

    //Move closer to a target
    public void MoveCloserToTarget()
    {
        //    //Set our velocity towards the target
        //    Vector3 direction = transform.position - target.transform.position;
        //    velocity -= (direction / boidsController.targetFactor);
        Vector3 direction = agent.steeringTarget - transform.position;
        desiredVelocity += direction / targetFactor;
    }
}
