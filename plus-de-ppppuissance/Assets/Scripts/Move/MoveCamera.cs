using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    //Properties
    [Range(0, 100)]
    public float angleFactor;

    [Range(0, 10)]
    public float angleSpeed;

    //External game object or scripts
    private GameObject go_avatar;

    //Components
    //private Rigidbody rb;

    //Variables
    private Vector3 distance;
    private float previousAngle;
    private float targetAngle;
    private float lerpFactor;
	
	void Start () {
        //rb = GetComponent<Rigidbody>();
        go_avatar = GameObject.FindGameObjectWithTag("Player");
        distance = transform.position - go_avatar.transform.position;
        previousAngle = 0;
        targetAngle = 0;
        lerpFactor = 0;
    }
	
	void Update () {
        transform.position = go_avatar.transform.position + distance;

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(go_avatar.transform.position + Vector3.up, Vector3.down, out hit, 30, layerMask))
        {
            float angle = angleFactor * Vector3.Dot(hit.normal.normalized, Vector3.back);

            if(targetAngle != angle)
            {
                previousAngle = Mathf.LerpAngle(previousAngle, targetAngle, lerpFactor);
                targetAngle = angle;
                lerpFactor = 0;
            }
        }

        transform.RotateAround(go_avatar.transform.position, Vector3.up, Mathf.LerpAngle(previousAngle, targetAngle, lerpFactor));

        if (lerpFactor < 1)
            lerpFactor += angleSpeed * Time.deltaTime;

        if (lerpFactor > 1)
            lerpFactor = 1;

        transform.LookAt(go_avatar.transform.position);
    }
}
