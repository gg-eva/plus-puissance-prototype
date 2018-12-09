using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    //External game object or scripts
    private GameObject go_avatar;

    //Components
    private Rigidbody rb;

    //Variables
    private Vector3 distance;
	
	void Start () {
        rb = GetComponent<Rigidbody>();
        go_avatar = GameObject.FindGameObjectWithTag("Player");
        distance = transform.position - go_avatar.transform.position;
	}
	
	void Update () {
        transform.position = go_avatar.transform.position + distance;
    }
}
